using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OpenAI_API;
using OpenAI_API.Completions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using System.Text.Json;

namespace ChatGPT_CSharp.Controllers
{
    [ApiController]
    public class GPTController : ControllerBase
    {
        private readonly OpenAIAPI openai;
        private readonly IConfiguration _configuration;

        public GPTController(IConfiguration configuration)
        {
            _configuration = configuration;
            string openAIKey = _configuration["OpenAIKey"];
            openai = new OpenAIAPI(openAIKey);
        }

            [HttpGet]
        [Route("GetMovieSuggestions")]
        public async Task<IActionResult> GetMovieSuggestions(string movies)
        {
            if (string.IsNullOrWhiteSpace(movies))
            {
                return BadRequest("Please provide a list of movies.");
            }

            List<string> movieList = movies.Split(',').Select(m => m.Trim()).ToList();

            // Ensure we have at least 3 movies for meaningful suggestions
            if (movieList.Count < 3)
            {
                return BadRequest("Please provide at least 3 movies for meaningful suggestions.");
            }

            string prompt = "Based on the fact that I like these following movies: " + string.Join(", ", movieList) +
                            ", suggest only 5 more movies that I might enjoy.";

            // Customize the completion request as needed
            CompletionRequest completionRequest = new CompletionRequest()
            {
                Prompt = prompt,
                Model = OpenAI_API.Models.Model.Davinci, 
                MaxTokens = 60,
                Temperature = 0.8, // Adjust as needed
                TopP = 1.0 // Adjust as needed
            };
                // Get completions from OpenAI API
                var completions = await openai.Completions.CreateCompletionAsync(completionRequest);

                // Extract text from completions
                List<string> suggestions = completions.Completions.Select(c => c.Text.Trim()).ToList();

                // Ensure we have at least 5 suggestions
                while (suggestions.Count < 5)
                {
                    completions = await openai.Completions.CreateCompletionAsync(completionRequest);
                    suggestions.AddRange(completions.Completions.Select(c => c.Text.Trim()));
                }

                // Extract the top 5 suggestions
                List<string> top5Suggestions = suggestions.Take(5).ToList();

                // Format the suggestions as per your requirement
                string formattedSuggestions = "These are the 5 movies you might like based on your personal preferences:\n";
                for (int i = 0; i < top5Suggestions.Count; i++)
                {
                    formattedSuggestions += $"{i + 1}. {top5Suggestions[i]}\n";
                }

                return Ok(formattedSuggestions);
            }
        }
    }

