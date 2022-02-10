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
    public class FestivalsController : ControllerBase
    {
        //private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork _unitOfWork;

        //public FestivalsController(ApplicationDbContext context)
        public FestivalsController(IUnitOfWork unitOfWork)
        {
            //_context = context;
            _unitOfWork = unitOfWork;
        }

        // GET: api/Festivals
        [HttpGet]
        //public async Task<ActionResult<IEnumerable<Festival>>> GetFestivals()
        public async Task<IActionResult> GetFestivals()
        {
            //return await _context.Festivals.ToListAsync();
            var festivals = await _unitOfWork.Festivals.GetAll();
            return Ok(festivals);
        }

        // GET: api/Festivals/5
        [HttpGet("{id}")]
        //public async Task<ActionResult<Festival>> GetFestival(int id)
        public async Task<IActionResult> GetFestival(int id)
        {
            //var festival = await _context.Festivals.FindAsync(id);
            var festival = await _unitOfWork.Festivals.Get(q => q.Id == id);

            if (festival == null)
            {
                return NotFound();
            }

            return Ok(festival);
        }

        // PUT: api/Festivals/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFestival(int id, Festival festival)
        {
            if (id != festival.Id)
            {
                return BadRequest();
            }

            //_context.Entry(festival).State = EntityState.Modified;
            _unitOfWork.Festivals.Update(festival);

            try
            {
                //await _context.SaveChangesAsync();
                await _unitOfWork.Save(HttpContext);
            }
            catch (DbUpdateConcurrencyException)
            {
                //if (!FestivalExists(id))
                if (!await FestivalExists(id))
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

        // POST: api/Festivals
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Festival>> PostFestival(Festival festival)
        {
            //_context.Festivals.Add(festival);
            //await _context.SaveChangesAsync();
            await _unitOfWork.Festivals.Insert(festival);
            await _unitOfWork.Save(HttpContext);

            return CreatedAtAction("GetFestival", new { id = festival.Id }, festival);
        }

        // DELETE: api/Festivals/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFestival(int id)
        {
            //var festival = await _context.Festivals.FindAsync(id);
            var festival = await _unitOfWork.Festivals.Get(q => q.Id == id);
            if (festival == null)
            {
                return NotFound();
            }

            //_context.Festivals.Remove(festival);
            //await _context.SaveChangesAsync();
            await _unitOfWork.Festivals.Delete(id);
            await _unitOfWork.Save(HttpContext);

            return NoContent();
        }

        //private bool FestivalExists(int id)
        private async Task<bool> FestivalExists(int id)
        {
            //return _context.Festivals.Any(e => e.Id == id);
            var festival = await _unitOfWork.Festivals.Get(q => q.Id == id);
            return festival != null;
        }
    }
}
