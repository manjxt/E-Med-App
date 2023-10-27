using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using E_Med_App.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using E_Med_App.Data;

namespace E_Med_App.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _context;

        public AuthenticationController(IConfiguration configuration, ApplicationDbContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] User user)
        {
            try
            {
                if (string.IsNullOrEmpty(user?.Password) || string.IsNullOrEmpty(user?.UserName))
                {
                    return BadRequest("Username or password is empty.");
                }

                // Add the user to the database
                _context.Users.Add(user);
                /*Cart cart = new()
                {
                    UserId = user.Id,
                    Items = new List<Medicine>()
                };
                _context.Carts.Add(cart);*/
                await _context.SaveChangesAsync();



                return Ok(user);
            }
            catch (DbUpdateException)
            {
                return BadRequest("User registration failed.");
            }
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] User model)
        {
            try
            {
                if (string.IsNullOrEmpty(model?.Password) || string.IsNullOrEmpty(model?.UserName))
                {
                    return BadRequest("Username or password is empty.");
                }

                // Check if the user exists in the database
                var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.UserName == model.UserName && u.Password == model.Password);

                if (existingUser != null)
                {
                    return Ok(existingUser);
                }
                else
                {
                    // User not found, return some error response
                    return NotFound("Invalid username or password.");
                }
            }
            catch (DbUpdateException)
            {
                return BadRequest("Login failed.");
            }
        }

    }
}
