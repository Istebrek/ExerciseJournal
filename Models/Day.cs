using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExerciseJournalAPI.Models;

public class Day
{
    [Key]
    public int Id { get; set; }

    [Required]
    public DateTime Date { get; set; } = DateTime.Now;

    [NotMapped]
    public int Year => Date.Year;

    [NotMapped]
    public int Month => Date.Month;

    [NotMapped]
    public int DayOfMonth => Date.Day;

    public List<Journal> Journals { get; set; } = new();

}
