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

                bool addImagesResult = await AddVenueImagesAsync(venue.VenueImages, insertedVenueId);

                return await GetVenueAsync(insertedVenueId);
            }
        }

        public async Task<bool> AddVenueImagesAsync(List<VenueImage> venueImages, Guid venueId)
        {
            List<VenueImageDto> dtos = new List<VenueImageDto>();

            foreach (VenueImage img in venueImages)
            {
                dtos.Add(new VenueImageDto
                {
                    Base64VenueImageString = img.Base64VenueImageString,
                    VenueId = venueId
                });
            }

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                int resultCount = await con.InsertAsync(dtos);

                if (resultCount == 0)
                    throw new HttpStatusCodeResponseException(HttpStatusCode.NotModified, $"Error inserting Venue Images:\n{JsonConvert.SerializeObject(venueImages)}\ninto Venue: {venueId}");

                return true;
            }
        }

        public async Task<VenueDto> GetVenueAsync(Guid venueId)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                VenueDto dto = await con.GetAsync<VenueDto>(venueId);

                if (dto == null)
                    throw new HttpStatusCodeResponseException(HttpStatusCode.NotFound);

                dto.Spaces = await GetSpacesAsync(venueId);

                return dto;
            }
        }

        public async Task<List<VenueDto>> GetVenuesAsync()
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                IEnumerable<VenueDto> dtos = await con.GetAllAsync<VenueDto>();

                if (dtos == null || dtos.Count() == 0)
                    throw new HttpStatusCodeResponseException(HttpStatusCode.NotFound);

                return dtos.ToList();
            }
        }

        public async Task<VenueDto> EditVenueAsync(Venue venue, Guid venueId)
        {
            VenueDto dto = new VenueDto
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
                bool result = await con.UpdateAsync(dto);

                if (!result)
                    throw new HttpStatusCodeResponseException(HttpStatusCode.NotModified);

                dto = await GetVenueAsync(venueId);

                return dto;
            }
        }

        public async Task<bool> DeleteVenueAsync(Guid venueId)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                bool result = await con.DeleteAsync(new VenueDto { VenueId = venueId });

                if (!result)
                    throw new HttpStatusCodeResponseException(HttpStatusCode.NotModified);

                return result;
            }
        }


        public async Task<SpaceDto> AddSpaceAsync(Space space, Guid venueId)
        {
            string insertSpaceSql =
            "DECLARE @TempTable table([SpaceId] [uniqueidentifier]); " +
            "INSERT INTO Space (MaxCapacity, VenueId, SpaceTypeId) " +
            "   OUTPUT INSERTED.[SpaceId] INTO @TempTable " +
            "VALUES (@MaxCapacity, @VenueId, @SpaceTypeId);" +
            "SELECT [SpaceId] FROM @TempTable;";

            SpaceDto spaceDto = new SpaceDto
            {
                VenueId = venueId,
                MaxCapacity = space.MaxCapacity,                
                SpaceType = space.SpaceType
            };            

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                Guid insertedSpaceId = await con.QueryFirstOrDefaultAsync<Guid>(insertSpaceSql, spaceDto);

                if (insertedSpaceId == Guid.Empty)
                    throw new HttpStatusCodeResponseException(HttpStatusCode.NotModified, $"Error inserting Space:\n{JsonConvert.SerializeObject(space)}\ninto Venue: {venueId}");

                await AddSpaceImagesAsync(space.SpaceImages, insertedSpaceId);

                return await GetSpaceAsync(venueId);
            }
        }

        public async Task<SpaceDto> GetSpaceAsync(Guid spaceId)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                SpaceDto space = await con.GetAsync<SpaceDto>(spaceId);

                if (space == null)
                    throw new HttpStatusCodeResponseException(HttpStatusCode.NotModified);

                space.SpaceImages = await GetSpaceImagesAsync(spaceId);

                return space;
            }
        }

        public async Task<List<SpaceDto>> GetSpacesAsync(Guid venueId)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                List<SpaceDto> spaces = await con.GetAsync<List<SpaceDto>>(venueId);

                if (spaces == null || spaces.Count == 0)
                    throw new HttpStatusCodeResponseException(HttpStatusCode.NotModified);

                return spaces;
            }
        }

        public async Task<SpaceDto> EditSpaceAsync(Space space, Guid spaceId, Guid venueId)
        {
            SpaceDto dto = new SpaceDto
            {
                SpaceId = spaceId,
                VenueId = venueId,
                MaxCapacity = space.MaxCapacity,
                SpaceType = space.SpaceType
            };

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                bool result = await con.UpdateAsync(dto);

                if (!result)
                    throw new HttpStatusCodeResponseException(HttpStatusCode.NotModified);

                dto = await GetSpaceAsync(spaceId);

                return dto;
            }
        }


        public async Task<bool> DeleteSpaceAsync(Guid spaceId, Guid venueId)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                bool result = await con.DeleteAsync(new SpaceDto { SpaceId = spaceId, VenueId = venueId });

                if (!result)
                    throw new HttpStatusCodeResponseException(HttpStatusCode.NotModified);

                return result;
            }
        }
                

        public async Task<List<SpaceImageDto>> AddSpaceImagesAsync(List<SpaceImage> spaceImages, Guid spaceId)
        {
            List<SpaceImageDto> dtos = new List<SpaceImageDto>();

            foreach (SpaceImage img in spaceImages)
            {
                dtos.Add(new SpaceImageDto
                {
                    Base64SpaceImageString = img.Base64SpaceImageString,
                    SpaceId = spaceId
                });
            }

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                int resultCount = await con.InsertAsync(dtos);

                if (resultCount == 0)
                    throw new HttpStatusCodeResponseException(HttpStatusCode.NotModified, $"Error inserting Space Images:\n{JsonConvert.SerializeObject(spaceImages)}\ninto Space: {spaceId}");

                List<SpaceImageDto> refreshedSpaceImages = await GetSpaceImagesAsync(spaceId);

                return refreshedSpaceImages;
            }
        }

        public async Task<List<SpaceImageDto>> GetSpaceImagesAsync(Guid spaceId)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                List<SpaceImageDto> spaceImages = await con.GetAsync<List<SpaceImageDto>>(spaceId);

                if (spaceImages == null || spaceImages.Count == 0)
                    throw new HttpStatusCodeResponseException(HttpStatusCode.NotModified);

                return spaceImages;
            }
        }

        public async Task<bool> DeleteSpaceImagesAsync(List<Guid> spaceImageIds, Guid spaceId)
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
