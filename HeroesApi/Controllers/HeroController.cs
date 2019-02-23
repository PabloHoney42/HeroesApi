using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HeroesApi.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HeroesApi.Controllers
{
    [Route("api/hero")]
    [ApiController]
    public class HeroController : ControllerBase
    {
        private readonly HeroContext _context;

        public HeroController(HeroContext context)
        {
            this._context = context;

            // inserindo dados na database
            if (_context.Heroes.Count() == 0)
            {
                _context.Heroes.Add(new Hero { Name = "Mr. Nice" });
                _context.Heroes.Add(new Hero { Name = "Narco" });
                _context.Heroes.Add(new Hero { Name = "Bombasto" });
                _context.Heroes.Add(new Hero { Name = "Celeritas" });
                _context.Heroes.Add(new Hero { Name = "Magneta" });
                _context.Heroes.Add(new Hero { Name = "RubberMan" });
                _context.Heroes.Add(new Hero { Name = "Dynama" });
                _context.Heroes.Add(new Hero { Name = "Dr IQ" });
                _context.Heroes.Add(new Hero { Name = "Magma" });
                _context.Heroes.Add(new Hero { Name = "Tornado" });
                _context.SaveChanges();
            }
        }

        // GET: api/hero
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Hero>>> GetHeroes()
        {
            return await _context.Heroes.ToListAsync();
        }

        // GET api/hero/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Hero>> GetHero(long id)
        {
            var hero = await _context.Heroes.FindAsync(id);

            if (hero == null)
            {
                return NotFound();
            }

            return hero;
        }

        // POST: api/hero
        [HttpPost]
        public async Task<ActionResult<Hero>> PostHero(Hero item)
        {
            _context.Heroes.Add(item);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetHero), new { id = item.Id }, item);
        }

        // PUT: api/hero/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHero(long id, Hero item)
        {
            if (id != item.Id)
            {
                return BadRequest();
            }

            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/hero/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHero(long id)
        {
            var item = await _context.Heroes.FindAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            _context.Heroes.Remove(item);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
