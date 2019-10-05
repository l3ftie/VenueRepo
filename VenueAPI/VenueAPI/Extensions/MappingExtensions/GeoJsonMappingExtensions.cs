using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VLibraries.APIModels;

namespace VenueAPI.Extensions
{
    public static class GeoJsonMappingExtensions
    {
        public static GeoJson MapToGeoJson(this LocationIqReverseResponse locationIqResponse)
        {
            return new GeoJson
            {
                Type = GeoJsonType.Point,
                Coordinates = new double[]
                {
                        double.Parse(locationIqResponse.Longitude),
                        double.Parse(locationIqResponse.Latitude)
                }
            };
        }
    }
}
