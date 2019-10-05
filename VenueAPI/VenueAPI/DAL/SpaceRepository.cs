using Dapper;
using Dapper.Contrib.Extensions;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using VenueAPI.Extensions;
using VLibraries.APIModels;
using VLibraries.CustomExceptions;

namespace VenueAPI.DAL
{
    public class SpaceRepository : ISpaceRepository
    {
        private readonly string _connectionString;

        public SpaceRepository(IConfiguration config, ISpaceImageRepository spaceImagesRepo)
        {
            _connectionString = config.GetConnectionString("LocalSqlServer");
        }


        public async Task<Guid> AddSpaceAsync(SpaceDto spaceDto)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                Guid insertedSpaceId = await con.QueryFirstOrDefaultAsync<Guid>("DECLARE @TempTable table([SpaceId] [uniqueidentifier]); " +
                    "INSERT INTO Space (Title, Description, Summary, Murl, MaxCapacity, VenueId, SpaceTypeId) " +
                    "   OUTPUT INSERTED.[SpaceId] INTO @TempTable " +
                    "VALUES (@Title, @Description, @Summary, @Murl, @MaxCapacity, @VenueId, @SpaceTypeId);" +
                    "SELECT [SpaceId] FROM @TempTable;", spaceDto);

                if (insertedSpaceId == Guid.Empty)
                    throw new HttpStatusCodeResponseException(HttpStatusCode.NotModified, $"Error inserting Space:\n{JsonConvert.SerializeObject(spaceDto)}\n");

                return insertedSpaceId;
            }
        }

        public async Task<List<SpaceDto>> GetSpaceAsync(Guid venueId, Guid spaceId, bool requestSpecificallyForSpaces = true)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                IEnumerable<SpaceDto> spaceDtos = await con.QueryAsync<SpaceDto>("SELECT S.SpaceId, S.VenueId, S.MaxCapacity, S.Title, S.Description, S.Summary, S.Murl, " +
                        "ST.SpaceTypeId, ST.Description AS SpaceTypeDescription, " +
                        "SI.SpaceImageId, SI.Base64SpaceImageString " +
                        "FROM [VenueFinder].[dbo].[Space] S " +
                        "JOIN [VenueFinder].[dbo].SpaceType ST " +
                        "ON ST.SpaceTypeId = S.SpaceTypeId " +
                        "LEFT OUTER JOIN [VenueFinder].[dbo].SpaceImage SI ON SI.SpaceId = S.SpaceId " +
                        "WHERE S.SpaceId = @spaceId " +
                        "AND S.VenueId = @venueId;", 
                        new { spaceId, venueId });

                if (spaceDtos == null || (requestSpecificallyForSpaces && spaceDtos.Count() == 0))
                    throw new HttpStatusCodeResponseException(HttpStatusCode.NotFound);

                return spaceDtos.ToList();
            }
        }

        public async Task<List<SpaceDto>> GetSpacesAsync(Guid venueId, bool requestSpecificallyForSpaces = true)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                IEnumerable<SpaceDto> spaceDtos = await con.QueryAsync<SpaceDto>("SELECT S.SpaceId, S.VenueId, S.MaxCapacity, S.Title, S.Description, S.Summary, S.Murl, " +
                        "ST.SpaceTypeId, ST.Description AS SpaceTypeDescription, " +
                        "SI.SpaceImageId, SI.Base64SpaceImageString " +
                        "FROM [VenueFinder].[dbo].[Space] S " +
                        "JOIN [VenueFinder].[dbo].SpaceType ST ON ST.SpaceTypeId = S.SpaceTypeId " +
                        "LEFT OUTER JOIN [VenueFinder].[dbo].SpaceImage SI ON SI.SpaceId = S.SpaceId " +
                        "WHERE S.VenueId = @venueId;", new { venueId });

                if (spaceDtos == null || (requestSpecificallyForSpaces && spaceDtos.Count() == 0))
                    throw new HttpStatusCodeResponseException(HttpStatusCode.NotFound);

                return spaceDtos.ToList();               
            }
        }        

        public async Task<bool> EditSpaceAsync(SpaceDto spaceDto)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                bool updateSpaceResult = await con.UpdateAsync(spaceDto);

                if (!updateSpaceResult)
                    throw new HttpStatusCodeResponseException(HttpStatusCode.NotModified);

                return updateSpaceResult;                
            }
        }

        public async Task<bool> DeleteSpaceAsync(Guid venueId, Guid spaceId)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {                
                bool result = await con.DeleteAsync(new SpaceDto { SpaceId = spaceId, VenueId = venueId });

                if (!result)
                    throw new HttpStatusCodeResponseException(HttpStatusCode.NotModified);

                return result;
            }
        }

        /// <summary>
        /// This method is not exposed at API level
        /// </summary>
        /// <param name="venueIds"></param>
        /// <param name="requestSpecificallyForSpaces"></param>
        /// <returns></returns>
        public async Task<List<SpaceDto>> GetSpacesAsync(List<Guid> venueIds, bool requestSpecificallyForSpaces = true)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                IEnumerable<SpaceDto> spaceDtos = await con.QueryAsync<SpaceDto>("SELECT S.SpaceId, S.VenueId, S.MaxCapacity, S.Title, S.Description, S.Summary, S.Murl, " +
                        "ST.SpaceTypeId, ST.Description AS SpaceTypeDescription, " +
                        "SI.SpaceImageId, SI.Base64SpaceImageString " +
                        "FROM [VenueFinder].[dbo].[Space] S " +
                        "JOIN [VenueFinder].[dbo].SpaceType ST ON ST.SpaceTypeId = S.SpaceTypeId " +
                        "LEFT OUTER JOIN [VenueFinder].[dbo].SpaceImage SI ON SI.SpaceId = S.SpaceId " +
                        "WHERE S.VenueId IN @venueIds;", new { venueIds });

                if (spaceDtos == null || (requestSpecificallyForSpaces && spaceDtos.Count() == 0))
                    throw new HttpStatusCodeResponseException(HttpStatusCode.NotFound);

                return spaceDtos.ToList();
            }
        }
    }
}
