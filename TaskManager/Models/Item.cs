using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Microsoft.VisualBasic.CompilerServices;

namespace TaskManager.Models
{
   [JsonConverter(typeof(ProductJsonConverter))]
    public class Item : INotifyPropertyChanged

    {
        public Item() { Id = Guid.NewGuid(); }
        //public Item(string v1, string v2) { Title = v1; Description = v2; }
        public string Title
        {
            get;set;
        }
        public string Description
        {
            get;set;
        }
        //1 for high, 2 for medium, 3 for low
        public string Priority
        {
            get;set;
        }

        public Guid Id { get; set; }

        public override string ToString()
        {
            return $"{Priority} | {Title} - {Description}";
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
