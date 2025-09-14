using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mynew.Data;
using Mynew.Models;

namespace Syria_Minefeilds.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = "Firebase,Local")] // ✅ قبول التوكن من الاثنين
    public class MinefieldLocationsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public MinefieldLocationsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<MinefieldLocation>> GetMinefieldLocations()
            => Ok(_context.MinefieldLocations.ToList());

        [HttpGet("{id:int}")]
        public ActionResult<MinefieldLocation> GetMinefieldLocation(int id)
        {
            var m = _context.MinefieldLocations.Find(id);
            return m is null ? NotFound() : Ok(m);
        }

        [HttpPost]
        public IActionResult PostMinefieldLocation([FromBody] MinefieldLocation newLocation)
        {
            if (newLocation is null) return BadRequest("Invalid minefield location data.");

            _context.MinefieldLocations.Add(newLocation);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetMinefieldLocation), new { id = newLocation.Id }, newLocation);
        }
    }
}
