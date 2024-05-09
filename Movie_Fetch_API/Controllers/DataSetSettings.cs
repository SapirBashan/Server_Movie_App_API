namespace Movie_Fetch_API.Controllers
{
    // this class is used to get the settings from the appsettings.json file
    // it implements the IDataSetSettings interface
    public class DataSetSettings : IDataSetSettings
    {
        public string MovieCollectName { get; set; }
        public string ConnectionStrings { get; set; }
        public string DataBaseName { get; set; }

        public DataSetSettings()
        {
            // Set default values for properties
            MovieCollectName = "Watch List";
            ConnectionStrings = "mongodb+srv://sapirbashan1:123sapir456@cluster0.tity9pa.mongodb.net/?retryWrites=true&w=majority&appName=Cluster0";
            DataBaseName = "Movie";
        }

        public DataSetSettings(string movieCollectName = null, string connectionStrings = null, string dataBaseName = null)
        {
            // Set values for properties
            MovieCollectName = movieCollectName ?? "DefaultMovieCollectName";
            ConnectionStrings = connectionStrings ?? "DefaultConnectionStrings";
            DataBaseName = dataBaseName ?? "DefaultDataBaseName";
        }
    }
}
