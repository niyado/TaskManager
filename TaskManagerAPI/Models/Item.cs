using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Runtime.CompilerServices;

namespace TaskManagerAPI.Models
{
    [JsonConverter(typeof(ProductJsonConverter))]
    public class Item

    {
        public Item() { Id = Guid.NewGuid(); }
        //public Item(string v1, string v2) { Title = v1; Description = v2; }
        public string Title
        {
            get; set;
        }
        public string Description
        {
            get; set;
        }
        //1 for high, 2 for medium, 3 for low
        public string Priority
        {
            get; set;
        }

        public Guid Id { get; set; }

        public override string ToString()
        {
            return $"{Priority} | {Title} - {Description}";
        }

        //public event PropertyChangedEventHandler PropertyChanged;
        //private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        //{
        //    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        //}
    }
}
