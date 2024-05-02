using Microsoft.AspNetCore.Mvc;
using OpenAI_ChatGPT;

public class ChatCompletionController : ControllerBase
{
    private readonly IChatCompletionService _chatCompletionService;

    public ChatCompletionController(IChatCompletionService chatCompletionService)
    {
        _chatCompletionService = chatCompletionService;
    }

    [HttpGet("api/movieRecommendation")]
    public async Task<IActionResult> Get(string question)
    {
        var response = await _chatCompletionService.GetChatCompletionAsync(question);
        return Ok(response);
    }
}
