using Movie_Fetch_API.Controllers;
using MongoDB.Driver;

namespace Movie_Fetch_API.Services
{
    public class MovieService : IMovieService
    {
        private readonly IMongoCollection<Movie> _movies;

        public MovieService(IDataSetSettings settings, IMongoClient mongoClient)
        {
            var dataBase = mongoClient.GetDatabase(settings.DataBaseName);
            _movies = dataBase.GetCollection<Movie>(settings.MovieCollectName);

        }
        public Movie Create(Movie movie)
        {
            _movies.InsertOne(movie);
            return movie;
        }

        public void Delete(string key)
        {
           _movies.DeleteOne(movie => movie.Key == key);

        }

        public Movie Get(string key)
        {
            return _movies.Find(movie => movie.Key == key).FirstOrDefault();
        }

        public List<Movie> GetAll()
        {
            return _movies.Find(movie => true).ToList();
        }

        public void Update(string key, Movie movie)
        {
            _movies.ReplaceOne(movie => movie.Key == key, movie); 
        }
    }
}
