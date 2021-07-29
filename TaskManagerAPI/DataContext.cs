using TaskManagerAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using FireSharp.Serialization;

namespace TaskManagerAPI
{
    public class DataContext
    {
        private static IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "noFZ0x8N3IdC2i31HHMLdaCqRQz3fetoYoCcZBVe",
            BasePath = "https://taskmanager-ba2cd-default-rtdb.firebaseio.com/"
        };


        //for testing purposes only, not used in the client application
        public static List<Item> Items = new List<Item>
        {
            new Appointment{ Title = "Daily Standup", Description = "Just go.", Attendees = "simba and I", Priority = "2", Id = Guid.NewGuid()},
            new Task{ Title = "Feed the dog", Description = "it is about time", IsCompleted = true, Priority = "1", Id = Guid.NewGuid()},
            new Appointment{ Title = "Daily Standup2", Description = "Just go.", Attendees = "simba and I", Priority = "1", Id = Guid.NewGuid()},
            new Task{ Title = "Feed the dog2", Description = "could use a walk too", IsCompleted = true, Priority = "2", Id = Guid.NewGuid()},
            new Appointment{ Title = "Daily Standup3", Description = "Just go.", Attendees = "simba and I", Priority = "3", Id = Guid.NewGuid()},
            new Task{ Title = "Feed the dog3", Description = "please", IsCompleted = false, Priority = "3", Id = Guid.NewGuid()}
        };

        //for testing purposes only, uses the Items list above
        public static async void SetTest()
        {
            IFirebaseClient client = new FireSharp.FirebaseClient(config);

            foreach(var item in Items){
                Set(item);
            }
        }

        public static Item Set(Item item)
        {
            IFirebaseClient client = new FireSharp.FirebaseClient(config);

            if (item is Task)
            {
                var setter = client.Set("items/task/" + item.Id, item as Task);
                return setter.ResultAs<Task>();
            }
            else if(item is Appointment)
            {
                var setter = client.Set("items/appt/" + item.Id, item as Appointment);
                return setter.ResultAs<Appointment>();
            }
            else
            {
                var setter = client.Set("items/" + item.Id, item);
            }
            return null;
        }

        public static List<Item> GetAll()
        {
            IFirebaseClient client = new FireSharp.FirebaseClient(config);

            List<Item> items = new List<Item>();
            FirebaseResponse response = client.Get("items/task");
            Dictionary<string, Task> data = response.ResultAs<Dictionary<string, Task>>();

            if (data != null)
            {
                foreach (KeyValuePair<string, Task> entry in data)
                {
                    items.Add(entry.Value as Task);
                }
            }

            FirebaseResponse response2 = client.Get("items/appt");
            Dictionary<string, Appointment> data2 = response2.ResultAs<Dictionary<string, Appointment>>();

            if (data2 != null)
            {
                foreach (KeyValuePair<string, Appointment> entry in data2)
                {
                    items.Add(entry.Value as Appointment);
                }
            }
            return items;
        }

        public static Item Remove(Item item)
        {
            IFirebaseClient client = new FireSharp.FirebaseClient(config);
            if (item is Task)
            {
                FirebaseResponse response = client.Delete("items/task/" + item.Id);
                return response.ResultAs<Task>();
            }
            else if(item is Appointment)
            {
                FirebaseResponse response = client.Delete("items/appt/" + item.Id);
                return response.ResultAs<Appointment>();
            }
            else
            {
                FirebaseResponse response = client.Delete("items/task/" + item.Id);
                if(response == null)
                {
                    response = client.Delete("items/appt/" + item.Id);
                }
                return response.ResultAs<Item>();
            }
        }
    }
}
