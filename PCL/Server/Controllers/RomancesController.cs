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
    public class RomancesController : ControllerBase
    {
        //private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork _unitOfWork;

        //public RomancesController(ApplicationDbContext context)
        public RomancesController(IUnitOfWork unitOfWork)
        {
            //_context = context;
            _unitOfWork = unitOfWork;
        }

        // GET: api/Romances
        [HttpGet]
        //public async Task<ActionResult<IEnumerable<Romance>>> GetRomances()
        public async Task<IActionResult> GetBookings()
        {
            //return await _context.Romances.ToListAsync();
            //Remember the applicant will be added later. Ramances was done first before Applicant
            var Romances = await _unitOfWork.Romances.GetAll(includes: q => q.Include(x => x.MyActivity).Include(x => x.Applicant));
            return Ok(Romances);
        }

        // GET: api/Romances/5
        [HttpGet("{id}")]
       // public async Task<ActionResult<Romance>> GetRomance(int id)
       public async Task<IActionResult> GetRomance(int id)
        {
            //var romance = await _context.Romances.FindAsync(id);
            var Romance = await _unitOfWork.Romances.Get(q => q.Id== id);

            if (Romance == null)
            {
                return NotFound();
            }

            return Ok(Romance);
        }

        // PUT: api/Romances/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRomance(int id, Romance Romance)
        {
            if (id != Romance.Id)
            {
                return BadRequest();
            }

            //_context.Entry(romance).State = EntityState.Modified;
            _unitOfWork.Romances.Update(Romance);

            try
            {
                //await _context.SaveChangesAsync();
                await _unitOfWork.Save(HttpContext);
            }
            catch (DbUpdateConcurrencyException)
            {
                //if (!RomanceExists(id))
                if (!await RomanceExists(id))
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

        // POST: api/Romances
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Romance>> PostRomance(Romance Romance)
        {
            //_context.Romances.Add(romance);
            //await _context.SaveChangesAsync();
            await _unitOfWork.Romances.Insert(Romance);
            await _unitOfWork.Save(HttpContext);

            return CreatedAtAction("GetRomance", new { id = Romance.Id }, Romance);
        }

        // DELETE: api/Romances/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRomance(int id)
        {
            //var romance = await _context.Romances.FindAsync(id);
            var Romance = await _unitOfWork.Romances.Get(q => q.Id == id);
            if (Romance == null)
            {
                return NotFound();
            }

            //_context.Romances.Remove(romance);
            //await _context.SaveChangesAsync();
            await _unitOfWork.Romances.Delete(id);
            await _unitOfWork.Save(HttpContext);

            return NoContent();
        }

        //private bool RomanceExists(int id)
        private async Task<bool> RomanceExists(int id)
        {
            //return _context.Romances.Any(e => e.Id == id);
            var Romance = await _unitOfWork.Romances.Get(q => q.Id ==id);
            return Romance !=null;
        }
    }
}
