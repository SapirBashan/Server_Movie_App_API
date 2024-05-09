namespace Movie_Fetch_API.Controllers
{
    // this interface is used to get the settings from the appsettings.json file
    public interface IDataSetSettings
    {
        string MovieCollectName { get; set; }
        string ConnectionStrings { get; set; }
        string DataBaseName { get; set; }
    }
}
