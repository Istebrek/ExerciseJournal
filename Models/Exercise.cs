using System.Text.Json.Serialization;

namespace ExerciseJournalAPI.Models;

public class Exercise
{
    [JsonPropertyName("bodyPart")]
    public string BodyPart { get; set; }

    [JsonPropertyName("equipment")]
    public string Equipment { get; set; }

    [JsonPropertyName("gifUrl")]
    public string GifUrl { get; set; }

    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("target")]
    public string Target { get; set; }

    [JsonPropertyName("secondaryMuscles")]
    public List<string> SecondaryMuscles { get; set; }

    [JsonPropertyName("instructions")]
    public List<string> Instructions { get; set; }

    [JsonPropertyName("description")]
    public string Description { get; set; }

    [JsonPropertyName("difficulty")]
    public string Difficulty { get; set; }

    [JsonPropertyName("category")]
    public string Category { get; set; }
}
