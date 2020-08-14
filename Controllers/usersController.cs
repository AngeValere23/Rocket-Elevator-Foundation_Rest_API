using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReStatus.Models;


namespace ReStatusApi.Controllers
{
    [Route ("api/users")]
    [ApiController]
    public class usersController : ControllerBase
    {
        private readonly elevatorsContext _context;

        public usersController(elevatorsContext context)
        {
            _context = context;
        }

        // GET: api/users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<users>>> Getusers()
        {
            return await _context.users.ToListAsync();
        }

        // GET: api/users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<users>> Getusers(long id)
        {
            var users = await _context.users.FindAsync(id);

            if (users == null)
            {
                return NotFound();
            }

            return users;
        }

        // GET: api/users/new
        [HttpGet("new")]
        public ActionResult<List<users>> Getall()
        {
            
            var use_list = _context.users;
            if (use_list == null) {
                return NotFound ("Not Found");
            }
            List<users> return_list = new List<users> ();
            List<users> users_list = new List<users> ();

            foreach (var user in use_list){
                users_list.Add (user);
            }
            Int32 count = users_list.Count;
            //DateTime today = DateTime.Now;
            DateTime target_date = DateTime.Now.AddDays(-30);
            foreach (var user in users_list){
                if(user.created_at >= target_date){
                        return_list.Add (user);
                }
            }
            return return_list;
        }
        // GET: api/users/null
        [HttpGet("null")]
        public ActionResult<List<users>> GetNull()
        {
            
            var use_list = _context.users;
            if (use_list == null) {
                return NotFound ("Not Found");
            }
            List<users> return_list = new List<users> ();
            List<users> users_list = new List<users> ();

            foreach (var user in use_list){
                users_list.Add (user);
            }
            Int32 count = users_list.Count;
            foreach (var user in users_list){
                if(user.reset_password_token == "null"){
                        return_list.Add (user);
                }
            }
            return return_list;
        }
        // PUT: api/users/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> Putusers(long id, users users)
        {
            if (id != users.id)
            {
                return BadRequest();
            }

            _context.Entry(users).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!usersExists(id))
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

        // POST: api/users
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<users>> Postusers(users users)
        {
            _context.users.Add(users);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Getusers", new { id = users.id }, users);
        }

        // DELETE: api/users/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<users>> Deleteusers(long id)
        {
            var users = await _context.users.FindAsync(id);
            if (users == null)
            {
                return NotFound();
            }

            _context.users.Remove(users);
            await _context.SaveChangesAsync();

            return users;
        }

        private bool usersExists(long id)
        {
            return _context.users.Any(e => e.id == id);
        }
    }
}