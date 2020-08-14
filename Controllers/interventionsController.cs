using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReStatus.Models;

namespace ReStatusApi.Controllers
{
    [Route ("api/interventions")]
    [ApiController]
    public class interventionsController : ControllerBase
    {
        private readonly elevatorsContext _context;

        public interventionsController(elevatorsContext context)
        {
            _context = context;
        }

        // Retriving the full interventions list                            
        // https://localhost:5001/api/interventions/all
        // GET: api/interventions/all
        [HttpGet("all")]
        public IEnumerable<interventions> Getinterventions()
        {
            return _context.interventions;
        }

        // To retrive a specify interventions by id                              
        // https://localhost:5001/api/interventions/2 
        // GET: api/interventions/(id)  
        [HttpGet("{id}")]
        public List<interventions> FindInterventionByID(long ID)
        {
            var intervention = _context.interventions.Where(inter => inter.id == ID).ToList();
            return intervention;
        }

        // GET: api/interventions/pending                               
        // https://localhost:5001/api/interventions/pending
        // Returns all fields of all Service Request records that do not have a start date and are in "Pending" status.
        [HttpGet("pending")]
        public List<interventions> getPending(string pending)
        {
            var interventions = _context.interventions.Where(interventions => interventions.Status == "pending" && interventions.InterventionStart == null ).ToList();
            return interventions;
        }


        // PUT: api/interventions/{id}/inProgress                      
        // https://localhost:5001/api/interventions/2/inProgress 
        // Change the status of the interventions request to "InProgress" and add a start date and time (Timestamp).
        [HttpPut("{id}/inProgress")]
        public async Task<ActionResult<interventions>> UpdateIntervention([FromRoute] long id)
        {
            var myIntervention = await this._context.interventions.FindAsync(id);
            if (myIntervention == null)
            {
                return NotFound();
            }
            else
            {
                myIntervention.Status = "pending";
                myIntervention.Status = "InProgress";
                myIntervention.InterventionStart = DateTime.Now;
                // string InterventionStart = DateTime.Now.ToString("yyyy/MM/dd H:mm:ss");
                // myIntervention.InterventionStart = InterventionStart;
            }
            this._context.interventions.Update(myIntervention);
            await this._context.SaveChangesAsync();

            return Content("The status of the intenvention ID: " + myIntervention.id +
            " has been changed to: " + myIntervention.Status + ". The start date is: "
             + myIntervention.InterventionStart + ".");
        }

        // PUT: api/interventions/{id}/completed                  
        // https://localhost:5001/api/interventions/2/completed
        //Change the status of the request for action to "Completed" and add an end date and time (Timestamp).
        [HttpPut("{id}/completed")]
        public async Task<ActionResult<interventions>> interventionCompleted([FromRoute] long id)
        {
            var myIntervention = await this._context.interventions.FindAsync(id);
            if (myIntervention == null)
            {
                return NotFound();
            }
            else
            {
                myIntervention.Result = "Complete";
                myIntervention.Status = "Completed";
                myIntervention.InterventionEnd = DateTime.Now;
            }
            this._context.interventions.Update(myIntervention);
            await this._context.SaveChangesAsync();

            return Content("The status of the intenvention ID: " + myIntervention.id +
            " has been changed to: " + myIntervention.Status + ". The end date is: "
             + myIntervention.InterventionEnd + ".");
        }

    }
}
