using Data.Dtos;
using Microsoft.AspNetCore.Mvc;
using movieProject.Model;
using movieProject.MovieServices;
using Newtonsoft.Json;

namespace movieProject.Service
{
    public class MovieService : IMovieService
    {
        private static List<string> searchHistory = new List<string>();
        private readonly string apiKey = "f51e7b79"; // Get your API key from http://www.omdbapi.com


        public async Task<Movie> MovieSearchAsync(string title)
        {
            // Saving the search query into the search history
            searchHistory.Insert(0, title);
            if (searchHistory.Count > 5)
                searchHistory.RemoveAt(5);

            // A call to OMDB API
            using (var client = new HttpClient())
            {
                var response = await client.GetStringAsync($"http://www.omdbapi.com/?apikey={apiKey}&t={title}");
                var result = JsonConvert.DeserializeObject<Movie>(response);
                return result;
            }
        }


        public  async Task<IEnumerable<string>> GetSearchHistoryAsync()
        {
            if (searchHistory.Count < 1)
            {
                return null;
            }
            return searchHistory.Take(5);
        }




    }
}
