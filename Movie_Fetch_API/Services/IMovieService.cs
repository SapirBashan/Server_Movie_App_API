namespace Movie_Fetch_API.Services
{
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
