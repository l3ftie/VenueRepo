using MongoDB.Bson.Serialization.Attributes;

namespace VLibraries.APIModels
{
    public class VenueLocationGeoJson
    {
        [BsonElement("type")]
        public string Type { get; set; }

        [BsonElement("coordinates")]
        public double[] Coordinates { get; set; }
    }
}
