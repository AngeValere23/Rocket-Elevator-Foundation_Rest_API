using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReStatus.Models;

namespace ReStatusApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class leadsController : ControllerBase
    {
        private readonly elevatorsContext _context;

        public leadsController(elevatorsContext context)
        {
            _context = context;
        }

        // GET: api/leads
        [HttpGet]
        public async Task<ActionResult<IEnumerable<leads>>> Getleads()
        {
            return await _context.leads.ToListAsync();
        }

        // GET: api/leads/5
        [HttpGet("{id}")]
        public async Task<ActionResult<leads>> Getleads(long id)
        {
            var leads = await _context.leads.FindAsync(id);

            if (leads == null)
            {
                return NotFound();
            }

            return leads;
        }

        // PUT: api/leads/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> Putleads(long id, leads leads)
        {
            if (id != leads.id)
            {
                return BadRequest();
            }

            _context.Entry(leads).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!leadsExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        private bool leadsExists(long id)
        {
            throw new NotImplementedException();
        }

        // POST: api/leads
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<leads>> Postleads(leads leads)
        {
            _context.leads.Add(leads);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Getleads", new { id = leads.id }, leads);
        }

        // DELETE: api/leads/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<leads>> Deleteleads(long id)
        {
            var leads = await _context.leads.FindAsync(id);
            if (leads == null)
            {
                return NotFound();
            }

            _context.leads.Remove(leads);
            await _context.SaveChangesAsync();

            return leads;
        }

        private bool leads(long id)
        {
            return _context.leads.Any(e => e.id == id);
        }
    }
}