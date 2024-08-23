using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using api.net5.Context;
using System;

namespace api.net5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UserController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("me")]
        public async Task<IActionResult> GetCurrentUser()
        {
            try
            {
                var email = User.Identity?.Name;

                if (string.IsNullOrEmpty(email))
                {
                    return BadRequest(new { message = "User not found in the token." });
                }

                var user = await _context.registrationModels
                    .SingleOrDefaultAsync(u => u.Email == email);

                if (user == null)
                {
                    return NotFound(new { message = "User not found in the database." });
                }

                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Internal server error: {ex.Message}" });
            }
        }
    }
}