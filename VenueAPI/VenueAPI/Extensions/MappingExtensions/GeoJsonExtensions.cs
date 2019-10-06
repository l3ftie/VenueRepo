using MongoDB.Driver;
using MongoDB.Driver.GeoJsonObjectModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VenueAPI.Services.LocationIq;
using VLibraries.APIModels;
using VLibraries.APIModels.GeoJson;

namespace VenueAPI.Extensions
{
    public static class GeoJsonExtensions
    {
        public static VenueLocationGeoJson MapToGeoJson(this LocationIqReverseResponse locationIqResponse)
        {
            return new VenueLocationGeoJson
            {
                Type = GeoJsonType.Point,
                Coordinates = new double[]
                {
                        double.Parse(locationIqResponse.Longitude),
                        double.Parse(locationIqResponse.Latitude)
                }
            };
        }

        public static FilterDefinition<VenueLocation> CreateGeoJsonFilter(this double searchRadiusMiles, double latitude, double longitude)
        {
            GeoJsonPoint<GeoJson2DGeographicCoordinates> geoPoint = new GeoJsonPoint<GeoJson2DGeographicCoordinates>(new GeoJson2DGeographicCoordinates(longitude, latitude));

            double searchRadiusInMeters = searchRadiusMiles * 1609.344; //1609.344 Meters = 1 Mile

            FilterDefinition<VenueLocation> filter = Builders<VenueLocation>.Filter.Near("location", geoPoint, maxDistance: searchRadiusInMeters);

            return filter;
        }
    }
}
