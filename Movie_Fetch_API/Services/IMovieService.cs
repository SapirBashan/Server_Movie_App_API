namespace Movie_Fetch_API.Services
{
    //we used this interface to implement the MovieService class
    //the reason we used this interface is to make the code more readable and maintainable
    public interface IMovieService
    {
        List<Movie> GetAll();
        Movie GetByKey(string key);
        Movie GetByTitle(string title);
        Movie Create(Movie movie);
        void Update(string title, string comment);
        void Delete(string key);

    }
}
