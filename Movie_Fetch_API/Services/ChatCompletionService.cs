using OpenAI_ChatGPT;
using System.Text;
using System.Text.Json;

public class ChatCompletionService : IChatCompletionService
{
    private readonly IConfiguration _configuration;
    private readonly IHttpClientFactory _httpClientFactory;

    public ChatCompletionService(IConfiguration configuration, IHttpClientFactory httpClientFactory)
    {
        _configuration = configuration;
        _httpClientFactory = httpClientFactory;
    }

    public async Task<string> GetChatCompletionAsync(string question)
    {
        var httpClient = _httpClientFactory.CreateClient("ChtpGPT");

        var completionRequest = new ChatCompletionRequest
        {
            Model = "gpt-3.5-turbo",
            MaxTokens = 200,
            Messages = new List<Message>
            {
                new Message
                {
                    Role = "user",
                    Content = "These are a few movies that the user likes " + question + 
                    "can you list 5 movies that the user might like based on the movies listed, start the answer with " +
                    "These are the 5 movies you might like based on your personal preferences:- than 5 movies with the acurate title of the movie " +
                    "whith the year and a varry short discription of the movie"
                }
            }
        };

        var httpReq = new HttpRequestMessage(HttpMethod.Post, "https://api.openai.com/v1/chat/completions");
        httpReq.Headers.Add("Authorization", $"Bearer {_configuration["OpenAIKey"]}");

        var requestString = JsonSerializer.Serialize(completionRequest);
        httpReq.Content = new StringContent(requestString, Encoding.UTF8, "application/json");

        using var httpResponse = await httpClient.SendAsync(httpReq);
        httpResponse.EnsureSuccessStatusCode();

        var completionResponse = httpResponse.IsSuccessStatusCode ? JsonSerializer.Deserialize<ChatCompletionResponse>(await httpResponse.Content.ReadAsStringAsync()) : null;

        return completionResponse?.Choices?[0]?.Message?.Content;
    }
}
