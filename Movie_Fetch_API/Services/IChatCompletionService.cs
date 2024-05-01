namespace OpenAI_ChatGPT
{
    public interface IChatCompletionService
    {
        Task<string> GetChatCompletionAsync(string question);
    }
}