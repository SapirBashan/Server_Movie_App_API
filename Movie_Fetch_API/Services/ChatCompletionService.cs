using OpenAI_ChatGPT;
using System.Text;
using System.Text.Json;

//implementation of IChatCompletionService
public class ChatCompletionService : IChatCompletionService
{
    private readonly IConfiguration _configuration;
    private readonly IHttpClientFactory _httpClientFactory;

    //ctor
    public ChatCompletionService(IConfiguration configuration, IHttpClientFactory httpClientFactory)
    {
        _configuration = configuration;
        _httpClientFactory = httpClientFactory;
    }

    //method to get chat completion
    public async Task<string> GetChatCompletionAsync(string question)
    {
        var httpClient = _httpClientFactory.CreateClient("ChtpGPT");

        var completionRequest = new ChatCompletionRequest
        {
            Model = "gpt-3.5-turbo",
            MaxTokens = 300,
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

        // Create a new HttpRequestMessage
        var httpReq = new HttpRequestMessage(HttpMethod.Post, "https://api.openai.com/v1/chat/completions");
        httpReq.Headers.Add("Authorization", $"Bearer {_configuration["OpenAIKey"]}");

        // Serialize the completionRequest object to a JSON string
        // the reason we serialize the request is to send the request to the API
        // the API accepts the request in JSON format so we need to serialize the request to send it to the API
        var requestString = JsonSerializer.Serialize(completionRequest);
        httpReq.Content = new StringContent(requestString, Encoding.UTF8, "application/json");

        // Send the request and get the response
        using var httpResponse = await httpClient.SendAsync(httpReq);
        httpResponse.EnsureSuccessStatusCode();

        // Deserialize the response to a ChatCompletionResponse object
        // the reason we are deserializing the response is to get the response from the API
        // the response from the API is in JSON format so we need to deserialize it to get the response
        var completionResponse = httpResponse.IsSuccessStatusCode ? JsonSerializer.Deserialize<ChatCompletionResponse>(await httpResponse.Content.ReadAsStringAsync()) : null;

        // Return the response
        return completionResponse?.Choices?[0]?.Message?.Content;
    }
}
