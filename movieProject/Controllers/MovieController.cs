using Data.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using movieProject.Model;
using movieProject.MovieServices;
using Newtonsoft.Json;

namespace movieProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private  readonly IMovieService _movieService;

        public MovieController(IMovieService movieService)
        {
            _movieService = movieService;
        }


        [HttpGet("search/{title}")]
        public async Task<IActionResult> MovieSearch(string title)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(title))
                {
                    return BadRequest("Please supply a valid movie name");
                } 
                var result = await _movieService.MovieSearchAsync(title);
                return Ok(JemmimahApiResponse.Success(result));
            }
            catch(Exception ex) 
            {
                return BadRequest("An error occured while fetching your movie.");
            }                         
        }





        [HttpGet("searchHistory")]
        public async Task<IActionResult> GetSearchHistory()
        {
            try
            {
                var search = await _movieService.GetSearchHistoryAsync();
                if(search == null)
                {
                    return NotFound(JemmimahApiResponse.Failed("No recent searches found."));
                }
                return Ok(JemmimahApiResponse.Success(search));
            }
            catch(Exception ex) 
            {
                return BadRequest("Oops, Please Try again later.");
            }
        }





    }
}
