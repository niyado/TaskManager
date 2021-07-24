﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using TaskManager.Models;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public Item SelectedItem { get; set; }
        public ObservableCollection<Item> Items { get; set; }
        public ObservableCollection<Item> SortedItems { get; set; }

        public MainViewModel()
        {
            Items = new ObservableCollection<Item>();
            SortedItems = new ObservableCollection<Item>();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void Remove()
        {
            Items.Remove(SelectedItem);
        }


        public void Sort()
        {
            Items.Sort(o => o.Priority);
        }

        public void SortByCompletion(bool isComplete)
        {
            SortedItems = new ObservableCollection<Item>();
            foreach(Item item in Items)
            {
                if(item is Models.Task && (item as Models.Task).IsCompleted && isComplete)
                {
                    SortedItems.Add(item);
                }
                else if(item is Models.Task && !(item as Models.Task).IsCompleted && !isComplete)
                {
                    SortedItems.Add(item);
                }

            }
        }
    }

    static class Extensions
    {
        public static void Sort<TSource, TKey>(this ObservableCollection<TSource> collection, Func<TSource, TKey> keySelector)
        {
            List<TSource> sorted = collection.OrderBy(keySelector).ToList();
            for (int i = 0; i < sorted.Count(); i++)
                collection.Move(collection.IndexOf(sorted[i]), i);
        }
    }
}
