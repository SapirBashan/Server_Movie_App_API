// Movie.cs
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace Movie_Fetch_API
{
    public class Movie
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Key { get; set; }

        [BsonElement("title")]
        public string? Title { get; set; }

        [BsonElement("year")]
        public string? Year { get; set; }

        [BsonElement("imdbID")]
        public string? imdbID { get; set; }

        [BsonElement("rated")]
        public string? Rated { get; set; }

        [BsonElement("type")]
        public string? Type { get; set; }

        [BsonElement("poster")]
        public string? Poster { get; set; }

        [BsonElement("plot")]
        public string? Plot { get; set; }

        [BsonElement("imdbRating")]
        public string? imdbRating { get; set; }

        [BsonElement("comment")]
        public string? Comment { get; set;}
    }
}
