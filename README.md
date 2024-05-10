
# Movie Fetch API

This project is a movie fetch API application built using C# .NET Core for the backend and integrated with external APIs such as OMDB API for movie data and OpenAI API for chat completion functionality. It also includes a PyQt frontend for managing movies and getting movie recommendations.



https://github.com/SapirBashan/Server_Movie_App_API/assets/99900812/e7434959-b8f9-4a14-8e46-9d0a25c25ba8

##Link to the PyQt frontend
https://github.com/SapirBashan/PyQt_frontend_Movie_APP

## Features

- **CRUD Operations**: The API allows users to perform CRUD operations on movies, including adding, removing, updating, and fetching movies.
- **Integration with OMDB API**: The application integrates with the OMDB API to fetch movie data based on user input.
- **Integration with OpenAI API**: The API includes functionality to generate movie recommendations using the OpenAI API's chat completion feature.
- **MongoDB Integration**: The application uses MongoDB as a database to store movie information, leveraging the flexibility and scalability of a NoSQL database.
- **MVC Architecture**: The backend follows the Model-View-Controller (MVC) architectural pattern for separation of concerns and improved maintainability.
- **Repository Pattern**: The application utilizes the Repository pattern to abstract data access and improve testability and code organization.
- **Docker Support**: The Dockerfile provided allows for easy containerization and deployment of the application.


## MongoDB Integration

The application leverages MongoDB as the database for storing movie information. MongoDB provides a flexible and scalable NoSQL solution, allowing efficient storage and retrieval of movie data. By integrating MongoDB into the backend, the application benefits from:

- **Scalability**: MongoDB's distributed architecture enables seamless scaling of the database as data volume increases.
- **Flexibility**: The schema-less nature of MongoDB allows for easy modification of data structures without downtime.
- **Performance**: MongoDB's indexing and query optimization capabilities ensure fast and efficient data retrieval.
- **Data Persistence**: MongoDB ensures data persistence, durability, and high availability, critical for a reliable application.

The backend interacts with MongoDB to perform CRUD operations, enabling seamless storage and management of movie data within the application.

1. **MongoDB Integration for Movie Storage:**
```csharp
using MongoDB.Driver;

namespace Movie_Fetch_API.Services
{
    // Implementation of IMovieService using MongoDB
    public class MovieService : IMovieService
    {
        private readonly IMongoCollection<Movie> _movies;

        public MovieService(IDataSetSettings settings, IMongoClient mongoClient)
        {
            var dataBase = mongoClient.GetDatabase(settings.DataBaseName);
            _movies = dataBase.GetCollection<Movie>(settings.MovieCollectName);
        }

        // Methods to implement the IMovieService interface
        public Movie Create(Movie movie)
        {
            _movies.InsertOne(movie);
            return movie;
        }

        // Other CRUD methods omitted for brevity
    }
}
```
In this snippet, `MovieService` class implements the `IMovieService` interface for CRUD operations with MongoDB.

2. **OpenAI Chat Bot Integration:**
```csharp
using OpenAI_ChatGPT;
using System.Threading.Tasks;

public class ChatCompletionService : IChatCompletionService
{
    public async Task<string> GetChatCompletionAsync(string question)
    {
        // Initialization and configuration
        var completionRequest = new ChatCompletionRequest
        {
            Model = "gpt-3.5-turbo",
            MaxTokens = 300,
            // Construct chat request based on user question
        };

        // Send request to OpenAI API
        var completionResponse = await SendCompletionRequest(completionRequest);

        // Process and return the response
        return completionResponse?.Choices?[0]?.Message?.Content;
    }

    private async Task<ChatCompletionResponse> SendCompletionRequest(ChatCompletionRequest request)
    {
        // Code to send HTTP request to OpenAI API
        // and deserialize the response
    }
}
```
This code snippet showcases the integration of the OpenAI ChatGPT API for generating chat completions based on user queries.

These snippets showcase different aspects of your C# .NET Core application, including MongoDB integration, OpenAI API usage.
---

Feel free to further customize or expand upon this section based on specific MongoDB features or configurations used in your project!

## Backend (C# .NET Core)

### Dependencies

- .NET 6.0 SDK: For building and running the .NET Core application.
- MongoDB: NoSQL database used for storing movie information.

### Installation and Usage

1. Clone the repository:

   ```bash
   git clone https://github.com/your-username/movie-fetch-api.git
   ```

2. Navigate to the project directory:

   ```bash
   cd movie-fetch-api/Movie_Fetch_API
   ```

3. Set up your MongoDB connection string in the `appsettings.json` file.

4. Build and run the application:

   ```bash
   dotnet build
   dotnet run
   ```

5. The API endpoints will be available at `https://localhost:5001`.

### API Endpoints

- `GET /api/MovieValue`: Get all movies.
- `GET /api/MovieValue/{title}`: Get a movie by title.
- `POST /api/MovieValue`: Add a new movie.
- `PUT /api/MovieValue?title={title}`: Update a movie's comment by title.
- `DELETE /api/MovieValue/{title}`: Delete a movie by title.
- `GET /Omdb/{movieName}`: Fetch movie data from the OMDB API.
- `GET /api/movieRecommendation?question={movieName}`: Get movie recommendations using the OpenAI API.

### Docker

The Dockerfile provided allows you to build and run the application in a Docker container. Follow these steps:

1. Build the Docker image:

   ```bash
   docker build -t movie-fetch-api .
   ```

2. Run the Docker container:

   ```bash
   docker run -d -p 80:80 --name movie-fetch-container movie-fetch-api
   ```

3. Access the API endpoints at `http://localhost:80/api/MovieValue`.

## Frontend (PyQt)

The frontend of the application is built using PyQt for the graphical user interface. It includes features such as adding, removing, searching, and updating movies in a watchlist, as well as getting movie recommendations based on user preferences using Chat GPT models.

For detailed instructions on setting up and running the PyQt frontend, refer to the README in the PyQt project directory.

## Contribution Guidelines

Contributions to this project are welcome! Please follow these steps:

1. Fork the repository.
2. Create a new branch (`git checkout -b feature/your-feature`).
3. Commit your changes (`git commit -am 'Add new feature'`).
4. Push to the branch (`git push origin feature/your-feature`).
5. Create a new Pull Request.
