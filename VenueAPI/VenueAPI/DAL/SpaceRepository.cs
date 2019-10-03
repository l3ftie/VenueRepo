using Dapper;
using Dapper.Contrib.Extensions;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using VenueAPI.MappingExtensions;
using VLibraries.APIModels;
using VLibraries.CustomExceptions;

namespace VenueAPI.DAL
{
    public class SpaceRepository : ISpaceRepository
    {
        private readonly string _connectionString;
        private readonly ISpaceImageRepository _spaceImagesRepo;

        public SpaceRepository(IConfiguration config, ISpaceImageRepository spaceImagesRepo)
        {
            _connectionString = config.GetConnectionString("LocalSqlServer");
            _spaceImagesRepo = spaceImagesRepo;
        }


        public async Task<SpaceResponse> AddSpaceAsync(SpaceRequest space, Guid venueId)
        {
            string insertSpaceSql =
            "DECLARE @TempTable table([SpaceId] [uniqueidentifier]); " +
            "INSERT INTO Space (MaxCapacity, VenueId, SpaceTypeId) " +
            "   OUTPUT INSERTED.[SpaceId] INTO @TempTable " +
            "VALUES (@MaxCapacity, @VenueId, @SpaceTypeId);" +
            "SELECT [SpaceId] FROM @TempTable;";

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                Guid insertedSpaceId = await con.QueryFirstOrDefaultAsync<Guid>(insertSpaceSql, new
                {
                    VenueId = venueId,
                    space.MaxCapacity,
                    space.SpaceTypeId
                });

                if (insertedSpaceId == Guid.Empty)
                    throw new HttpStatusCodeResponseException(HttpStatusCode.NotModified, $"Error inserting Space:\n{JsonConvert.SerializeObject(space)}\ninto Venue: {venueId}");

                return await GetSpaceAsync(venueId, insertedSpaceId, false);
            }
        }

        public async Task<SpaceResponse> GetSpaceAsync(Guid venueId, Guid spaceId, bool requestSpecificallyForSpaces = true)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                IEnumerable<SpaceDto> spaceDtos = await con.QueryAsync<SpaceDto>("SELECT S.SpaceId, S.VenueId, S.MaxCapacity, " +
                        "ST.SpaceTypeId, ST.Description AS SpaceTypeDescription, " +
                        "SI.SpaceImageId, SI.Base64SpaceImageString " +
                        "FROM [VenueFinder].[dbo].[Space] S " +
                        "JOIN [VenueFinder].[dbo].SpaceType ST " +
                        "ON ST.SpaceTypeId = S.SpaceTypeId " +
                        "LEFT OUTER JOIN [VenueFinder].[dbo].SpaceImage SI ON SI.SpaceId = S.SpaceId " +
                        "WHERE S.SpaceId = @spaceId;", new { spaceId });

                if (spaceDtos == null || (requestSpecificallyForSpaces && spaceDtos.Count() == 0))
                    throw new HttpStatusCodeResponseException(HttpStatusCode.NotFound);

                SpaceResponse model = spaceDtos.MapDtoToResponse();

                return model;
            }
        }

        public async Task<List<SpaceResponse>> GetSpacesAsync(Guid venueId, bool requestSpecificallyForSpaces = true)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                IEnumerable<SpaceDto> spaces = await con.QueryAsync<SpaceDto>("SELECT S.SpaceId, S.VenueId, S.MaxCapacity, " +
                        "ST.SpaceTypeId, ST.Description AS SpaceTypeDescription, " +
                        "SI.SpaceImageId, SI.Base64SpaceImageString " +
                        "FROM [VenueFinder].[dbo].[Space] S " +
                        "JOIN [VenueFinder].[dbo].SpaceType ST ON ST.SpaceTypeId = S.SpaceTypeId " +
                        "LEFT OUTER JOIN [VenueFinder].[dbo].SpaceImage SI ON SI.SpaceId = S.SpaceId " +
                        "WHERE S.VenueId = @venueId;", new { venueId });

                List<List<SpaceDto>> groupedSpaces = spaces.GroupBy(x => x.SpaceId).Select(y => y.ToList()).ToList();

                List<SpaceResponse> models = new List<SpaceResponse>();

                foreach (List<SpaceDto> spacesGroupedById in groupedSpaces)
                {
                    models.Add(spacesGroupedById.MapDtoToResponse());
                }

                return models.ToList();
            }
        }
        
        public async Task<SpaceResponse> EditSpaceAsync(SpaceRequest space, Guid venueId, Guid spaceId)
        {
            SpaceDto dto = new SpaceDto
            {
                SpaceId = spaceId,
                VenueId = venueId,
                MaxCapacity = space.MaxCapacity
            };

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                bool updateSpaceResult = await con.UpdateAsync(dto);

                if (!updateSpaceResult)
                    throw new HttpStatusCodeResponseException(HttpStatusCode.NotModified);

                SpaceResponse model = await GetSpaceAsync(venueId, spaceId);

                return model;
            }
        }

        public async Task<bool> DeleteSpaceAsync(Guid venueId, Guid spaceId)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                List<SpaceImageDto> spaceImageDtos = await _spaceImagesRepo.GetSpaceImagesAsync(venueId, spaceId);

                bool result1 = await con.DeleteAsync(spaceImageDtos);

                bool result = await con.DeleteAsync(new SpaceDto { SpaceId = spaceId, VenueId = venueId });

                if (!result)
                    throw new HttpStatusCodeResponseException(HttpStatusCode.NotModified);

                return result;
            }
        }

        public async Task<SpaceResponse> UpsertSpaceType(Guid venueId, Guid spaceId, SpaceTypeDto spaceType)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                int updateSpaceTypeResult = await con.ExecuteAsync("UPDATE Space SET SpaceTypeId = @spaceTypeId WHERE SpaceId = @spaceId", new { spaceTypeId = spaceType.SpaceTypeId, spaceId });

                if (updateSpaceTypeResult == 0)
                    throw new HttpStatusCodeResponseException(HttpStatusCode.NotFound);
            }

            SpaceResponse model = await GetSpaceAsync(venueId, spaceId, false);

            return model;
        }

        //Only at Repo level
        public async Task<List<SpaceDto>> GetSpacesAsync(List<Guid> venueIds, bool requestSpecificallyForSpaces = true)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                IEnumerable<SpaceDto> spaces = await con.QueryAsync<SpaceDto>("SELECT * FROM Space WHERE venueId IN @venueIds", new { venueIds });

                if (spaces == null || (requestSpecificallyForSpaces && spaces.Count() == 0))
                    throw new HttpStatusCodeResponseException(HttpStatusCode.NotFound);
                
                List<List<SpaceDto>> groupedSpacesbyVenueId = spaces.GroupBy(x => x.VenueId).Select(y => y.ToList()).ToList();

                foreach (List<SpaceDto> groupedSpace in groupedSpacesbyVenueId)
                {
                    List<List<SpaceDto>> groupedSpacesBySpaceId = spaces.GroupBy(x => x.SpaceId).Select(y => y.ToList()).ToList();

                    List<SpaceResponse> models = new List<SpaceResponse>();

                    foreach (List<SpaceDto> spacesGroupedById in groupedSpacesbyVenueId)
                    {
                        models.Add(spacesGroupedById.MapDtoToResponse());
                    }
                }                

                return spaces.ToList();
            }
        }       
    }
}
