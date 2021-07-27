using System;
using System.Collections.Generic;
using System.Linq;
using TaskManagerAPI.Models;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;

namespace TaskManagerAPI
{
    public class ProductJsonConverter : JsonCreationConverter<Item>
    {
        protected override Item Create(Type objectType, JObject jObject)
        {
            if (jObject == null) throw new ArgumentNullException("jObject");

            if (jObject["IsCompleted"] != null || jObject["isCompleted"] != null)
            {
                return new TaskManagerAPI.Models.Task();
            }
            else if (jObject["Attendees"] != null || jObject["attendees"] != null)
            {
                return new Appointment();
            }
            else
            {
                return new Item();
            }
        }
    }
}
