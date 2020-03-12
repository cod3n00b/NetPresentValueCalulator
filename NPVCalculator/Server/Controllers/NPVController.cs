using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NPVCalculator.Server.DataContexts;
using NPVCalculator.Shared.Models;

namespace NPVCalculator.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NPVController : ControllerBase
    {
        private readonly NPVContext _context;

        public NPVController(NPVContext context)
        {
            _context = context;
        }

        // GET: api/NPV
        [HttpGet]
        public async Task<ActionResult<IEnumerable<NPVCriteria>>> GetCriterias()
        {
            return await _context.Criterias.Include(x => x.Results)
                .ToListAsync();
        }

        // GET: api/NPV/5
        [HttpGet("{id}")]
        public async Task<ActionResult<NPVCriteria>> GetNPVCriteria(int id)
        {
            //var nPVCriteria = await _context.Criterias.FindAsync(id);
            var nPVCriteria = await _context.Criterias
                .Include(x => x.Results)
                .FirstOrDefaultAsync(x => x.ReferenceId == id);

            if (nPVCriteria == null)
            {
                nPVCriteria = new NPVCriteria()
                {
                    ErrorMessage = "Reference Id not found."
                };
            }

            return nPVCriteria;
        }

        // PUT: api/NPV/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNPVCriteria(int id, NPVCriteria nPVCriteria)
        {
            if (id != nPVCriteria.ReferenceId)
            {
                return BadRequest();
            }

            _context.Entry(nPVCriteria).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NPVCriteriaExists(id))
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

        // POST: api/NPV
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<NPVCriteria>> PostNPVCriteria(NPVCriteria nPVCriteria)
        {
            nPVCriteria.ComputeNPVForAllDiscounts();

            _context.Criterias.Add(nPVCriteria);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetNPVCriteria", new { id = nPVCriteria.ReferenceId }, nPVCriteria);
        }

        // DELETE: api/NPV/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<NPVCriteria>> DeleteNPVCriteria(int id)
        {
            var nPVCriteria = await _context.Criterias.FindAsync(id);
            if (nPVCriteria == null)
            {
                return NotFound();
            }

            _context.Criterias.Remove(nPVCriteria);
            await _context.SaveChangesAsync();

            return nPVCriteria;
        }

        private bool NPVCriteriaExists(int id)
        {
            return _context.Criterias.Any(e => e.ReferenceId == id);
        }
    }
}
