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
    public class CafesController : ControllerBase
    {
        //private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork _unitOfWork;

        //public CafesController(ApplicationDbContext context)
        public CafesController(IUnitOfWork unitOfWork)
        {
            //_context = context;
            _unitOfWork = unitOfWork;
        }

        // GET: api/Cafes
        [HttpGet]
        //public async Task<ActionResult<IEnumerable<Cafe>>> GetCafes()
        public async Task<IActionResult> GetCafes()
        {
            // TO be removed once the testing is completed
           //return NotFound();
            //return await _context.Cafes.ToListAsync();
            var cafes = await _unitOfWork.Cafes.GetAll();
            return Ok(cafes);
        }

        // GET: api/Cafes/5
        [HttpGet("{id}")]
        //public async Task<ActionResult<Cafe>> GetCafe(int id)
        public async Task<IActionResult> GetCafe(int id)
        {
            //var cafe = await _context.Cafes.FindAsync(id);
            var cafe = await _unitOfWork.Cafes.Get(q => q.Id == id);

            if (cafe == null)
            {
                return NotFound();
            }

            return Ok(cafe);
        }

        // PUT: api/Cafes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCafe(int id, Cafe cafe)
        {
            if (id != cafe.Id)
            {
                return BadRequest();
            }

            //_context.Entry(cafe).State = EntityState.Modified;
            _unitOfWork.Cafes.Update(cafe);

            try
            {
                //await _context.SaveChangesAsync();
                await _unitOfWork.Save(HttpContext);
            }
            catch (DbUpdateConcurrencyException)
            {
                //if (!CafeExists(id))
                if (!await CafeExists(id))
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

        // POST: api/Cafes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Cafe>> PostCafe(Cafe cafe)
        {
            //_context.Cafes.Add(cafe);
            //await _context.SaveChangesAsync();
            await _unitOfWork.Cafes.Insert(cafe);
            await _unitOfWork.Save(HttpContext);

            return CreatedAtAction("GetCafe", new { id = cafe.Id }, cafe);
        }

        // DELETE: api/Cafes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCafe(int id)
        {
           // var cafe = await _context.Cafes.FindAsync(id);
           var cafe = await _unitOfWork.Cafes.Get(q => q.Id == id); 
            if (cafe == null)
            {
                return NotFound();
            }

            //_context.Cafes.Remove(cafe);
            //await _context.SaveChangesAsync();
            await _unitOfWork.Cafes.Delete(id);
            await _unitOfWork.Save(HttpContext);

            return NoContent();
        }

        //private bool CafeExists(int id)
        private async Task<bool> CafeExists(int id)
        {
            //return _context.Cafes.Any(e => e.Id == id);
            var cafe = await _unitOfWork.Cafes.Get(q => q.Id == id);
            return cafe != null;
        }
    }
}
