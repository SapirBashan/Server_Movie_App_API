using Microsoft.AspNetCore.Mvc;
using OpenAI_ChatGPT;

public class ChatCompletionController : ControllerBase
{
    private readonly IChatCompletionService _chatCompletionService;

    public ChatCompletionController(IChatCompletionService chatCompletionService)
    {
        _chatCompletionService = chatCompletionService;
    }

    [HttpGet("Enter movies you like")]
    public async Task<IActionResult> Get(string question)
    {
        var response = await _chatCompletionService.GetChatCompletionAsync(question);
        return Ok(response);
    }
}
