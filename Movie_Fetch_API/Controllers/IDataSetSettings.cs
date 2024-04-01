namespace Movie_Fetch_API.Controllers
{
    public interface IDataSetSettings
    {
        string MovieCollectName { get; set; }
        string ConnectionStrings { get; set; }
        string DataBaseName { get; set; }
    }
}
