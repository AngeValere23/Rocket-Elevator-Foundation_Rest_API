using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ReStatus.Models;
using Newtonsoft.Json.Linq;


namespace ReStatusApi.Controllers
{
    [Route ("api/batteries")]
    [ApiController]
    public class batteriesController : ControllerBase
    {
        private readonly elevatorsContext _context;

        public batteriesController(elevatorsContext context)
        {
            _context = context;
        }

        // Retriving the full batteries list 
        // https://localhost:5001/api/batteries/all
        // GET api/batteries/all :       
        [HttpGet("all")]
        public ActionResult<List<batteries>> GetAll()
        {
            return _context.batteries.ToList();
        }


        // GET api/batteries/5
        [HttpGet("{id}")]
        public IActionResult GetById(string Status, long id)
        {
            var item = _context.batteries.Find(id);
            if (item == null)
            {
                return NotFound("Not found");
            }
            var json = new JObject();
            json["Id"] = item.id;
            json["building_id"] = item.building_id;
            json["PropertyType"] = item.PropertyType;
            json["Status"] = item.Status;
            json["employee_id"] = item.employee_id;
            return Content(json.ToString(), "application/json");
        }

        // PUT api/batteries/5
        [HttpPut("{id}")]
        public IActionResult Update(long id, batteries battery)
        {
            var bat = _context.batteries.Find(id);
            if (bat == null)
            {
                return NotFound();
            }

            bat.Status = battery.Status;

            _context.batteries.Update(bat);
            _context.SaveChanges();
            var json = new JObject();
            json["New Status"] = battery.Status;
            json["message"] = "The Status of the Battery Id: " + bat.id + " has been changed to: " + bat.Status;
            return Content(json.ToString(), "application/json");
        }

        // PUT: api/batteries/{id}/inactive                     
        // https://localhost:5001/api/batteries/2/inactive
        // Change the Status of the batteries to inactive
        [HttpPut("{id}/inactive")]
        public async Task<ActionResult<batteries>> UpdateBatteryToInactive([FromRoute] long id)
        {
            var batStatus = await this._context.batteries.FindAsync(id);
            if (batStatus == null)
            {
                return NotFound();
            }
            else
            {
                batStatus.Status = "inactive";

            }
            this._context.batteries.Update(batStatus);
            await this._context.SaveChangesAsync();

            return Content("The Status of the elevator ID: " + batStatus.id +
            " has been changed to: " + batStatus.Status);
        }

        // PUT: api/batteries/{id}/active                     
        // https://localhost:5001/api/batteries/2/active
        // Change the Status of the batteries to active
        [HttpPut("{id}/active")]
        public async Task<ActionResult<batteries>> UpdateBatteryToActive([FromRoute] long id)
        {
            var batStatus = await this._context.batteries.FindAsync(id);
            if (batStatus == null)
            {
                return NotFound();
            }
            else
            {
                batStatus.Status = "active";

            }
            this._context.batteries.Update(batStatus);
            await this._context.SaveChangesAsync();

            return Content("The Status of the elevator ID: " + batStatus.id +
            " has been changed to: " + batStatus.Status);
        }

    }
}
