using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ExerciseJournalAPI.Models;

public class Journal
{
    public int Id { get; set; }
    public int DayId { get; set; }
    public Day? Day { get; set; }
    public string? Exercise { get; set; }
    public string? Target { get; set; }
    public int? Repetitions { get; set; }
    public int? Sets { get; set; }
}

