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
    public class DinnersController : ControllerBase
    {
        //private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork _unitOfWork;

        //public DinnersController(ApplicationDbContext context)
        public DinnersController(IUnitOfWork unitOfWork)
        {
            //_context = context;
            _unitOfWork = unitOfWork;
        }

        // GET: api/Dinners
        [HttpGet]
        //public async Task<ActionResult<IEnumerable<Dinner>>> GetDinners()
        public async Task<IActionResult> GetDinners()
        {
            //return await _context.Dinners.ToListAsync();
            var dinners = await _unitOfWork.Dinners.GetAll();
            return Ok(dinners);
        }

        // GET: api/Dinners/5
        [HttpGet("{id}")]
        //public async Task<ActionResult<Dinner>> GetDinner(int id)
        public async Task<IActionResult> GetDinner(int id)
        {
            //var dinner = await _context.Dinners.FindAsync(id);
            var dinner = await _unitOfWork.Dinners.Get(q => q.Id == id);

            if (dinner == null)
            {
                return NotFound();
            }

            return Ok(dinner);
        }

        // PUT: api/Dinners/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDinner(int id, Dinner dinner)
        {
            if (id != dinner.Id)
            {
                return BadRequest();
            }

            //_context.Entry(dinner).State = EntityState.Modified;
            _unitOfWork.Dinners.Update(dinner);

            try
            {
                //await _context.SaveChangesAsync();
                await _unitOfWork.Save(HttpContext);
            
            }
            catch (DbUpdateConcurrencyException)
            {
                //if (!DinnerExists(id))
                if (!await DinnerExists(id))
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

        // POST: api/Dinners
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Dinner>> PostDinner(Dinner dinner)
        {
            //_context.Dinners.Add(dinner);
            //await _context.SaveChangesAsync();
            await _unitOfWork.Dinners.Insert(dinner);
            await _unitOfWork.Save(HttpContext);

            return CreatedAtAction("GetDinner", new { id = dinner.Id }, dinner);
        }

        // DELETE: api/Dinners/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDinner(int id)
        {
            //var dinner = await _context.Dinners.FindAsync(id);
            var dinner = await _unitOfWork.Dinners.Get(q => q.Id == id);
            if (dinner == null)
            {
                return NotFound();
            }

            //_context.Dinners.Remove(dinner);
            //await _context.SaveChangesAsync();
            await _unitOfWork.Dinners.Delete(id);
            await _unitOfWork.Save(HttpContext);

            return NoContent();
        }

        //private bool DinnerExists(int id)
        private async Task<bool> DinnerExists(int id)
        {
            //return _context.Dinners.Any(e => e.Id == id);
            var dinner = await _unitOfWork.Dinners.Get(q =>q.Id == id);
            return dinner != null;
        }
    }
}
