using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using TaskManager.Models;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace TaskManager.Dialogs
{
    public sealed partial class AppointmentDialog : ContentDialog
    {
        private IList<Item> items;
        public AppointmentDialog(IList<Item> Items)
        {
            InitializeComponent();
            DataContext = new Appointment();
            this.items = Items;
        }

        public AppointmentDialog(IList<Item> Items, Item itemToEdit)
        {
            InitializeComponent();
            DataContext = itemToEdit as Appointment;
            this.items = Items;
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            var itemOfInterest = DataContext as Appointment;
            var existingItem = items.FirstOrDefault(t => t.Id == itemOfInterest.Id);
            if (existingItem == null)
            {
                items.Add(DataContext as Appointment);
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
