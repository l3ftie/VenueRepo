using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Dapper;
using Dapper.Contrib.Extensions;
using Microsoft.Extensions.Configuration;
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
                    throw new HttpStatusCodeResponseException(HttpStatusCode.NotModified);

                return await GetVenueAsync(insertedVenueId);
            }
        }
        
        public async Task<VenueDto> GetVenueAsync(Guid venueId)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                VenueDto dto = await con.GetAsync<VenueDto>(venueId);

                if (dto == null)
                    throw new HttpStatusCodeResponseException(HttpStatusCode.NotFound);

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
            //Map this and make all return types DTO from repo
            VenueDto dto = new VenueDto
            {
                VenueId = venueId,
                Description = venue.Description,
                MUrl = venue.MUrl        
            };

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                bool result = await con.UpdateAsync(dto);

                if (!result)
                    throw new HttpStatusCodeResponseException(HttpStatusCode.NotModified);

                return dto;
            }
        }

        public async Task<bool> DeleteVenueAsync(Guid id)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                bool result = await con.DeleteAsync(new VenueDto { VenueId = id });

                if (!result)
                    throw new HttpStatusCodeResponseException(HttpStatusCode.NotModified);

                return result;
            }
        }


        public async Task<VenueDto> AddSpaceAsync(Space space, Guid venueId)
        {
            throw new NotImplementedException();
        }
    }
}
