using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManagerAPI.Models
{
    public class Task : Item
    {

        private DateTime due;
        private bool isCompleted;
        public DateTimeOffset Due
        {
            get { return (DateTimeOffset)due; }
            set { due = value.DateTime; }
        }
        public bool IsCompleted
        {
            get { return isCompleted; }
            set { isCompleted = value; }
        }

        public override string ToString()
        {
            return $"{Priority} | Task: {Title} - {Description}\n\t{Due} - Completed: {IsCompleted}";
        }

        //public event PropertyChangedEventHandler PropertyChanged;
        //private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        //{
        //    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        //}

    }
}