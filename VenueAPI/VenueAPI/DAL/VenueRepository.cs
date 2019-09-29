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
        private readonly IVenueImageRepository _venueImageRepo;
        private readonly ISpaceRepository _spaceRepo;
        public VenueRepository(IConfiguration config, IVenueImageRepository venueImageRepo, ISpaceRepository spaceRepo)
        {
            _connectionString = config.GetConnectionString("LocalSqlServer");
            _venueImageRepo = venueImageRepo;
            _spaceRepo = spaceRepo;
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

                venueDto.VenueImages = await _venueImageRepo.GetVenueImagesAsync(venueId);

                if (!initialInsert)
                    venueDto.Spaces = await _spaceRepo.GetSpacesAsync(venueId, initialInsert);

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
                    venue.VenueImages = await _venueImageRepo.GetVenueImagesAsync(venue.VenueId);

                    venue.Spaces = await _spaceRepo.GetSpacesAsync(venue.VenueId, false);
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
                List<VenueImageDto> venueImageDtos = await _venueImageRepo.GetVenueImagesAsync(venueId);

                //Delete images first or foreign key constraint fails at DB level
                bool deleteImagesResult = await con.DeleteAsync(venueImageDtos);

                bool deleteVenueResult = await con.DeleteAsync(new VenueDto { VenueId = venueId });

                if (!deleteVenueResult)
                    throw new HttpStatusCodeResponseException(HttpStatusCode.NotModified);

                return deleteVenueResult;
            }
        }        
    }
}
