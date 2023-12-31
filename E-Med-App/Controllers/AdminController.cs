﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using E_Med_App.Data;
using E_Med_App.Models;

namespace E_Med_App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("Users")]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        // GET: api/Admin
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Medicine>>> GetMedicines()
        {
          if (_context.Medicines == null)
          {
              return NotFound();
          }
            return await _context.Medicines.ToListAsync();
        }

        // GET: api/Admin/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Medicine>> GetMedicine(int id)
        {
          if (_context.Medicines == null)
          {
              return NotFound();
          }
            var medicine = await _context.Medicines.FindAsync(id);

            if (medicine == null)
            {
                return NotFound();
            }

            return medicine;
        }

        // PUT: api/Admin/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMedicine(int id, Medicine medicine)
        {
            if (id != medicine.Id)
            {
                return BadRequest();
            }

            _context.Entry(medicine).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MedicineExists(id))
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

        // POST: api/Admin
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Medicine>> PostMedicine(Medicine medicine)
        {
          if (_context.Medicines == null)
          {
              return Problem("Entity set 'ApplicationDbContext.Medicines'  is null.");
          }
            _context.Medicines.Add(medicine);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMedicine", new { id = medicine.Id }, medicine);
        }

        // DELETE: api/Admin/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMedicine(int id)
        {
            if (_context.Medicines == null)
            {
                return NotFound();
            }
            var medicine = await _context.Medicines.FindAsync(id);
            if (medicine == null)
            {
                return NotFound();
            }

            _context.Medicines.Remove(medicine);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MedicineExists(int id)
        {
            return (_context.Medicines?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
