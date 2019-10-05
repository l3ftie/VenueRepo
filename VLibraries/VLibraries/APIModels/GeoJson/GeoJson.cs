using MongoDB.Bson.Serialization.Attributes;

namespace VLibraries.APIModels
{
    public class GeoJson
    {
        [BsonElement("type")]
        public string Type { get; set; }

        [BsonElement("coordinates")]
        public double[] Coordinates { get; set; }
    }
}
