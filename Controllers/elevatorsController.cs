using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReStatus.Models;
using Newtonsoft.Json.Linq;

namespace ReStatusApi.Controllers
{
    [Route ("api/elevators")]
    [ApiController]
    public class elevatorsController : ControllerBase
    {
        private readonly elevatorsContext _context;

        public elevatorsController(elevatorsContext context)
        {
            _context = context;
        }

        // Retriving the full elevators list 
        // https://localhost:5001/api/elevators/all
        // GET api/elevator/all :       
        [HttpGet("all")]
        public ActionResult<List<elevators>> GetAll()
        {
            return _context.elevators.ToList();
        }

        // Retriving Status of All the elevators not in operation            
        // https://localhost:5001/api/elevators/notinoperation
        // GET: api/elevators/notinoperation            

        [HttpGet("notinoperation")]
        public IEnumerable<elevators> GetInactiveElevators()
        {
            IQueryable<elevators> elevators = from list_elev in _context.elevators
                                             where list_elev.Status != "active"
                                             select list_elev;

            return elevators.ToList();
        }


        // Retriving Status of All the elevators active             
        // https://localhost:5001/api/elevators/active
        // GET: api/elevators/active            

        [HttpGet("active")]
        public IEnumerable<elevators> GetActiveElevators()
        {
            IQueryable<elevators> elevators = from list_elev in _context.elevators
                                             where list_elev.Status == "active"
                                             select list_elev;

            return elevators.ToList();
        }



        // Retriving Status of a specific Elevator                      
        // https://localhost:5001/api/elevators/4
        // GET: api/elevator/4            
        [HttpGet("{id}")]
        public async Task<ActionResult<elevators>> GetElevatorsId(long id)
        {
            var elev = await _context.elevators.FindAsync(id);

            if (elev == null)
            {
                return NotFound();
            }

            return elev;
        }

        // Changing Status of a specific Elevator                      
        // https://localhost:5001/api/elevators/4   
        // PUT: api/elevator/4             
        [HttpPut("{id}")]
        public IActionResult Update(long id, elevators elevator)
        {
            var elev = _context.elevators.Find(id);
            if (elev == null)
            {
                return NotFound();
            }

            elev.Status = elevator.Status;

            _context.elevators.Update(elev);
            _context.SaveChanges();
            var json = new JObject();
            json["New Status"] = elevator.Status;
            json["message"] = "The Status of the Elevator Id: " + elev.id + " has been changed to: " + elev.Status;
            return Content(json.ToString(), "application/json");
        }


        // PUT: api/elevator/{id}/inactive                     
        // https://localhost:5001/api/elevators/2/inactive
        // Change the Status of the elevator to inactive
        [HttpPut("{id}/inactive")]
        public async Task<ActionResult<elevators>> UpdateElevatortoInactive([FromRoute] long id)
        {
            var elevStatus = await this._context.elevators.FindAsync(id);
            if (elevStatus == null)
            {
                return NotFound();
            }
            else
            {
                elevStatus.Status = "inactive";

            }
            this._context.elevators.Update(elevStatus);
            await this._context.SaveChangesAsync();

            return Content("The Status of the elevator ID: " + elevStatus.id +
            " has been changed to: " + elevStatus.Status);
        }

        // PUT: api/elevator/{id}/active                     
        // https://localhost:5001/api/elevators/2/active
        // Change the Status of the elevator to active
        [HttpPut("{id}/active")]
        public async Task<ActionResult<elevators>> UpdateElevatorToActive([FromRoute] long id)
        {
            var elevStatus = await this._context.elevators.FindAsync(id);
            if (elevStatus == null)
            {
                return NotFound();
            }
            else
            {
                elevStatus.Status = "active";

            }
            this._context.elevators.Update(elevStatus);
            await this._context.SaveChangesAsync();

            return Content("The Status of the elevator ID: " + elevStatus.id +
            " has been changed to: " + elevStatus.Status);
        }


    }
}
