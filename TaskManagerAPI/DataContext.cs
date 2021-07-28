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

            var setter = client.Set("items/" + Items[0].Id, Items[0]);
            setter = client.Set("items/" + Items[1].Id, Items[1]);
            setter = client.Set("items/" + Items[2].Id, Items[2]);
            setter = client.Set("items/" + Items[3].Id, Items[3]);
            setter = client.Set("items/" + Items[4].Id, Items[4]);
        }

        public static async void Set(Item item)
        {
            IFirebaseClient client = new FireSharp.FirebaseClient(config);

            var setter = client.Set("items/" + item.Id, item);
        }
    }
}
