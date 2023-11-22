using Data.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace movieProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {


        private static List<string> searchHistory = new List<string>();
        private readonly string apiKey = "f51e7b79"; // Get your API key from http://www.omdbapi.com


        [HttpGet("search/{title}")]
        public async Task<IActionResult> SearchMovie(string title)
        {
            // Save search query to history
            searchHistory.Insert(0, title);
            if (searchHistory.Count > 5)
                searchHistory.RemoveAt(5);

            // Call OMDB API
            using (var client = new HttpClient())
            {
                var response = await client.GetStringAsync($"http://www.omdbapi.com/?apikey={apiKey}&t={title}");
                var result = JsonConvert.DeserializeObject<MovieDetails>(response);
                return Ok(ApiResponse.Success(result));
            }
        }





        [HttpGet("searchHistory")]
        public IActionResult GetSearchHistory()
        {
            return Ok(ApiResponse.Success(searchHistory.Take(5)));
        }





    }
}
