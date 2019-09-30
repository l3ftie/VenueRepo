﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Dapper;
using Dapper.Contrib.Extensions;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using VenueAPI.BLL;
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


        public async Task<SpaceDto> AddSpaceAsync(Space space, Guid venueId)
        {
            string insertSpaceSql =
            "DECLARE @TempTable table([SpaceId] [uniqueidentifier]); " +
            "INSERT INTO Space (MaxCapacity, VenueId) " +
            "   OUTPUT INSERTED.[SpaceId] INTO @TempTable " +
            "VALUES (@MaxCapacity, @VenueId);" +
            "SELECT [SpaceId] FROM @TempTable;";

            SpaceDto spaceDto = new SpaceDto
            {
                VenueId = venueId,
                MaxCapacity = space.MaxCapacity
            };

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                Guid insertedSpaceId = await con.QueryFirstOrDefaultAsync<Guid>(insertSpaceSql, spaceDto);

                if (insertedSpaceId == Guid.Empty)
                    throw new HttpStatusCodeResponseException(HttpStatusCode.NotModified, $"Error inserting Space:\n{JsonConvert.SerializeObject(space)}\ninto Venue: {venueId}");

                return await GetSpaceAsync(venueId, insertedSpaceId, false);
            }
        }

        public async Task<SpaceDto> GetSpaceAsync(Guid venueId, Guid spaceId, bool requestSpecificallyForSpaces = true)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                SpaceDto space = await con.QueryFirstAsync<SpaceDto>("SELECT * FROM Space WHERE spaceId = @spaceId", new { spaceId });

                if (space == null && requestSpecificallyForSpaces)
                    throw new HttpStatusCodeResponseException(HttpStatusCode.NotFound);

                space.SpaceImages = await _spaceImagesRepo.GetSpaceImagesAsync(venueId, spaceId, requestSpecificallyForSpaces);

                return space;
            }
        }

        public async Task<List<SpaceDto>> GetSpacesAsync(Guid venueId, bool requestSpecificallyForSpaces = true)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                IEnumerable<SpaceDto> spaces = await con.QueryAsync<SpaceDto>("SELECT * FROM Space WHERE venueId = @venueId", new { venueId });

                if (spaces == null || (requestSpecificallyForSpaces && spaces.Count() == 0))
                    throw new HttpStatusCodeResponseException(HttpStatusCode.NotFound);

                IEnumerable<SpaceImageDto> spaceImages = await _spaceImagesRepo.GetSpaceImagesAsync(spaces.Select(x => x.SpaceId).ToList(), false);             

                //Map to Model
                foreach (SpaceDto space in spaces)
                {
                    space.SpaceImages = spaceImages.Where(x => x?.SpaceId == space.SpaceId).ToList();
                }

                return spaces.ToList();
            }
        }
        
        public async Task<SpaceDto> EditSpaceAsync(Space space, Guid venueId, Guid spaceId)
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

                dto = await GetSpaceAsync(venueId, spaceId);

                return dto;
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


        public async Task<List<SpaceDto>> GetSpacesAsync(List<Guid> venueIds, bool requestSpecificallyForSpaces = true)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                IEnumerable<SpaceDto> spaces = await con.QueryAsync<SpaceDto>("SELECT * FROM Space WHERE venueId IN @venueIds", new { venueIds });

                if (spaces == null || (requestSpecificallyForSpaces && spaces.Count() == 0))
                    throw new HttpStatusCodeResponseException(HttpStatusCode.NotFound);

                IEnumerable<SpaceImageDto> spaceImages = await _spaceImagesRepo.GetSpaceImagesAsync(spaces.Select(x => x.SpaceId).ToList(), false);         

                //Map to Model
                foreach (SpaceDto space in spaces)
                {
                    space.SpaceImages = spaceImages.Where(x => x?.SpaceId == space.SpaceId).ToList();
                }

                return spaces.ToList();
            }
        }
    }
}
