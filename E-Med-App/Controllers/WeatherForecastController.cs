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


    }
}