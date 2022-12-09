using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace MyApi.Controllers
{
    [Route("api/[controller]")]
[ApiController]
public class HeroesController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public HeroesController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: api/Heroes
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Hero>>> GetHeroes()
    {
        return await _context.Heroes.ToListAsync();
    }

    // GET: api/Heroes/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Hero>> GetHero(int id)
    {
        var hero = await _context.Heroes.FindAsync(id);

        if (hero == null)
        {
            return NotFound();
        }

        return hero;
    }

    // POST: api/Heroes
    [HttpPost]
    public async Task<ActionResult<Hero>> PostHero(Hero hero)
    {
        _context.Heroes.Add(hero);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetHero", new { id = hero.Id }, hero);
    }

    // PUT: api/Heroes/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutHero(int id, Hero hero)
    {
        if (id != hero.Id)
        {
            return BadRequest();
        }

        _context.Entry(hero).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!HeroExists(id))
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

    // DELETE: api/Heroes/5
    [HttpDelete("{id}")]
    public async Task<ActionResult<Hero>> DeleteHero(int id)
    {
        var hero = await _context.Heroes.FindAsync(id);
        if (hero == null)
        {
            return NotFound();
        }

        _context.Heroes.Remove(hero);
        await _context.SaveChangesAsync();

        return hero;
    }

    private bool HeroExists(int id)
    {
        return _context.Heroes.Any(e => e.Id == id);
    }
}






}