using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReStatus.Models;


namespace ReStatusApi.Controllers
{
    [Route ("api/quotes")]
    [ApiController]
    public class quotesController : ControllerBase
    {
        private readonly elevatorsContext _context;

        public quotesController(elevatorsContext context)
        {
            _context = context;
        }

        // GET: api/quotes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<quotes>>> Getquotes()
        {
            return await _context.quotes.ToListAsync();
        }

        // GET: api/quotes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<quotes>> Getquotes(long id)
        {
            var quotes = await _context.quotes.FindAsync(id);

            if (quotes == null)
            {
                return NotFound();
            }

            return quotes;
        }
        [HttpGet("new")]
        public ActionResult<List<quotes>> Getall()
        {
            
            var qte_list = _context.quotes;
            if (qte_list == null) {
                return NotFound ("Not Found");
            }
            List<quotes> return_list = new List<quotes> ();
            List<quotes> quotes_list = new List<quotes> ();

            foreach (var quote in qte_list){
                quotes_list.Add (quote);
            }
            Int32 count = quotes_list.Count;
            //DateTime today = DateTime.Now;
            DateTime target_date = DateTime.Now.AddDays(-30);
            foreach (var quote in quotes_list){
                if(quote.created_at >= target_date){
                        return_list.Add (quote);
                }
            }
            return return_list;
        }
        // PUT: api/quotes/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> Putquotes(long id, quotes quotes)
        {
            if (id != quotes.id)
            {
                return BadRequest();
            }

            _context.Entry(quotes).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!quotesExists(id))
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

        // POST: api/quotes
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<quotes>> Postquotes(quotes quotes)
        {
            _context.quotes.Add(quotes);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Getquotes", new { id = quotes.id }, quotes);
        }

        // DELETE: api/quotes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<quotes>> Deletequotes(long id)
        {
            var quotes = await _context.quotes.FindAsync(id);
            if (quotes == null)
            {
                return NotFound();
            }

            _context.quotes.Remove(quotes);
            await _context.SaveChangesAsync();

            return quotes;
        }

        private bool quotesExists(long id)
        {
            return _context.quotes.Any(e => e.id == id);
        }
    }
}