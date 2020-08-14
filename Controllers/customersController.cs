using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReStatus.Models;


namespace ReStatusApi.Controllers
{
    [Route ("api/customers")]
    [ApiController]
    public class customersController : ControllerBase
    {
        private readonly elevatorsContext _context;

        public customersController(elevatorsContext context)
        {
            _context = context;
        }

        // GET: api/customers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<customers>>> Getcustomers()
        {
            return await _context.customers.ToListAsync();
        }

        // GET: api/customers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<customers>> Getcustomers(long id)
        {
            var customers = await _context.customers.FindAsync(id);

            if (customers == null)
            {
                return NotFound();
            }

            return customers;
        }
        [HttpGet("new")]
        public ActionResult<List<customers>> Getall()
        {
            
            var cust_list = _context.customers;
            if (cust_list == null) {
                return NotFound ("Not Found");
            }
            List<customers> return_list = new List<customers> ();
            List<customers> customers_list = new List<customers> ();

            foreach (var customer in cust_list){
                customers_list.Add (customer);
            }
            Int32 count = customers_list.Count;
            //DateTime today = DateTime.Now;
            DateTime target_date = DateTime.Now.AddDays(-30);
            foreach (var customer in customers_list){
                if(customer.created_at >= target_date){
                        return_list.Add (customer);
                }
            }
            return return_list;
        }
        // PUT: api/customers/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> Putcustomers(long id, customers customers)
        {
            if (id != customers.id)
            {
                return BadRequest();
            }

            _context.Entry(customers).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!customersExists(id))
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

        // POST: api/customers
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<customers>> Postcustomers(customers customers)
        {
            _context.customers.Add(customers);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Getcustomers", new { id = customers.id }, customers);
        }

        // DELETE: api/customers/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<customers>> Deletecustomers(long id)
        {
            var customers = await _context.customers.FindAsync(id);
            if (customers == null)
            {
                return NotFound();
            }

            _context.customers.Remove(customers);
            await _context.SaveChangesAsync();

            return customers;
        }

        private bool customersExists(long id)
        {
            return _context.customers.Any(e => e.id == id);
        }
    }
}