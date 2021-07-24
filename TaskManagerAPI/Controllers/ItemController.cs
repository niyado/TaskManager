using Microsoft.AspNetCore.Http;
using TaskManager.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        /// code to add or update appt or task here ///
        /// // TODO //
        /// 

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
    }
}
