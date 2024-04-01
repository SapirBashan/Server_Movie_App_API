namespace Movie_Fetch_API.Controllers
{
    public class DataSetSettings : IDataSetSettings
    {
        public string MovieCollectName { get; set; } = string.Empty;
        public string ConnectionStrings { get; set; } = string.Empty;
        public string DataBaseName { get; set; } = string.Empty;
    }
}