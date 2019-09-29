using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Dapper;
using Dapper.Contrib.Extensions;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using VLibraries.APIModels;
using VLibraries.CustomExceptions;

namespace VenueAPI.DAL
{
    public class VenueImageRepository : IVenueImageRepository
    {
        private readonly string _connectionString;

        public VenueImageRepository(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("LocalSqlServer");
        }

        public async Task<List<VenueImageDto>> AddVenueImagesAsync(List<string> base64EncodedVenueImages, Guid venueId)
        {
            List<VenueImageDto> venueImageDtos = new List<VenueImageDto>();

            foreach (string img in base64EncodedVenueImages)
            {
                venueImageDtos.Add(new VenueImageDto
                {
                    Base64VenueImageString = img,
                    VenueId = venueId
                });
            }

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                int resultCount = await con.InsertAsync(venueImageDtos);

                if (resultCount == 0)
                    throw new HttpStatusCodeResponseException(HttpStatusCode.NotModified, $"Error inserting Venue Images:\n{JsonConvert.SerializeObject(venueImageDtos)}\ninto Venue: {venueId}");

                venueImageDtos = await GetVenueImagesAsync(venueId);

                return venueImageDtos;
            }
        }

        public async Task<List<VenueImageDto>> GetVenueImagesAsync(Guid venueId)
        {
            string getVenueImagesByVenueIdSql = "SELECT * FROM VenueImage WHERE venueId = @venueId";

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                IEnumerable<VenueImageDto> venueImages = await con.QueryAsync<VenueImageDto>(getVenueImagesByVenueIdSql, new { venueId });

                if (venueImages == null)
                    throw new HttpStatusCodeResponseException(HttpStatusCode.NotModified);

                return venueImages.ToList();
            }
        }

        public async Task<bool> DeleteVenueImagesAsync(List<Guid> venueImageIds, Guid venueId)
        {
            List<VenueImageDto> venueImageDtos = new List<VenueImageDto>();

            venueImageIds.ForEach(x => venueImageDtos.Add(new VenueImageDto { VenueId = venueId, VenueImageId = x }));

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                bool result = await con.DeleteAsync(venueImageDtos);

                if (!result)
                    throw new HttpStatusCodeResponseException(HttpStatusCode.NotModified);

                return true;
            }
        }
    }
}
