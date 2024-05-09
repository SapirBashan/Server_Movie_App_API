// MovieController.cs
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

//this is the controller that will be used to fetch the movie data from the OMDB API
//it will take the title of the movie as a parameter and return the movie object
//it will also fetch additional data such as plot and rating using the IMDb ID
//the controller will return a 400 status code if the movie was not found
//the controller will return a 200 status code if the movie was found
//the controller will return the movie object if the movie was found
namespace Movie_Fetch_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OmdbController : ControllerBase
    {
        private readonly HttpClient _httpClient;

        //ctor
        public OmdbController()
        {
            _httpClient = new HttpClient();
        }

        //GET method to fetch the movie data from the OMDB API
        [HttpGet("{title}")]
        public async Task<IActionResult> Get(string title)
        {
            // Fetch movie data using title
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
