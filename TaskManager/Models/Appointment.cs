using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Models
{
    public class Appointment : Item, INotifyPropertyChanged
    {
        
        public DateTimeOffset Start
        {
            get { return (DateTimeOffset)start; } 
            set { start = value.DateTime; }
        }

        public DateTimeOffset Stop
        {
            get { return (DateTimeOffset)stop; } 
            set { stop = value.DateTime; }
        }

        public string Attendees
        {
            get { return attendees; }
            set { attendees = value; }
        }

        private DateTime start;
        private DateTime stop;
        private string attendees;

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public override string ToString()
        {
            return $"{Priority} | Appointment: {Title} - {Description}\n\t{Start}-{Stop} With: {Attendees}";
        }

    }
}
