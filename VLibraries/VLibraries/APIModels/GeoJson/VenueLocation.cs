using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace VLibraries.APIModels.GeoJson
{
    public class VenueLocation
    {
        [BsonElement("venueId")]
        public Guid VenueId { get; set; }

        [BsonElement("location")]
        public VenueLocationGeoJson Location { get; set; }
    }
}
