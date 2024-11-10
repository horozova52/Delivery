using Delivery.Shared;
using Delivery.Server.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Delivery.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressesController : ControllerBase
    {
        private readonly CourierAppContext _context;
        private readonly ILogger<AddressesController> _logger;

        public AddressesController(CourierAppContext context, ILogger<AddressesController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Address>>> GetAddresses()
        {
            _logger.LogInformation("Fetching all addresses");
            return await _context.Addresses.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Address>> GetAddress(int id)
        {
            _logger.LogInformation("Fetching address with ID: {Id}", id);
            var address = await _context.Addresses.FindAsync(id);
            if (address == null)
            {
                _logger.LogWarning("Address with ID {Id} not found", id);
                return NotFound();
            }
            return address;
        }

        [HttpPost]
        public async Task<ActionResult<Address>> PostAddress(Address address)
        {
            _logger.LogInformation("Creating a new address");
            try
            {
                _context.Addresses.Add(address);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Address created successfully with ID: {Id}", address.Id);
                return CreatedAtAction("GetAddress", new { id = address.Id }, address);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating address");
                return StatusCode(500, "An error occurred while creating the address.");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAddress(int id, [FromBody] Address address)
        {
            if (id != address.Id)
            {
                return BadRequest();
            }

            _context.Entry(address).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Addresses.Any(e => e.Id == id))
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAddress(int id)
        {
            _logger.LogInformation("Deleting address with ID: {Id}", id);
            var address = await _context.Addresses.FindAsync(id);
            if (address == null)
            {
                _logger.LogWarning("Address with ID {Id} not found for deletion", id);
                return NotFound();
            }

            _context.Addresses.Remove(address);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Address with ID {Id} deleted successfully", id);

            return NoContent();
        }
    }
}
