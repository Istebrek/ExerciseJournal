using ExerciseJournalAPI.Data;
using ExerciseJournalAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ExerciseJournalAPI
{
    [Route("api/[controller]")]
    [ApiController]
    public class JournalController : ControllerBase
    {
        private readonly AppDbContext context;

        public JournalController(AppDbContext _context)
        {
            context = _context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Journal>>> GetAllJournals()
        {
            try
            {
                var journal = await context.Journals.ToListAsync();
                return Ok(journal);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Journal>> GetJournal(int id)
        {
            try
            {
                var journal = await context.Journals.FindAsync(id);
                if (journal == null) return NotFound();
                return Ok(journal);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteJournal(int id)
        {
            try
            {
                var journal = await context.Journals.FirstOrDefaultAsync(x => x.Id == id);
                if (journal == null) return NotFound();
                context.Remove(journal);
                await context.SaveChangesAsync();
                return Ok(journal);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<Journal>> CreateJournal([FromBody] Journal journal)
        {
            Console.WriteLine($"Received: DayId={journal.DayId}, Exercise={journal.Exercise}, Target={journal.Target}");
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                journal.Repetitions ??= 0;
                journal.Sets ??= 0;

                await context.Journals.AddAsync(journal);
                await context.SaveChangesAsync();
                return Ok(journal);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<Journal>> PatchJournal(int id, Journal journal)
        {
            try
            {
                var updateJournal = await context.Journals.FirstOrDefaultAsync(x => x.Id == id);
                if (updateJournal == null) return NotFound();

                if (journal.Sets.HasValue) updateJournal.Sets = journal.Sets;
                if (journal.Repetitions.HasValue) updateJournal.Repetitions = journal.Repetitions;

                await context.SaveChangesAsync();
                return Ok(updateJournal);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
