using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
// using trailers_api.Data;
// using trailers_api.Models;

namespace trailers_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrailerController : ControllerBase
    {
        // private readonly trailersContext _context;

        // public TrailerController(trailersContext context)
        // {
        //     _context = context;
        // }

        // // GET: api/Trailer
        // [HttpGet]
        // public async Task<ActionResult<Trailer[]>> Get()
        // {
        //     var trailers = await _context.Trailers.ToListAsync();

        //     return Ok(trailers);
        // }

        // // GET: api/Trailer/:id
        // [HttpGet("{id}", Name = "Get")]
        // public async Task<ActionResult<Trailer>> Get(int id)
        // {
        //     var trailer = await _context.Trailers.FindAsync(id);

        //     return Ok(trailer);
        // }

        // // POST: api/Trailer
        // [HttpPost]
        // public async Task<ActionResult<Trailer>> Post(Trailer trailer)
        // {
        //     await _context.AddAsync(trailer);
        //     await _context.SaveChangesAsync();

        //     return CreatedAtAction(nameof(Get), new { id = trailer.Id }, trailer);
        // }

        // // PUT: api/Trailer/:id
        // [HttpPut("{id}")]
        // public async Task<ActionResult<Trailer>> Put(int id, Trailer trailer)
        // {
        //     if (id != trailer.Id) return BadRequest();

        //     var existingTrailer = await _context.Trailers.Where(t => t.Id == id).FirstOrDefaultAsync();

        //     try
        //     {
        //         if (existingTrailer != null)
        //         {
        //             _context.Entry(trailer).State = EntityState.Modified;

        //             await _context.SaveChangesAsync();
        //         }
        //         else
        //         {
        //             return NotFound();
        //         }
        //     }
        //     catch (Exception e)
        //     {
        //         BadRequest(e.InnerException);
        //     }

        //     return NoContent();
        // }

        // // DELETE: api/Trailer/:id
        // [HttpDelete("{id}")]
        // public async Task<IActionResult> Delete(int id)
        // {
        //     var trailer = await _context.Trailers.FindAsync(id);
        //     _context.Trailers.Remove(trailer);

        //     await _context.SaveChangesAsync();

        //     return NoContent();
        // }
    }
}
