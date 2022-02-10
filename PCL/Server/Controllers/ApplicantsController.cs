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
    public class ApplicantsController : ControllerBase
    {
        // private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork _unitOfWork;

        //public ApplicantsController(ApplicationDbContext context)
        public ApplicantsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            //_context = context;
        }

        // GET: api/Applicants
        [HttpGet]
        //public async Task<ActionResult<IEnumerable<Applicant>>> GetApplicants()
        public async Task<IActionResult> GetApplicants()
        {
            //return await _context.Applicants.ToListAsync();
            var Applicants = await _unitOfWork.Applicants.GetAll();
            return Ok(Applicants);
        }

        // GET: api/Applicants/5
        [HttpGet("{id}")]
        //public async Task<ActionResult<Applicant>> GetApplicant(int id)
        public async Task<IActionResult> GetApplicant(int id)
        {
            //var applicant = await _context.Applicants.FindAsync(id);
            var Applicant = await _unitOfWork.Applicants.Get(q => q.Id == id);

            if (Applicant == null)
            {
                return NotFound();
            }

            return Ok(Applicant);
        }

        // PUT: api/Applicants/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutApplicant(int id, Applicant Applicant)
        {
            if (id != Applicant.Id)
            {
                return BadRequest();
            }

            //_context.Entry(applicant).State = EntityState.Modified;
            _unitOfWork.Applicants.Update(Applicant);

            try
            {
                //await _context.SaveChangesAsync();
                await _unitOfWork.Save(HttpContext);
            }
            catch (DbUpdateConcurrencyException)
            {
                //if (!ApplicantExists(id))
                if (!await ApplicantExists(id))
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

        // POST: api/Applicants
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Applicant>> PostApplicant(Applicant Applicant)
        {
            //_context.Applicants.Add(applicant);
            //await _context.SaveChangesAsync();
            await _unitOfWork.Applicants.Insert(Applicant);
            await _unitOfWork.Save(HttpContext);

            return CreatedAtAction("GetApplicant", new { id = Applicant.Id }, Applicant);
        }

        // DELETE: api/Applicants/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteApplicant(int id)
        {
            //var applicant = await _context.Applicants.FindAsync(id);
            var Applicant = await _unitOfWork.Applicants.Get(q => q.Id== id);
            if (Applicant == null)
            {
                return NotFound();
            }

            //_context.Applicants.Remove(applicant);
            //await _context.SaveChangesAsync();
            await _unitOfWork.Applicants.Delete(id);
            await _unitOfWork.Save(HttpContext);

            return NoContent();
        }

        //private bool ApplicantExists(int id)
        private async Task<bool> ApplicantExists(int id)
        {
            //return _context.Applicants.Any(e => e.Id == id);
            var Applicant = await _unitOfWork.Applicants.Get(q =>q.Id== id);
            return Applicant != null;
        }
    }
}
