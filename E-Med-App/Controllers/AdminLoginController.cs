using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using E_Med_App.Models;
using E_Med_App.Data;

namespace E_Med_App.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AdminLoginController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AdminLoginController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("AddAdmin")]
        public IActionResult AddAdmin(Admin admin)
        {
            _context.Admins.Add(admin);
            _context.SaveChanges();
            return Ok("Admin added successfully.");
        }

        [HttpPost]
        [Route("Login")]
        public IActionResult Login(Admin admin)
        {
            var existingAdmin = _context.Admins.FirstOrDefault(a => a.UserName == admin.UserName && a.Password == admin.Password);
            if (existingAdmin != null)
            {
                return Ok("Login successful.");
            }
            return BadRequest("Invalid username or password.");
        }

        [HttpGet]
        [Route("GetAdmins")]
        public IActionResult GetAdmins()
        {
            var admins = _context.Admins.ToList();
            return Ok(admins);
        }
    }
}
