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
    public class VenueRepository : IVenueRepository
    {
        private readonly string _connectionString;

        public VenueRepository(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("LocalSqlServer");
        }
        
        public async Task<VenueDto> AddVenueAsync(Venue venue)
        {
            //Need to use manual SQL (Not Dapper.Contrib extensions) to get the venueID after insert in 1 call

            string insertVenueSql =
            "DECLARE @TempTable table([VenueId] [uniqueidentifier]); " +
            "INSERT INTO Venue (Title, Description, Summary, Testimonial, TestimonialContactName, " +
            "TestimonialContactOrganisation, TestimonialContactEmail, MUrl) " +
            "   OUTPUT INSERTED.[VenueId] INTO @TempTable " +
            "VALUES (@Title, @Description, @Summary, @Testimonial, @TestimonialContactName, " +
            "@TestimonialContactOrganisation, @TestimonialContactEmail, @MUrl);" +
            "SELECT [VenueId] FROM @TempTable;";

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                Guid insertedVenueId = await con.QueryFirstOrDefaultAsync<Guid>(insertVenueSql, venue);

                if (insertedVenueId == Guid.Empty)
                    throw new HttpStatusCodeResponseException(HttpStatusCode.NotModified, $"Error inserting Venue:\n{JsonConvert.SerializeObject(venue)}");

                return await GetVenueAsync(insertedVenueId, true);
            }
        }
        
        public async Task<VenueDto> GetVenueAsync(Guid venueId, bool initialInsert = false)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                VenueDto venueDto = await con.GetAsync<VenueDto>(venueId);

                if (venueDto == null)
                    throw new HttpStatusCodeResponseException(HttpStatusCode.NotFound);

                venueDto.VenueImages = await GetVenueImagesAsync(venueId);

                if (!initialInsert)
                    venueDto.Spaces = await GetSpacesAsync(venueId, initialInsert);

                return venueDto;
            }
        }

        public async Task<List<VenueDto>> GetVenuesAsync()
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                IEnumerable<VenueDto> venueDtos = await con.GetAllAsync<VenueDto>();

                foreach (VenueDto venue in venueDtos)
                {
                    venue.VenueImages = await GetVenueImagesAsync(venue.VenueId);

                    venue.Spaces = await GetSpacesAsync(venue.VenueId, false);
                }

                if (venueDtos == null || venueDtos.Count() == 0)
                    throw new HttpStatusCodeResponseException(HttpStatusCode.NotFound);

                return venueDtos.ToList();
            }
        }

        public async Task<VenueDto> EditVenueAsync(Venue venue, Guid venueId)
        {
            VenueDto venueDto = new VenueDto
            {
                VenueId = venueId,
                Title = venue.Title,
                Description = venue.Description,
                MUrl = venue.MUrl,
                Summary = venue.Summary,
                Testimonial = venue.Testimonial,
                TestimonialContactEmail = venue.TestimonialContactEmail,
                TestimonialContactName = venue.TestimonialContactName,
                TestimonialContactOrganisation = venue.TestimonialContactOrganisation
            };

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                bool updateVenueResult = await con.UpdateAsync(venueDto);

                if (!updateVenueResult)
                    throw new HttpStatusCodeResponseException(HttpStatusCode.NotModified);

                venueDto = await GetVenueAsync(venueId);

                return venueDto;
            }
        }

        public async Task<bool> DeleteVenueAsync(Guid venueId)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                List<VenueImageDto> venueImageDtos = await GetVenueImagesAsync(venueId);

                //Delete images first or foreign key constraint fails at DB level
                bool deleteImagesResult = await con.DeleteAsync(venueImageDtos);

                bool deleteVenueResult = await con.DeleteAsync(new VenueDto { VenueId = venueId });

                if (!deleteVenueResult)
                    throw new HttpStatusCodeResponseException(HttpStatusCode.NotModified);

                return deleteVenueResult;
            }
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
            string getSpaceByIdSql = "SELECT * FROM Space WHERE spaceId = @spaceId";

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                SpaceDto space = await con.QueryFirstAsync<SpaceDto>(getSpaceByIdSql, new { spaceId });

                if (space == null && requestSpecificallyForSpaces)
                    throw new HttpStatusCodeResponseException(HttpStatusCode.NotFound);

                space.SpaceImages = await GetSpaceImagesAsync(venueId, spaceId, requestSpecificallyForSpaces);

                return space;
            }
        }

        public async Task<List<SpaceDto>> GetSpacesAsync(Guid venueId, bool requestSpecificallyForSpaces = true)
        {
            string getSpacesByVenueIdSql = "SELECT * FROM Space WHERE venueId = @venueId";

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                IEnumerable<SpaceDto> spaces = await con.QueryAsync<SpaceDto>(getSpacesByVenueIdSql, new { venueId });

                if (spaces == null || (requestSpecificallyForSpaces && spaces.Count() == 0))
                    throw new HttpStatusCodeResponseException(HttpStatusCode.NotFound);

                foreach(SpaceDto space in spaces)
                {
                    space.SpaceImages = await GetSpaceImagesAsync(venueId, space.SpaceId, false);
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
                List<SpaceImageDto> spaceImageDtos = await GetSpaceImagesAsync(venueId, spaceId);

                bool result1 = await con.DeleteAsync(spaceImageDtos);

                bool result = await con.DeleteAsync(new SpaceDto { SpaceId = spaceId, VenueId = venueId });

                if (!result)
                    throw new HttpStatusCodeResponseException(HttpStatusCode.NotModified);

                return result;
            }
        }

        public async Task<List<SpaceImageDto>> AddSpaceImagesAsync(List<string> base64EncodedVenueImages, Guid venueId, Guid spaceId)
        {
            List<SpaceImageDto> spaceImageDtos = new List<SpaceImageDto>();

            foreach (string img in base64EncodedVenueImages)
            {
                spaceImageDtos.Add(new SpaceImageDto
                {
                    Base64SpaceImageString = img,
                    SpaceId = spaceId
                });
            }

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                int resultCount = await con.InsertAsync(spaceImageDtos);

                if (resultCount == 0)
                    throw new HttpStatusCodeResponseException(HttpStatusCode.NotModified, $"Error inserting Space Images:\n{JsonConvert.SerializeObject(spaceImageDtos)}\ninto Space: {spaceId}");

                spaceImageDtos = await GetSpaceImagesAsync(venueId, spaceId);

                return spaceImageDtos;
            }
        }

        public async Task<List<SpaceImageDto>> GetSpaceImagesAsync(Guid venueId, Guid spaceId, bool requestSpecificallyForSpaceImages = true)
        {
            string getSpaceImagesBySpaceIdSql = "SELECT * FROM SpaceImage WHERE spaceId = @spaceId";

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                IEnumerable<SpaceImageDto> spaceImages = await con.QueryAsync<SpaceImageDto>(getSpaceImagesBySpaceIdSql, new { spaceId });

                if (spaceImages == null || (requestSpecificallyForSpaceImages && spaceImages.Count() == 0))
                    throw new HttpStatusCodeResponseException(HttpStatusCode.NotModified);

                return spaceImages.ToList();
            }
        }

        public async Task<bool> DeleteSpaceImagesAsync(List<Guid> spaceImageIds, Guid venueId, Guid spaceId)
        {
            List<SpaceImageDto> spaceImageDtos = new List<SpaceImageDto>();

            spaceImageIds.ForEach(x => spaceImageDtos.Add(new SpaceImageDto { SpaceId = spaceId, SpaceImageId = x }));

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                bool result = await con.DeleteAsync(spaceImageDtos);

                if (!result)
                    throw new HttpStatusCodeResponseException(HttpStatusCode.NotModified);

                return true; 
            }
        }
    }
}
