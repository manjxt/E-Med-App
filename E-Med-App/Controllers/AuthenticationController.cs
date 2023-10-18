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
        public async Task<IActionResult> Register(User user)
        {
            try
            {
                if (string.IsNullOrEmpty(user?.Password) || string.IsNullOrEmpty(user?.UserName))
                {
                    return BadRequest("Username or password is empty.");
                }

                // Add the user to the database
                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                return Ok("User registered successfully.");
            }
            catch (DbUpdateException)
            {
                return BadRequest("User registration failed.");
            }
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(User model)
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
                    return Ok("Login successful.");
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
