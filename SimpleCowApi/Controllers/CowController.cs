using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleCowApi.Data;
using SimpleCowApi.Data.Models;

namespace SimpleCowApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CowsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CowsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Get all cows
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cow>>> Get()
        {
            return Ok(await _context.Cows.Include(c => c.Farm).ToListAsync());
        }

        // Get a cow by ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Cow>> Get(int id)
        {
            var cow = await _context.Cows.Include(c => c.Farm).FirstOrDefaultAsync(c => c.Id == id);
            if (cow == null)
            {
                return NotFound();
            }
            return Ok(cow);
        }

        // Create a new cow
        [HttpPost]
        public async Task<ActionResult<Cow>> Post([FromBody] Cow cow)
        {
            if (cow.FarmId != null)
            {
                var farm = await _context.Farms.FindAsync(cow.FarmId);
                if (farm == null)
                {
                    return BadRequest("Farm ID does not exist.");
                }
            }

            _context.Cows.Add(cow);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = cow.Id }, cow);
        }

        // Add a cow to a farm
        [HttpPost("{id}/addToFarm/{farmId}")]
        public async Task<ActionResult> AddCowToFarm(int id, int farmId)
        {
            var cow = await _context.Cows.FindAsync(id);
            if (cow == null)
            {
                return NotFound();
            }

            var farm = await _context.Farms.FindAsync(farmId);
            if (farm == null)
            {
                return BadRequest("Farm ID does not exist.");
            }

            cow.FarmId = farmId;
            await _context.SaveChangesAsync();

            return Ok(cow);
        }

        // Remove a cow from a farm
        [HttpPost("{id}/removeFromFarm")]
        public async Task<ActionResult> RemoveCowFromFarm(int id)
        {
            var cow = await _context.Cows.FindAsync(id);
            if (cow == null)
            {
                return NotFound();
            }

            cow.FarmId = null; // Remove cow from farm
            await _context.SaveChangesAsync();

            return Ok(cow);
        }
    }
}
