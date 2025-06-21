using ExerciseJournalAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ExerciseJournalAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Day> Days { get; set; }
        public DbSet<Journal> Journals { get; set; }
    }
}
