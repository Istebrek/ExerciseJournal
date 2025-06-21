using ExerciseJournalAPI.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;


namespace ExerciseJournalAPI.Service;

public class ExerciseAPI
{
    private readonly HttpClient client;

    public ExerciseAPI (HttpClient _client)
    {
        client = _client;
    }

    public async Task<List<Exercise>> GetExercises()
    {
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri("https://exercisedb.p.rapidapi.com/exercises?limit=10&offset=0"),
            Headers =
            {
                { "x-rapidapi-key", "07a44e86bdmsh91bc25da782761cp12d213jsn0b3031406052" },
                { "x-rapidapi-host", "exercisedb.p.rapidapi.com" },
            },
        };
        using (var response = await client.SendAsync(request))
        {
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<Exercise>>(json);
        }
    }
}
