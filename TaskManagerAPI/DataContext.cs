using TaskManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TaskManagerAPI
{
    public class DataContext
    {
        public static List<Item> Items = new List<Item>
        {
            new Appointment{ Title = "Daily Standup", Description = "Just go."},
            new Task{ Title = "Feed the dog", Description = "He is hungry", IsCompleted = true},
            new Appointment{ Title = "Daily Standup2", Description = "Just go."},
            new Task{ Title = "Feed the dog2", Description = "He is hungry", IsCompleted = true},
            new Appointment{ Title = "Daily Standup3", Description = "Just go."},
            new Task{ Title = "Feed the dog3", Description = "He is hungry", IsCompleted = false}
        };
    }
}
