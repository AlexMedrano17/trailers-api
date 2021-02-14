using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            var movies = await _context.Movies.Include(m => m.RatingNavigation).ToListAsync();     
                                
            var movieDTO = movies.Select(m => new MovieDTO 
            {
                       Id = m.Id,
                    Title = m.Title,
                     Year = m.Year,
                 Trailers = _context.Trailers.Where(t => t.MovieId == m.Id)
                            .Select(t => t.Url)
                            .ToList(),
                   Actors = _context.MovieActors.Where(t => t.MovieId == m.Id)
                            .Select(x => x.Actor.Name + " " + x.Actor.LastName)
                            .ToList(),
                Directors = _context.MovieDirectors.Where(t => t.MovieId == m.Id)
                            .Select(x => x.Director.Name + " " + x.Director.LastName)
                            .ToList(),
                   Genres = _context.MovieGenres.Where(mg => mg.MovieId == m.Id)
                            .Select(g => g.Genre.Genre1)
                            .ToList(),
                   Rating = m.RatingNavigation.Rating,
                    Image = Encoding.UTF8.GetString(m.Image),
                CreatedAt = m.CreatedAt
            });

            return Ok(movieDTO);
        }

        // GET: api/Trailer/:id
        [HttpGet("{id}")]
        public async Task<ActionResult<MovieDTO>> Get(int id)
        {
            var movie = await _context.Movies.Include(m => m.RatingNavigation).Where(m => m.Id == id).FirstOrDefaultAsync();

            if (movie == null) return NotFound();

            var movieDTO = new MovieDTO 
            {
                       Id = movie.Id,
                    Title = movie.Title,
                     Year = movie.Year,
                 Trailers = _context.Trailers.Where(t => t.MovieId == movie.Id)
                            .Select(t => t.Url)
                            .ToList(),
                   Actors = _context.MovieActors.Where(t => t.MovieId == movie.Id)
                            .Select(x => x.Actor.Name + " " + x.Actor.LastName)
                            .ToList(),
                Directors = _context.MovieDirectors.Where(t => t.MovieId == movie.Id)
                            .Select(x => x.Director.Name + " " + x.Director.LastName)
                            .ToList(),
                   Genres = _context.MovieGenres.Where(mg => mg.MovieId == movie.Id)
                            .Select(g => g.Genre.Genre1)
                            .ToList(),
                   Rating = movie.RatingNavigation.Rating,
                    Image = Encoding.UTF8.GetString(movie.Image),
                CreatedAt = movie.CreatedAt
            };

            return Ok(movieDTO);
        }

        // POST: api/Trailer
        [HttpPost]
        public async Task<ActionResult<MovieDTO>> Post(MovieDTO movieDTO)
        {
            try
            {
                var rating = await _context.Ratings.Where(r => r.Rating == movieDTO.Rating).FirstOrDefaultAsync();

                var movie = new Movie
                {
                    Title = movieDTO.Title,
                     Year = movieDTO.Year,
                    Image = Encoding.UTF8.GetBytes(movieDTO.Image),
                    Rating = rating.Id
                };

                await _context.Movies.AddAsync(movie);
                await _context.SaveChangesAsync();

                movieDTO.Id = movie.Id;
                movieDTO.CreatedAt = movie.CreatedAt;

                foreach (var item in movieDTO.Genres)
                { 
                    var genre = await _context.Genres.Where(g => g.Genre1 == item).FirstOrDefaultAsync();

                    if (genre == null) return NotFound();
                    
                    var movieGenre = new MovieGenre
                    {
                        MovieId = movieDTO.Id,
                        GenreId = genre.Id
                    };
                    
                    await _context.MovieGenres.AddAsync(movieGenre);
                    
                }

                foreach (var item in movieDTO.Actors)
                {
                    string[] actorObj = item.Split(" ");
                    string actorName = actorObj[0];
                    string actorLastName = actorObj[1];

                    var actor = await _context.Actors.Where(a => a.Name == actorName && a.LastName == actorLastName).FirstOrDefaultAsync();

                    if (actor == null)
                    {
                        var newActor = new Actor
                        {
                            Name = actorName,
                            LastName = actorLastName
                        };

                        await _context.Actors.AddAsync(newActor);
                        await _context.SaveChangesAsync();

                        actor = newActor;
                    }
                    Console.WriteLine(actor);
                    var movieActor = new MovieActor
                    {
                        MovieId = movieDTO.Id,
                        ActorId = actor.Id
                    };
                    
                    await _context.MovieActors.AddAsync(movieActor);
                }

                foreach (var item in movieDTO.Directors)
                {
                    string[] directorObj = item.Split(" ");
                    string directorName = directorObj[0];
                    string directorLastName = directorObj[1];

                    var director = await _context.Directors.Where(d => d.Name == directorName && d.LastName == directorLastName).FirstOrDefaultAsync();
                    
                    if (director == null)
                    {
                        var newDirector = new Director
                        {
                            Name = directorName,
                            LastName = directorLastName
                        };

                        await _context.Directors.AddAsync(newDirector);
                        await _context.SaveChangesAsync();

                        director = newDirector;
                    }

                    var movieDirector = new MovieDirector
                    {
                        MovieId = movieDTO.Id,
                        DirectorId = director.Id
                    };
                    
                    await _context.MovieDirectors.AddAsync(movieDirector);
                }

                foreach (var item in movieDTO.Trailers)
                {
                    var trailer = new Trailer
                    {
                        MovieId = movieDTO.Id,
                        Url = item
                    };
                    
                    await _context.Trailers.AddAsync(trailer);
                }

                await _context.SaveChangesAsync();
                
                return CreatedAtAction(nameof(Get), new { id = movieDTO.Id }, movieDTO);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);  
            }
            
        }

        // PUT: api/Trailer/:id
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, MovieDTO movieDTO)
        {
            var movieToUpdate     = await _context.Movies.FindAsync(id);
            var mGenresToUpdate   = await _context.MovieGenres.Where(g => g.MovieId == id).ToListAsync();
            var mActorToUpdate    = await _context.MovieActors.Where(a => a.MovieId == id).ToListAsync();
            var mDirectorToUpdate = await _context.MovieDirectors.Where(d => d.MovieId == id).ToListAsync();
            var trailerToUpdate   = await _context.Trailers.Where(t => t.MovieId == id).ToListAsync();

            if (movieToUpdate == null) return NotFound();
            
            try
            {   
                var rating = await _context.Ratings.Where(r => r.Rating == movieDTO.Rating).FirstOrDefaultAsync();

                movieToUpdate.Title  = movieDTO.Title;
                movieToUpdate.Year   = movieDTO.Year;
                movieToUpdate.Rating = rating.Id;
                movieToUpdate.Image  = Encoding.UTF8.GetBytes(movieDTO.Image);

                foreach(var (item, index) in movieDTO.Genres.Select((value, index) => (value, index))) {
                    var genre = await _context.Genres.Where(g => g.Genre1 == item).FirstOrDefaultAsync();
                    mGenresToUpdate[index].GenreId = genre.Id;
                }

                foreach(var (item, index) in movieDTO.Actors.Select((value, index) => (value, index))) {
                    string[] actorObj = item.Split(" ");
                    string Name = actorObj[0];
                    string LastName = actorObj[1];
                    var actor = await _context.Actors.Where(a => a.Name == Name && a.LastName == LastName).FirstOrDefaultAsync();
                    mActorToUpdate[index].ActorId = actor.Id;
                }

                foreach(var (item, index) in movieDTO.Directors.Select((value, index) => (value, index))) {
                    string[] directorObj = item.Split(" ");
                    string Name = directorObj[0];
                    string LastName = directorObj[1];
                    var director = await _context.Directors.Where(a => a.Name == Name && a.LastName == LastName).FirstOrDefaultAsync();
                    mDirectorToUpdate[index].DirectorId = director.Id;
                }

                movieDTO.Trailers.Select((value, index) => (value, index)).ToList().ForEach(item => 
                    trailerToUpdate[item.index].Url = item.value
                );

                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                BadRequest(e.Message);
            }

            return NoContent();
        }

        // DELETE: api/Trailer/:id
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var movie      = await _context.Movies.FindAsync(id);
            var mGenres    = await _context.MovieGenres.Where(g => g.MovieId == id).ToListAsync();
            var mActors    = await _context.MovieActors.Where(a => a.MovieId == id).ToListAsync();
            var mDirectors = await _context.MovieDirectors.Where(d => d.MovieId == id).ToListAsync();
            var trailers   = await _context.Trailers.Where(t => t.MovieId == id).ToListAsync();

            if (movie == null) return NotFound();

            try
            {
                mGenres.ForEach(movieGenre => 
                    _context.MovieGenres.Remove(movieGenre)
                );

                mActors.ForEach(movieActor => 
                    _context.MovieActors.Remove(movieActor)
                );

                mDirectors.ForEach(movieDirector => 
                    _context.MovieDirectors.Remove(movieDirector)
                );

                trailers.ForEach(movietrailer => 
                    _context.Trailers.Remove(movietrailer)
                );

                _context.Movies.Remove(movie);

                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
            
            return NoContent();
        }
    }
}
