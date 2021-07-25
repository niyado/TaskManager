using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using TaskManager.ViewModels;
using TaskManager.Models;
using TaskManager.Dialogs;
using System.Collections.ObjectModel;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Newtonsoft.Json;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace TaskManager
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {

        public MainPage()
        {
            InitializeComponent();
            DataContext = new MainViewModel();

            var handler = new WebRequestHandler();
            var items = JsonConvert.DeserializeObject<List<Item>>(handler.Get("http://localhost/TaskManagerAPI/item/getall").Result);
            var context = DataContext as MainViewModel;

            items.ForEach(context.Items.Add);
        }

        private async void AddTask_Click(object sender, RoutedEventArgs e)
        {
            var diag = new TaskDialog((DataContext as MainViewModel).Items);
            await diag.ShowAsync();
        }

        private async void AddAppt_Click(object sender, RoutedEventArgs e)
        {
            
            var diag = new AppointmentDialog((DataContext as MainViewModel).Items);
            await diag.ShowAsync();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as MainViewModel).Remove();
        }

        private async void Edit_Click(object sender, RoutedEventArgs e)
        {
            var mainViewModel = DataContext as MainViewModel;
            if (mainViewModel.SelectedItem is Task)
            {
                var diag = new TaskDialog(mainViewModel.Items, mainViewModel.SelectedItem);
                await diag.ShowAsync();
            }
            else if(mainViewModel.SelectedItem is Appointment)
            {
                var diag = new AppointmentDialog(mainViewModel.Items, mainViewModel.SelectedItem);
                await diag.ShowAsync();
            }
        }

        private void Sort_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as MainViewModel).Sort();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            var directory = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

            var result = JsonConvert.SerializeObject((DataContext as MainViewModel).Items, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All
            });
            File.WriteAllText(@directory+"/storedData.json", result);
        }

        private void Load_Click(object sender, RoutedEventArgs e)
        {
            var directory = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            var temp = new ObservableCollection<Item>(JsonConvert.DeserializeObject<ObservableCollection<Item>>(File.ReadAllText(@directory + "/storedData.json"), new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto
            }));

            (DataContext as MainViewModel).Items.Clear();
            for (int i = 0; i < temp.Count; i++)
            {
                if(temp.ElementAt(i) is Task)
                    (DataContext as MainViewModel).Items.Add(temp.ElementAt(i) as Task);
                else if(temp.ElementAt(i) is Appointment)
                    (DataContext as MainViewModel).Items.Add(temp.ElementAt(i) as Appointment);
            }
        }

        private async void Search_Click(SearchBox sender, SearchBoxQuerySubmittedEventArgs args)
        {
            if(args.QueryText == "")
            {
                ItemsList.ItemsSource = (DataContext as MainViewModel).Items;
                return;
            }
            var handler = new WebRequestHandler();
            var results = JsonConvert.DeserializeObject<List<Item>>(await handler.Post("http://localhost/TaskManagerAPI/item/search", args.QueryText));

            (DataContext as MainViewModel).Items.Clear();
            results.ForEach((DataContext as MainViewModel).Items.Add);
        }

        private async void Incomplete_Click(object sender, RoutedEventArgs e)
        {
            var handler = new WebRequestHandler();
            var results = JsonConvert.DeserializeObject<List<Item>>(await handler.Post("http://localhost/TaskManagerAPI/item/sortbycompletion", false));
            // ItemsList.ItemsSource = results;
            (DataContext as MainViewModel).Items.Clear();
            results.ForEach((DataContext as MainViewModel).Items.Add);
        }

        private async void Completed_Click(object sender, RoutedEventArgs e)
        {
            var handler = new WebRequestHandler();
            var results = JsonConvert.DeserializeObject<List<Item>>(await handler.Post("http://localhost/TaskManagerAPI/item/sortbycompletion", true));

            (DataContext as MainViewModel).Items.Clear();
            results.ForEach((DataContext as MainViewModel).Items.Add);
        }

        private void ShowAll_Click(object sender, RoutedEventArgs e)
        {
            var handler = new WebRequestHandler();
            var results = JsonConvert.DeserializeObject<List<Item>>(handler.Get("http://localhost/TaskManagerAPI/item/getall").Result);

            (DataContext as MainViewModel).Items.Clear();
            results.ForEach((DataContext as MainViewModel).Items.Add);
        }
    }
}
