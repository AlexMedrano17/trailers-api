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
    public class MovieController : ControllerBase
    {
        private readonly MoviesContext _context;

        public MovieController(MoviesContext context)
        {
            _context = context;
        }

        // GET: api/Trailer
        [HttpGet]
        public async Task<ActionResult<MovieDTO[]>> Get()
        {
            var movies = await _context.Movies.ToListAsync();

            var moviesDto = movies.Select(m => new MovieDTO
            {
                         Id = m.Id,
                      Title = m.Title,
                       Year = m.Year,
                   Trailers = _context.Trailers.Where(t => t.MovieId == m.Id)
                                               .Select(t => t.Url)
                                               .ToList(),
                     Actors = _context.MovieActors.Where(a => a.MovieId == m.Id)
                                               .Select(a => a.ActorId)
                                               .ToList(),
                  Directors = _context.MovieDirectors.Where(d => d.MovieId == m.Id)
                                               .Select(d => d.DirectorId)
                                               .ToList(),
                      Image = m.Image,
                  CreatedAt = m.CreatedAt,
                     Genres = _context.MovieGenres.Where(t => t.MovieId == m.Id)
                                                  .Select(g => g.GenreId)
                                                  .ToList()
            });

            return Ok(moviesDto);
        }

        // GET: api/Trailer/:id
        [HttpGet("{id}")]
        public async Task<ActionResult<MovieDTO>> Get(int id)
        {
            var movie = await _context.Movies.FindAsync(id);

            if (movie == null) return NotFound();

            var trailersDto = new MovieDTO
            {
                         Id = movie.Id,
                      Title = movie.Title,
                       Year = movie.Year,
                   Trailers = _context.Trailers.Where(t => t.MovieId == movie.Id)
                                               .Select(t => t.Url)
                                               .ToList(),
                     Actors = _context.MovieActors.Where(a => a.MovieId == movie.Id)
                                               .Select(a => a.ActorId)
                                               .ToList(),
                  Directors = _context.MovieDirectors.Where(d => d.MovieId == movie.Id)
                                               .Select(d => d.DirectorId)
                                               .ToList(),
                     Genres = _context.MovieGenres.Where(g => g.MovieId == movie.Id)
                                                  .Select(g => g.GenreId)
                                                  .ToList(),                       
                      Image = movie.Image,
                  CreatedAt = movie.CreatedAt
                     
            };

            return Ok(trailersDto);
        }

        // POST: api/Trailer
        [HttpPost]
        public async Task<ActionResult<MovieDTO>> Post(MovieDTO movieDTO)
        {
            try
            {
                var genreList = movieDTO.Genres;

                var movie = new Movie
                {
                    Title = movieDTO.Title,
                     Year = movieDTO.Year,
                    Image = movieDTO.Image
                };

                await _context.Movies.AddAsync(movie);
                await _context.SaveChangesAsync();

                movieDTO.Id = movie.Id;
                movieDTO.CreatedAt = movie.CreatedAt;

                foreach (var item in genreList)
                {
                    var movieGenre = new MovieGenre
                    {
                        MovieId = movieDTO.Id,
                        GenreId = item
                    };
                    
                    await _context.MovieGenres.AddAsync(movieGenre);
                }

                await _context.SaveChangesAsync();
                
                return CreatedAtAction(nameof(Get), new { id = movieDTO.Id }, movieDTO);
            }
            catch (Exception)
            {
                return BadRequest();  
            }
            
        }

        // PUT: api/Trailer/:id
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(long id, MovieDTO movieDTO)
        {
            int i = 0;
            var movieToUpdate = await _context.Movies.FindAsync(id);
            var mGenresToUpdate = await _context.MovieGenres.Where(g => g.MovieId == id).ToListAsync();

            if (movieToUpdate == null) return NotFound();
            
            try
            {   
                 movieToUpdate.Title = movieDTO.Title;
                  movieToUpdate.Year = movieDTO.Year;
                movieToUpdate.Image = movieDTO.Image;

                mGenresToUpdate.ForEach(x => 
                    x.GenreId = movieDTO.Genres[i++]
                );

                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                BadRequest();
            }

            return NoContent();
        }

        // DELETE: api/Trailer/:id
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var movies = await _context.Trailers.FindAsync(id);
            var mGenres = await _context.MovieGenres.Where(g => g.MovieId == id).ToListAsync(); 

            if (movies == null) return NotFound();

            try
            {
                _context.Trailers.Remove(movies);

                mGenres.ForEach(movieGenre => 
                    _context.MovieGenres.Remove(movieGenre)
                );

                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return BadRequest();
            }
            
            return NoContent();
        }
    }
}
