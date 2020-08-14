using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ReStatus.Models;
using Newtonsoft.Json.Linq;

namespace ReStatusApi.Controllers
{
    [Route ("api/columns")]
    [ApiController]
    public class columnsController : ControllerBase
    {
        private readonly elevatorsContext _context;

        public columnsController(elevatorsContext context)
        {
            _context = context;
        }

        // Retriving the full columns list 
        // https://localhost:5001/api/columns/all
        // GET api/columns/all :       
        [HttpGet("all")]
        public ActionResult<List<columns>> GetAll()
        {
            return _context.columns.ToList();
        }


        // GET api/columns/5
        [HttpGet("{id}")]
        public IActionResult GetById(string Status, long id)
        {
            var item = _context.columns.Find(id);
            if (item == null)
            {
                return NotFound("Not found");
            }
            var json = new JObject();
            json["Id"] = item.id;
            json["battery_id"] = item.battery_id;
            json["PropertyType"] = item.PropertyType;
            json["numberOfFLoor"] = item.NumberOfFLoor;
            json["Status"] = item.Status;
            return Content(json.ToString(), "application/json");
        }

        // PUT api/columns/5
        [HttpPut("{id}")]
        public IActionResult Update(long id, columns column)
        {
            var col = _context.columns.Find(id);
            if (col == null)
            {
                return NotFound();
            }

            col.Status = column.Status;

            _context.columns.Update(col);
            _context.SaveChanges();
            var json = new JObject();
            json["New Status"] = column.Status;
            json["message"] = "The Status of the Column Id: " + col.id + " has been changed to: " + col.Status;
            return Content(json.ToString(), "application/json");
        }

        // PUT: api/columns/{id}/inactive                     
        // https://localhost:5001/api/columns/2/inactive
        // Change the Status of the columns to inactive
        [HttpPut("{id}/inactive")]
        public async Task<ActionResult<columns>> UpdateColumnToInactive([FromRoute] long id)
        {
            var colStatus = await this._context.columns.FindAsync(id);
            if (colStatus == null)
            {
                return NotFound();
            }
            else
            {
                colStatus.Status = "inactive";

            }
            this._context.columns.Update(colStatus);
            await this._context.SaveChangesAsync();

            return Content("The Status of the elevator ID: " + colStatus.id +
            " has been changed to: " + colStatus.Status);
        }

        // PUT: api/columns/{id}/active                     
        // https://localhost:5001/api/columns/2/active
        // Change the Status of the columns to active
        [HttpPut("{id}/active")]
        public async Task<ActionResult<columns>> UpdateColumnToActive([FromRoute] long id)
        {
            var colStatus = await this._context.columns.FindAsync(id);
            if (colStatus == null)
            {
                return NotFound();
            }
            else
            {
                colStatus.Status = "active";

            }
            this._context.columns.Update(colStatus);
            await this._context.SaveChangesAsync();

            return Content("The Status of the elevator ID: " + colStatus.id +
            " has been changed to: " + colStatus.Status);
        }
    }
}
