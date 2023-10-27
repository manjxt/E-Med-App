using E_Med_App.Data;
using E_Med_App.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace E_Med_App.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class MedicinesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<MedicinesController> _logger;

        public MedicinesController(ILogger<MedicinesController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Medicine>>> GetMedicines()
        {
            var medicines = await _context.Medicines.ToListAsync();
            return Ok(medicines);
        }

        [HttpPost]
        public async Task<ActionResult> AddMedicine(Medicine medicine)
        {
            await _context.Medicines.AddAsync(medicine);
            await _context.SaveChangesAsync();

            return Ok("Success");
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Medicine>>> GetMedicine(int id)
        {
            var medicine = await _context.Medicines.FindAsync(id);

            if (medicine == null)
            {
                return NotFound();
            }

            return Ok(medicine);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteMedicine(int id)
        {
            var medicine = await _context.Medicines.FindAsync(id);
               
            if(medicine == null) {
                return NotFound();
            }

             _context.Medicines.Remove(medicine);

            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> EditMedicine(int id, Medicine updatedMedicine)
        {
            var medicine = await _context.Medicines.FindAsync(id);

            if (medicine == null)
            {
                return NotFound();
            }

            // Update properties of the medicine based on the provided updatedMedicine object
            medicine.ImageUrl = updatedMedicine.ImageUrl;
            medicine.Name = updatedMedicine.Name;
            medicine.Description = updatedMedicine.Description;
            medicine.Seller = updatedMedicine.Seller;
            medicine.Price = updatedMedicine.Price;

            await _context.SaveChangesAsync();

            return Ok();
        }

    }
}