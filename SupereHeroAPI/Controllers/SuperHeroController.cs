using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SupereHeroAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {
        private static List<SuperHero> heroes = new List<SuperHero> {};

        private readonly DataContext _context;

        public SuperHeroController(DataContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<List<SuperHero>>> Get()
        {
            return Ok(await _context.SuperHeroes.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<SuperHero>>> Get(int id)
        {
            var hero = await _context.SuperHeroes.FindAsync(id);
            if (hero == null)
            {
                return BadRequest("Hero not found");
            }
            return Ok(hero);
        }

        [HttpPost]
        public async Task<ActionResult<List<SuperHero>>> AddHero(SuperHero hero)
        {
            _context.SuperHeroes.Add(hero);
            await _context.SaveChangesAsync();
            return Ok(await _context.SuperHeroes.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<SuperHero>>> EditHero(SuperHero heroRequest)
        {
            var dBhero = await _context.SuperHeroes.FindAsync(heroRequest.Id);
            if (dBhero == null)
            {
                return BadRequest("Hero not found");
            }
            dBhero.Name = heroRequest.Name;
            dBhero.FirstName = heroRequest.FirstName;
            dBhero.LastName = heroRequest.LastName;
            dBhero.Place = heroRequest.Place;

            await _context.SaveChangesAsync();

            return Ok(dBhero);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<List<SuperHero>>> Delete(int id)
        {
            var dBhero = await _context.SuperHeroes.FindAsync(id);
            if (dBhero == null)
            {
                return BadRequest("Hero not found");
            }
            _context.SuperHeroes.Remove(dBhero);

            await _context.SaveChangesAsync();

            return Ok(await _context.SuperHeroes.ToListAsync());
        }
    }
}
