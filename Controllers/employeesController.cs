using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReStatus.Models;


namespace ReStatusApi.Controllers
{
    [Route ("api/employees")]
    [ApiController]
    public class employeesController : ControllerBase
    {
        private readonly elevatorsContext _context;

        public employeesController(elevatorsContext context)
        {
            _context = context;
        }

        // GET: api/employees
        [HttpGet]
        public async Task<ActionResult<IEnumerable<employees>>> Getemployees()
        {
            return await _context.employees.ToListAsync();
        }

        // GET: api/employees/5
        [HttpGet("{id}")]
        public async Task<ActionResult<employees>> Getemployees(long id)
        {
            var employees = await _context.employees.FindAsync(id);

            if (employees == null)
            {
                return NotFound();
            }

            return employees;
        }
        [HttpGet("new")]
        public ActionResult<List<employees>> Getall()
        {
            
            var emp_list = _context.employees;
            if (emp_list == null) {
                return NotFound ("Not Found");
            }
            List<employees> return_list = new List<employees> ();
            List<employees> employees_list = new List<employees> ();

            foreach (var employee in emp_list){
                employees_list.Add (employee);
            }
            Int32 count = employees_list.Count;
            //DateTime today = DateTime.Now;
            DateTime target_date = DateTime.Now.AddDays(-30);
            foreach (var employee in employees_list){
                if(employee.created_at >= target_date){
                        return_list.Add (employee);
                }
            }
            return return_list;
        }
        // PUT: api/employees/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> Putemployees(long id, employees employees)
        {
            if (id != employees.id)
            {
                return BadRequest();
            }

            _context.Entry(employees).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!employeesExists(id))
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

        // POST: api/employees
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<employees>> Postemployees(employees employees)
        {
            _context.employees.Add(employees);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Getemployees", new { id = employees.id }, employees);
        }

        // DELETE: api/employees/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<employees>> Deleteemployees(long id)
        {
            var employees = await _context.employees.FindAsync(id);
            if (employees == null)
            {
                return NotFound();
            }

            _context.employees.Remove(employees);
            await _context.SaveChangesAsync();

            return employees;
        }

        private bool employeesExists(long id)
        {
            return _context.employees.Any(e => e.id == id);
        }
    }
}