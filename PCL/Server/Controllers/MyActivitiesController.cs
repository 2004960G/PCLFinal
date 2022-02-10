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
    public class MyActivitiesController : ControllerBase
    {
        //private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork _unitOfWork;

        //public MyActivitiesController(ApplicationDbContext context)
        public MyActivitiesController(IUnitOfWork unitOfWork)
        {
            //_context = context;
            _unitOfWork = unitOfWork;
        }

        // GET: api/MyActivities
        [HttpGet]
        //public async Task<ActionResult<IEnumerable<MyActivity>>> GetMyActivities()
        public async Task <IActionResult> GetMyActivities()
        {
            //return await _context.MyActivities.ToListAsync();
            var myactivities = await _unitOfWork.MyActivities.GetAll(includes: q => q.Include(x => x.Movie).Include(x => x.Cafe).Include(x => x.Festival).Include(x => x.Dinner));
            return Ok(myactivities);
        }

        // GET: api/MyActivities/5
        [HttpGet("{id}")]
        //public async Task<ActionResult<MyActivity>> GetMyActivity(int id)
        public async Task<IActionResult> GetMyActivity(int id)
        {
            // if data is not showing check here !!
            //var myActivity = await _context.MyActivities.FindAsync(id);
            var myActivity = await _unitOfWork.MyActivities.Get(q => q.Id == id);

            if (myActivity == null)
            {
                return NotFound();
            }

            return Ok(myActivity);
        }

        // PUT: api/MyActivities/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMyActivity(int id, MyActivity myActivity)
        {
            if (id != myActivity.Id)
            {
                return BadRequest();
            }

            //_context.Entry(myActivity).State = EntityState.Modified;
            _unitOfWork.MyActivities.Update(myActivity);

            try
            {
                //await _context.SaveChangesAsync();
                await _unitOfWork.Save(HttpContext);
            }
            catch (DbUpdateConcurrencyException)
            {
                //if (!MyActivityExists(id))
                if (!await MyActivityExists(id))
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

        // POST: api/MyActivities
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<MyActivity>> PostMyActivity(MyActivity myActivity)
        {
            //_context.MyActivities.Add(myActivity);
            //await _context.SaveChangesAsync();
            await _unitOfWork.MyActivities.Insert(myActivity);
            await _unitOfWork.Save(HttpContext);

            return CreatedAtAction("GetMyActivity", new { id = myActivity.Id }, myActivity);
        }

        // DELETE: api/MyActivities/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMyActivity(int id)
        {
            //var myActivity = await _context.MyActivities.FindAsync(id);
            var myActivity = await _unitOfWork.MyActivities.Get(q => q.Id == id);
            if (myActivity == null)
            {
                return NotFound();
            }

            //_context.MyActivities.Remove(myActivity);
            //await _context.SaveChangesAsync();
            await _unitOfWork.MyActivities.Delete(id);
            await _unitOfWork.Save(HttpContext);

            return NoContent();
        }

        //private bool MyActivityExists(int id)
        private async Task<bool> MyActivityExists(int id)
        {
            //return _context.MyActivities.Any(e => e.Id == id);
            var myActivity = await _unitOfWork.MyActivities.Get(q =>q.Id == id);
            return myActivity != null;
        
        }
    }
}
