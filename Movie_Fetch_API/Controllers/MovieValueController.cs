using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson.IO;
using Movie_Fetch_API.Services;
using System;

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
            try
            {
                return movieService.GetAll();
            }
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately
                return StatusCode(500, "An error occurred while retrieving movies.");
            }
        }

        // GET api/<MovieValueController>/5
        [HttpGet("{title}")]
        public ActionResult<Movie> Get(string title)
        {
            try
            {
                var movie = movieService.GetByTitle(title);

                if (movie == null)
                {
                    return NotFound($"{title} movie not found");
                }

                return movie;
            }
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately
                return StatusCode(500, "An error occurred while retrieving the movie.");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Movie>> Post([FromBody] Movie movie)
        {
            try
            {
                if (movie == null)
                {
                    return BadRequest("Invalid movie data provided");
                }

                movieService.Create(movie);

                return CreatedAtAction(nameof(Get), new { key = movie.Key }, movie);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately
                return StatusCode(500, "An error occurred while creating the movie.");
            }
        }

        //// PUT api/<MovieValueController>/5
        //[HttpPut("{key}")]
        //public ActionResult Update(string key, [FromBody] Movie movie)
        //{
        //    var existingMovie = movieService.GetByKey(key);

        //    if( existingMovie == null)
        //    {
        //        return NotFound($"movie with key = {key} not found");
        //    }

        //    movieService.Update(key, movie);

        //    //return NoContent
        //    return Content("update function succsesful");

        //}

        // DELETE api/<MovieValueController>/5

        [HttpDelete("{title}")]
        public ActionResult Delete(string title)
        {
            try
            {
                var movie = movieService.GetByTitle(title);

                if (movie == null)
                {
                    return NotFound($"Movie with title = {title} not found");
                }

                movieService.Delete(movie.Key);

                return Ok($"Movie with title = {title} removed");
            }
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately
                return StatusCode(500, "An error occurred while deleting the movie.");
            }
        }
    }
}

