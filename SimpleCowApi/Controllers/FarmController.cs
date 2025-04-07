using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleCowApi.Data;
using SimpleCowApi.Data.Models;

namespace SimpleCowApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FarmsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public FarmsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Get all farms
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Farm>>> Get()
        {
            return Ok(await _context.Farms.ToListAsync());
        }

        // Get a farm by ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Farm>> Get(int id)
        {
            var farm = await _context.Farms.FindAsync(id);
            if (farm == null)
            {
                return NotFound();
            }
            return Ok(farm);
        }

        // Create a new farm
        [HttpPost]
        public async Task<ActionResult<Farm>> Post([FromBody] Farm farm)
        {
            _context.Farms.Add(farm);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = farm.Id }, farm);
        }
    }
}
