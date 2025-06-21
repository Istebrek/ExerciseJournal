using ExerciseJournalAPI.Data;
using ExerciseJournalAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ExerciseJournalAPI;

[Route("api/[controller]")]
[ApiController]
public class DayController : ControllerBase
{
    private readonly AppDbContext context;

    public DayController (AppDbContext _context)
    {
        context = _context;
    }

    [HttpGet]
    public async Task<ActionResult<List<Day>>> GetAllDays ()
    {
        try
        {
            var day = await context.Days.ToListAsync();
            return Ok(day);
        } catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Day>> GetDay (int id)
    {
        try
        {
            var day = await context.Days.FindAsync(id);
            if (day == null) return NotFound();
            return Ok(day);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteDay (int id)
    {
        try
        {
            var day = await context.Days.FirstOrDefaultAsync(x => x.Id == id);
            if (day == null) return NotFound();
            context.Remove(day);
            await context.SaveChangesAsync();
            return Ok(day);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpPost]
    public async Task<ActionResult<Day>> CreateDay (Day day)
    {
        try
        {
            await context.Days.AddAsync(day);
            await context.SaveChangesAsync();
            return Ok(day);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpPatch("{id}")]
    public async Task<ActionResult<Day>> PatchDay (int id, Day day)
    {
        try
        {
            var updateDay = await context.Days.FirstOrDefaultAsync(x => x.Id == id);
            if (updateDay == null) return NotFound();

            if (day.Date != null) updateDay.Date = day.Date;

            return Ok(updateDay);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpGet("today")]
    public async Task<ActionResult<int>> GetTodayDayId()
    {
        try
        {
            var today = DateTime.Today;
            var day = await context.Days.FirstOrDefaultAsync(d => d.Date.Date == today);

            if (day == null)
            {
                day = new Day { Date = today };
                context.Days.Add(day);
                await context.SaveChangesAsync();
            }

            return Ok(day.Id);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpGet("by-date")]
    public async Task<ActionResult<int>> GetOrCreateDayByDate([FromQuery] DateTime date)
    {
        Console.WriteLine($"Fetching day for: {date}");
        try
        {
            var day = await context.Days.FirstOrDefaultAsync(d => d.Date.Date == date.Date);
            if (day == null)
            {
                day = new Day { Date = date };
                context.Days.Add(day);
                await context.SaveChangesAsync();
                Console.WriteLine("Created new day entry.");
            }
            return Ok(day.Id);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
}
