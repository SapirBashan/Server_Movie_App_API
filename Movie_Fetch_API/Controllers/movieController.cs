// MovieController.cs
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Movie_Fetch_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MovieController : ControllerBase
    {
        private readonly HttpClient _httpClient;

        public MovieController()
        {
            _httpClient = new HttpClient();
        }

        [HttpGet("{title}")]
        public async Task<IActionResult> Get(string title)
        {
            var response = await _httpClient.GetAsync($"http://www.omdbapi.com/?apikey=b23f75aa&t={title}&r=json");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var movie = JsonSerializer.Deserialize<Movie>(content);

                // Fetch additional data if the movie was found
                if (movie != null)
                {
                    // Fetch plot and rating using IMDb ID
                    var id = movie.imdbID;
                    var detailsResponse = await _httpClient.GetAsync($"http://www.omdbapi.com/?apikey=b23f75aa&i={id}&plot=full&r=json");
                    if (detailsResponse.IsSuccessStatusCode)
                    {
                        var detailsContent = await detailsResponse.Content.ReadAsStringAsync();
                        var details = JsonSerializer.Deserialize<Movie>(detailsContent);

                        // Update movie object with additional details
                        movie.Plot = details.Plot;
                    }
                }

                return Ok(movie);
            }

            return BadRequest();
        }
    }
}
