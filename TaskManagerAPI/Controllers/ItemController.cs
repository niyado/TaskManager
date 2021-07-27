using Microsoft.AspNetCore.Http;
using TaskManagerAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Task = TaskManagerAPI.Models.Task;

namespace TaskManagerAPI.Controllers
{
    [ApiController]
    [Route("Item")]
    public class ItemController : ControllerBase
    {
        [HttpGet("test")]
        public string Test()
        {
            return "Hello, World!";
        }

        [HttpGet("GetAll")]
        public ActionResult<List<Item>> Get()
        {
            return Ok(DataContext.Items);
        }

        [HttpPost("AddOrUpdateTask")]
        public ActionResult<Task> AddOrUpdateTask([FromBody] Task task)
        {
            if (task == null)
            {
                return BadRequest();
            }

            if (task.Id == Guid.Empty)
            {
                DataContext.Items.Add(task as Task);
            }
            else
            {
                var ticketToSync = DataContext.Items.FirstOrDefault(t => t.Id.Equals(task.Id));
                var index = DataContext.Items.IndexOf(ticketToSync);
                DataContext.Items.RemoveAt(index);
                DataContext.Items.Insert(index, task);
            }


            return Ok(task);
        }

        [HttpPost("AddOrUpdateAppointment")]
        public ActionResult<Appointment> AddOrUpdateAppointment([FromBody] Appointment appt)
        {
            if (appt == null)
            {
                return BadRequest();
            }

            if (appt.Id == Guid.Empty)
            {
                DataContext.Items.Add(appt as Appointment);
            }
            else
            {
                var ticketToSync = DataContext.Items.FirstOrDefault(t => t.Id.Equals(appt.Id));
                var index = DataContext.Items.IndexOf(ticketToSync);
                DataContext.Items.RemoveAt(index);
                DataContext.Items.Insert(index, appt);


            }

            return Ok(appt);
        }

        [HttpPost("Delete")]
        public ActionResult<Item> Delete([FromBody] Guid id)
        {
            var itemToRemove = DataContext.Items.FirstOrDefault(t => t.Id.Equals(id));
            if (itemToRemove?.Id != Guid.Empty)
            {
                DataContext.Items.Remove(itemToRemove);
            }

            return itemToRemove;
        }

        [HttpPost("Search")]
        public ActionResult<List<Item>> Search([FromBody] String keyword)
        {
            var results = DataContext.Items.Where(item => (item.Title.Contains(keyword, StringComparison.InvariantCultureIgnoreCase)) ||
                                             (item.Description.Contains(keyword, StringComparison.InvariantCultureIgnoreCase)) ||
                                             ((item as Appointment)?.Attendees?.Contains(keyword, StringComparison.InvariantCultureIgnoreCase) ?? false)).ToList(); 
            

            return results;
        }

        [HttpPost("SortByCompletion")]
        public ActionResult<List<Item>> SortByCompletion([FromBody] bool isCompleted)
        {
            var results = DataContext.Items.Where(item => (item as TaskManagerAPI.Models.Task)?.IsCompleted == isCompleted).ToList(); 


            return results;
        }
    }
}
