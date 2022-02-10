using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PCL.Server.Data;
using PCL.Server.IRepository;
using PCL.Shared.Domain;

namespace PCL.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        //private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork _unitOfWork;

        //public MoviesController(ApplicationDbContext context)
        public MoviesController(IUnitOfWork unitOfWork)
        {
            //_context = context;
            _unitOfWork = unitOfWork;
        }

        // GET: api/Movies
        [HttpGet]
        //public async Task<ActionResult<IEnumerable<Movie>>> GetMovies()
        public async Task<IActionResult> GetMovies()
        {
            //return await _context.Movies.ToListAsync();
            var movies = await _unitOfWork.Movies.GetAll();
            return Ok(movies);
        }

        // GET: api/Movies/5
        [HttpGet("{id}")]
        //public async Task<ActionResult<Movie>> GetMovie(int id)
        public async Task<IActionResult> GetMovie(int id)
        {
            //var movie = await _context.Movies.FindAsync(id);
            var movie = await _unitOfWork.Movies.Get(q => q.Id == id);

            if (movie == null)
            {
                return NotFound();
            }

            return Ok(movie);
        }

        // PUT: api/Movies/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMovie(int id, Movie movie)
        {
            if (id != movie.Id)
            {
                return BadRequest();
            }

            //_context.Entry(movie).State = EntityState.Modified;
            _unitOfWork.Movies.Update(movie);

            try
            {
                //await _context.SaveChangesAsync();
                await _unitOfWork.Save(HttpContext);
            }
            catch (DbUpdateConcurrencyException)
            {
                //if (!MovieExists(id))
                if (!await MovieExists(id))
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

        // POST: api/Movies
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Movie>> PostMovie(Movie movie)
        {
            //_context.Movies.Add(movie);
            //await _context.SaveChangesAsync();
            await _unitOfWork.Movies.Insert(movie);
            await _unitOfWork.Save(HttpContext);

            return CreatedAtAction("GetMovie", new { id = movie.Id }, movie);
        }

        // DELETE: api/Movies/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            //var movie = await _context.Movies.FindAsync(id);
            var movie = await _unitOfWork.Movies.Get(q => q.Id == id);
            if (movie == null)
            {
                return NotFound();
            }

            //_context.Movies.Remove(movie);
            //await _context.SaveChangesAsync();
            await _unitOfWork.Movies.Delete(id);
            await _unitOfWork.Save(HttpContext);

            return NoContent();
        }

        //private bool MovieExists(int id)
        private async Task<bool> MovieExists(int id)
        {
            //return _context.Movies.Any(e => e.Id == id);
            var movie = await _unitOfWork.Movies.Get(q => q.Id == id);
            return movie != null;
        }
    }
}
