using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using trailers_api.Data;
using trailers_api.Models;
using trailers_api.Models.DTO;

namespace trailers_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrailerController : ControllerBase
    {
        private readonly trailersContext _context;

        public TrailerController(trailersContext context)
        {
            _context = context;
        }

        // GET: api/Trailer
        [HttpGet]
        public async Task<ActionResult<TrailerDTO[]>> Get()
        {
            var trailers = await _context.Trailers.ToListAsync();

            var trailersDto = trailers.Select(t => new TrailerDTO
            {
                         Id = t.Id,
                      Title = t.Title,
                       Year = t.Year,
                        Url = t.Url,
                     ImgUrl = t.ImgUrl,
                  CreatedAt = t.CreatedAt,
            });

            return Ok(trailersDto);
        }

        // GET: api/Trailer/:id
        [HttpGet("{id}")]
        public async Task<ActionResult<TrailerDTO>> Get(long id)
        {
            var trailer = await _context.Trailers.FindAsync(id);

            if (trailer == null) return NotFound();

            var trailersDto = new TrailerDTO
            {
                         Id = trailer.Id,
                      Title = trailer.Title,
                       Year = trailer.Year,
                        Url = trailer.Url,
                     ImgUrl = trailer.ImgUrl,
                  CreatedAt = trailer.CreatedAt
            };

            return Ok(trailersDto);
        }

        // POST: api/Trailer
        [HttpPost]
        public async Task<ActionResult<TrailerDTO>> Post(TrailerDTO trailerDto)
        {
            var trailer = new Trailer
            {
                 Title = trailerDto.Title,
                  Year = trailerDto.Year,
                   Url = trailerDto.Url,
                ImgUrl = trailerDto.ImgUrl
            };

            await _context.Trailers.AddAsync(trailer);
            await _context.SaveChangesAsync();

            trailerDto.Id = trailer.Id;
            trailerDto.CreatedAt = trailer.CreatedAt;
            
            return CreatedAtAction(nameof(Get), new { id = trailerDto.Id }, trailerDto);
        }

        // PUT: api/Trailer/:id
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(long id, TrailerDTO trailerDto)
        {
            var trailerToUpdate = await _context.Trailers.FindAsync(id);

            if (trailerToUpdate == null) return NotFound();
            
            try
            {   
                 trailerToUpdate.Title = trailerDto.Title;
                  trailerToUpdate.Year = trailerDto.Year;
                trailerToUpdate.ImgUrl = trailerDto.ImgUrl;
                   trailerToUpdate.Url = trailerDto.Url;

                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                BadRequest(e.InnerException);
            }

            return NoContent();
        }

        // DELETE: api/Trailer/:id
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var trailer = await _context.Trailers.FindAsync(id);

            if (trailer == null) return NotFound();

            try
            {
                _context.Trailers.Remove(trailer);

                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return BadRequest(e.InnerException);
            }
            
            return NoContent();
        }
    }
}
