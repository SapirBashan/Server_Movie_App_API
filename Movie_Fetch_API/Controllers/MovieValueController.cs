using Microsoft.AspNetCore.Mvc;
using Movie_Fetch_API.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Movie_Fetch_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieValueController : ControllerBase
    {
        private readonly IMovieService movieService;

        public MovieValueController(IMovieService movieService)
        {
            this.movieService = movieService;
        }
        // GET: api/<MovieValueController>
        [HttpGet]
        public ActionResult<List<Movie>> Get()
        {
            return movieService.GetAll();
        }

        // GET api/<MovieValueController>/5
        [HttpGet("{key}")]
        public ActionResult<Movie> Get(string key)
        {
            var movie = movieService.Get(key);

            if( movie == null)
            {
                return NotFound($"movie with key = {key} not found");

            }
            return movie;
        }

        // POST api/<MovieValueController>
        [HttpPost]
        public ActionResult<Movie> Post([FromBody] Movie movie)
        {
            movieService.Create(movie);

            return CreatedAtAction(nameof(Get), new { key = movie.Key }, movie);
        }

        // PUT api/<MovieValueController>/5
        [HttpPut("{key}")]
        public ActionResult Put(string key, [FromBody] Movie movie)
        {
            var existingMovie = movieService.Get(key);

            if( existingMovie == null)
            {
                return NotFound($"movie with key = {key} not found");
            }

            movieService.Update(key, movie);

            //return NoContent
            return Content("put function succsesful");

        }

        // DELETE api/<MovieValueController>/5
        [HttpDelete("{key}")]
        public ActionResult Delete(string key)
        {

            var movie = movieService.Get(key);

            if (movie == null)
            {
                return NotFound($"movie with key = {key} not found");
            }

            movieService.Delete(movie.Key);

            //return NoContent
            return Ok($"movie with key = {key} removed");
        }
    }
}
