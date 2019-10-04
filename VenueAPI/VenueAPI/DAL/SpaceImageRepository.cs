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
    public class SpaceImageRepository : ISpaceImageRepository
    {
        private readonly string _connectionString;

        public SpaceImageRepository(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("LocalSqlServer");
        }

        public async Task<int> AddSpaceImagesAsync(List<SpaceImageDto> spaceImageDtos)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                int resultCount = await con.InsertAsync(spaceImageDtos);

                if (resultCount == 0)
                    throw new HttpStatusCodeResponseException(HttpStatusCode.NotModified, $"Error inserting Space Images:\n{JsonConvert.SerializeObject(spaceImageDtos)}\n");

                return resultCount;
            }
        }

        public async Task<List<SpaceImageDto>> GetSpaceImagesAsync(Guid venueId, Guid spaceId, bool requestSpecificallyForSpaceImages = true)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                IEnumerable<SpaceImageDto> spaceImages = await con.QueryAsync<SpaceImageDto>("SELECT * FROM SpaceImage WHERE spaceId = @spaceId", new { spaceId });

                if (spaceImages == null || (requestSpecificallyForSpaceImages && spaceImages.Count() == 0))
                    throw new HttpStatusCodeResponseException(HttpStatusCode.NotModified);

                return spaceImages.ToList();
            }
        }              

        public async Task<bool> DeleteSpaceImagesAsync(List<SpaceImageDto> spaceImageDtos)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                bool result = await con.DeleteAsync(spaceImageDtos);

                if (!result)
                    throw new HttpStatusCodeResponseException(HttpStatusCode.NotModified);

                return true;
            }
        }

       /// <summary>
       /// Only exposed at Repository level
       /// </summary>
       /// <param name="spaceIds"></param>
       /// <param name="requestSpecificallyForSpaceImages"></param>
       /// <returns></returns>
        public async Task<List<SpaceImageDto>> GetSpaceImagesAsync(List<Guid> spaceIds, bool requestSpecificallyForSpaceImages = true)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                IEnumerable<SpaceImageDto> spaceImages = await con.QueryAsync<SpaceImageDto>("SELECT * FROM SpaceImage WHERE spaceId IN @spaceIds", new { spaceIds });

                if (spaceImages == null || (requestSpecificallyForSpaceImages && spaceImages.Count() == 0))
                    throw new HttpStatusCodeResponseException(HttpStatusCode.NotModified);

                return spaceImages.ToList();
            }
        }
    }
}
