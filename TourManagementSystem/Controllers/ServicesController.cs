﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TourManagementSystem.Data;
using TourManagementSystem.Models;

namespace TourManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicesController : ControllerBase
    {
        private readonly DataContext _context;

        public ServicesController(DataContext context)
        {
            _context = context;
        }

        // GET: api/User
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Service>>> GetServices()
        {
            if (_context.Users == null)
            {
                return NotFound();
            }
            return await _context.Services.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Service>> GetService(int id)
        {
            if (_context.Services == null)
            {
                return NotFound();
            }
            var Service = await _context.Services!.FindAsync(id);

            if (Service == null)
            {
                return NotFound("Person not found");
            }
            return Ok(Service);
        }

        [HttpPost]
        public async Task<ActionResult<Service>> AddService(Service service)
        {
            _context.Services!.Add(service);
            await _context.SaveChangesAsync();

            return Ok(await GetServices());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutService(int id, Service service)
        {
            if (id != service.Id)
            {
                return BadRequest();
            }

            _context.Entry(service).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ServiceExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(service);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteService(int id)
        {

            if (_context.Services == null)
            {
                return NotFound();
            }
            var Service = await _context.Services!.FindAsync(id);
            if (Service == null)
            {
                return NotFound();
            }
            _context.Services.Remove(Service);
            await _context.SaveChangesAsync();

            return Ok(Service);
        }

        private bool ServiceExists(int id)
        {
            return (_context.Services?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}