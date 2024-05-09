using Movie_Fetch_API.Controllers;
using MongoDB.Driver;

namespace Movie_Fetch_API.Services
{
    //this class is used to implement the IMovieService interface
    //this calss a service class that is used to interact with the database
    public class MovieService : IMovieService
    {
        private readonly IMongoCollection<Movie> _movies;

        //constructor
        public MovieService(IDataSetSettings settings, IMongoClient mongoClient)
        {
            var dataBase = mongoClient.GetDatabase(settings.DataBaseName);
            _movies = dataBase.GetCollection<Movie>(settings.MovieCollectName);
        }

        //methods to implement the interface
        public Movie Create(Movie movie)
        {
            _movies.InsertOne(movie);
            return movie;
        }

        public void Delete(string key)
        {
           _movies.DeleteOne(movie => movie.Key == key);

        }

        public Movie GetByTitle(string title)
        {
            return _movies.Find(movie => movie.Title == title).FirstOrDefault();
        }

        public Movie GetByKey(string key)
        {
            return _movies.Find(movie => movie.Key == key).FirstOrDefault();
        }

        public List<Movie> GetAll()
        {
            return _movies.Find(movie => true).ToList();
        }

        //this method is used to update the comment of a movie by its title
        public void Update(string title, string comment)
        {
            //find the movie by its title
            Movie myMovie = _movies.Find(movie => movie.Title == title).FirstOrDefault();
            if(myMovie == null)
            {
                return;
            }
            myMovie.Comment = comment;
            _movies.ReplaceOne(movie => movie.Title == title, myMovie);
        }
    }
}
