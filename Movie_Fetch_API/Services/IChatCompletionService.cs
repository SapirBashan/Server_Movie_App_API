namespace OpenAI_ChatGPT
{
    // Interface for ChatCompletionService to get chat completion
    public interface IChatCompletionService
    {
        Task<string> GetChatCompletionAsync(string question);
    }
}