using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
                SheduleDate = t.SheduleDate,
                      Genre = _context.TrailerGenres.Where(l => l.TrailerId == t.Id)
                                                    .Select(g => g.GenreId)
                                                    .ToList()
            });

            return Ok(trailersDto);
        }

        // GET: api/Trailer/:id
        [HttpGet("{id}")]
        public async Task<ActionResult<TrailerDTO>> Get(long id)
        {
            var trailer = await _context.Trailers.FindAsync(id);

            var trailersDto = new TrailerDTO
            {
                         Id = trailer.Id,
                      Title = trailer.Title,
                       Year = trailer.Year,
                        Url = trailer.Url,
                     ImgUrl = trailer.ImgUrl,
                SheduleDate = trailer.SheduleDate,
                      Genre = _context.TrailerGenres.Where(l => l.TrailerId == trailer.Id)
                                                    .Select(g => g.GenreId)
                                                    .ToList()
            };

            return Ok(trailersDto);
        }

        // POST: api/Trailer
        [HttpPost]
        public async Task<ActionResult<Trailer>> Post(TrailerDTO trailerDto)
        {
            var genreList = trailerDto.Genre;

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

            foreach (var item in genreList)
            {
                var trailerGenre = new TrailerGenre
                {
                    TrailerId = trailerDto.Id,
                    GenreId = item
                };
                
                await _context.TrailerGenres.AddAsync(trailerGenre);
            }

            await _context.SaveChangesAsync();
            
            return CreatedAtAction(nameof(Get), new { id = trailerDto.Id }, trailerDto);
            
            //return Ok(trailerDto);
        }

        // PUT: api/Trailer/:id
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(long id, TrailerDTO trailerDto)
        {
            if (id != trailerDto.Id) return BadRequest();

            var trailerToUpdate = await _context.Trailers.Where(t => t.Id == id)
                                                         .FirstOrDefaultAsync();
            
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
            try
            {
                var trailer = await _context.Trailers.FindAsync(id);
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
