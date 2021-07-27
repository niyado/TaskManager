using TaskManagerAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TaskManagerAPI
{
    public class DataContext
    {
        public static List<Item> Items = new List<Item>
        {
            new Appointment{ Title = "Daily Standup", Description = "Just go.", Attendees = "simba and I", Priority = "2", Id = Guid.NewGuid()},
            new Task{ Title = "Feed the dog", Description = "it is about time", IsCompleted = true, Priority = "1", Id = Guid.NewGuid()},
            new Appointment{ Title = "Daily Standup2", Description = "Just go.", Attendees = "simba and I", Priority = "1", Id = Guid.NewGuid()},
            new Task{ Title = "Feed the dog2", Description = "could use a walk too", IsCompleted = true, Priority = "2", Id = Guid.NewGuid()},
            new Appointment{ Title = "Daily Standup3", Description = "Just go.", Attendees = "simba and I", Priority = "3", Id = Guid.NewGuid()},
            new Task{ Title = "Feed the dog3", Description = "please", IsCompleted = false, Priority = "3", Id = Guid.NewGuid()}
        };
    }
}
