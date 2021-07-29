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
            //return Ok(DataContext.Items);
            //DataContext.SetTest();
            return Ok(DataContext.GetAll());
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
                //DataContext.Items.Add(task as Task);
                task = DataContext.Set(task as Task) as Task;
            }
            else
            {
                //var ticketToSync = DataContext.Items.FirstOrDefault(t => t.Id.Equals(task.Id));
                //var index = DataContext.Items.IndexOf(ticketToSync);
                //DataContext.Items.RemoveAt(index);
                //DataContext.Items.Insert(index, task);
                DataContext.Remove(task);
                task = DataContext.Set(task as Task) as Task ;
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
                // DataContext.Items.Add(appt as Appointment);
                appt = DataContext.Set(appt as Appointment) as Appointment;
            }
            else
            {
                //var ticketToSync = DataContext.Items.FirstOrDefault(t => t.Id.Equals(appt.Id));
                //var index = DataContext.Items.IndexOf(ticketToSync);
                //DataContext.Items.RemoveAt(index);
                //DataContext.Items.Insert(index, appt);
                DataContext.Remove(appt);
                appt = DataContext.Set(appt as Appointment) as Appointment;


            }

            return Ok(appt);
        }

        [HttpPost("Delete")]
        public ActionResult<Item> Delete([FromBody] Item item)
        {
            //var itemToRemove = DataContext.Items.FirstOrDefault(t => t.Id.Equals(id));
            //if (itemToRemove?.Id != Guid.Empty)
            //{
            //    DataContext.Items.Remove(itemToRemove);
            //}
            var itemToRemove = DataContext.Remove(item);

            return itemToRemove;
        }

        [HttpPost("Search")]
        public ActionResult<List<Item>> Search([FromBody] String keyword)
        {
            List<Item> items = DataContext.GetAll();
            var results = items.Where(item => (item.Title.Contains(keyword, StringComparison.InvariantCultureIgnoreCase)) ||
                                             (item.Description.Contains(keyword, StringComparison.InvariantCultureIgnoreCase)) ||
                                             ((item as Appointment)?.Attendees?.Contains(keyword, StringComparison.InvariantCultureIgnoreCase) ?? false)).ToList(); 
            

            return results;
        }

        [HttpPost("SortByCompletion")]
        public ActionResult<List<Item>> SortByCompletion([FromBody] bool isCompleted)
        {
            List<Item> items = DataContext.GetAll();
            var results = items.Where(item => (item as TaskManagerAPI.Models.Task)?.IsCompleted == isCompleted).ToList(); 


            return results;
        }
    }
}
