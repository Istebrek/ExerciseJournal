using ExerciseJournalAPI.Data;
using ExerciseJournalAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace ExerciseJournalAPI.Service;

public class DayInitializer
{
    private readonly IServiceProvider serviceProvider;

    public DayInitializer(IServiceProvider _serviceProvider)
    {
        this.serviceProvider = _serviceProvider;
    }

    public async Task InitializeAsync()
    {
        int retries = 5;
        while (retries > 0)
        try
        {
            using var scope = serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            var today = DateTime.Today;
            bool exists = await dbContext.Days.AnyAsync(d => d.Date.Date == today);

            if (!exists)
            {
                var day = new Day { Date = today };
                dbContext.Days.Add(day);
                await dbContext.SaveChangesAsync();
            }

            break;

        }
        catch (Exception ex)
        {
            retries--;
            Console.WriteLine($"Database not ready yet. Retries left: {retries}. Error: {ex.Message}");
            Thread.Sleep(3000);
        }
    }
}
