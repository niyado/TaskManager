using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using TaskManager.Models;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace TaskManager.Dialogs
{
    public sealed partial class TaskDialog : ContentDialog
    {
        private IList<Item> items;
        public TaskDialog()
        {
            this.InitializeComponent();
         //   DueDatePicker.SelectedDate = DateTime.Now;

        }
        public TaskDialog(IList<Item> Items)
        {
            InitializeComponent();
            DataContext = new Task();
            this.items = Items;
         //   DueDatePicker.SelectedDate = DateTime.Now;
        }

        public TaskDialog(IList<Item> Items, Item itemToEdit)
        {
            InitializeComponent();
            DataContext = itemToEdit as Task;
            this.items = Items;
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            var itemOfInterest = DataContext as Task;
            var existingItem = items.FirstOrDefault(t => t.Id == itemOfInterest.Id);
            if (existingItem == null)
            {
                items.Add(DataContext as Task);
            }
            else
            {
                var index = items.IndexOf(existingItem);
                items.RemoveAt(index);
                items.Insert(index, itemOfInterest);
            }
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }
    }
}
