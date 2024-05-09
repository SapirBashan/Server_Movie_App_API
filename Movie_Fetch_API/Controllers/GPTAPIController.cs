using Microsoft.AspNetCore.Mvc;
using OpenAI_ChatGPT;

// This controller is responsible for handling the API requests and responses
public class ChatCompletionController : ControllerBase
{
    private readonly IChatCompletionService _chatCompletionService;

    // ctor
    public ChatCompletionController(IChatCompletionService chatCompletionService)
    {
        _chatCompletionService = chatCompletionService;
    }

    // This method is used to get the response from the GPT-3 model
    [HttpGet("api/movieRecommendation")]
    public async Task<IActionResult> Get(string question)
    {
        var response = await _chatCompletionService.GetChatCompletionAsync(question);
        return Ok(response);
    }
}
