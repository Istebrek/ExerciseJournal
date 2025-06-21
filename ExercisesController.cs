using ExerciseJournalAPI.Models;
using ExerciseJournalAPI.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExerciseJournalAPI
{
    [Route("api/exercises")]
    [ApiController]
    public class ExercisesController : ControllerBase
    {
        private readonly ExerciseAPI service;

        public ExercisesController(ExerciseAPI _service)
        {
            service = _service;
        }

        [HttpGet("search")]
        public async Task<ActionResult<List<Exercise>>> SearchExercises(string query)
        {
            var all = await service.GetExercises(); // optionally cache this
            var result = all
                .Where(e => e.Name.Contains(query, StringComparison.OrdinalIgnoreCase))
                .ToList();

            return Ok(result);
        }
    }
}
