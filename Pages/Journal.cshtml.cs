using ExerciseJournalAPI.Data;
using ExerciseJournalAPI.Models;
using ExerciseJournalAPI.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ExerciseJournalAPI.Pages;

public class JournalModel : PageModel
{
    private readonly ExerciseAPI service;
    private readonly AppDbContext context;


    public string ExerciseJson { get; set; }

    public List<Exercise> Exercises { get; set; }

    public JournalModel (ExerciseAPI _service, AppDbContext _context)
    {
        service = _service;
        context = _context;
    }
    [BindProperty(SupportsGet = true)]
    public DateTime CurrentDate { get; set; } = DateTime.Today;

    public Day CurrentDay { get; set; }

    public List<Journal> JournalsForTheDay { get; set; } = new();

    public async Task OnGetAsync()
    {
        Exercises = await service.GetExercises();

        CurrentDay = await context.Days
        .Include(d => d.Journals)
        .FirstOrDefaultAsync(d => d.Date == CurrentDate.Date);

        if (CurrentDay != null)
        {
            JournalsForTheDay = await context.Journals
                .Where(j => j.DayId == CurrentDay.Id)
                .ToListAsync();
        }
        else
        {
            JournalsForTheDay = new List<Journal>();
        }
    }
}
