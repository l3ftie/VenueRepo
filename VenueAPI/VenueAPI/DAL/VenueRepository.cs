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
using VenueAPI.MappingExtensions;
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
        
        public async Task<Guid> AddVenueAsync(VenueRequest venue)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                Guid insertedVenueId = await con.QueryFirstOrDefaultAsync<Guid>("DECLARE @TempTable table([VenueId] [uniqueidentifier]); " +
                    "INSERT INTO Venue (Title, Description, Summary, Testimonial, TestimonialContactName, " +
                    "TestimonialContactOrganisation, TestimonialContactEmail, MUrl, VenueTypeId) " +
                    "   OUTPUT INSERTED.[VenueId] INTO @TempTable " +
                    "VALUES (@Title, @Description, @Summary, @Testimonial, @TestimonialContactName, " +
                    "@TestimonialContactOrganisation, @TestimonialContactEmail, @MUrl, @VenueTypeId);" +
                    "SELECT [VenueId] FROM @TempTable;", venue);

                if (insertedVenueId == Guid.Empty)
                    throw new HttpStatusCodeResponseException(HttpStatusCode.NotModified, $"Error inserting Venue:\n{JsonConvert.SerializeObject(venue)}");

                return insertedVenueId;
            }
        }
        
        public async Task<List<VenueDto>> GetVenueAsync(Guid venueId)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                IEnumerable<VenueDto> venueDtos = await con.QueryAsync<VenueDto>("SELECT V.VenueId, V.Title, V.Description, V.MUrl, V.Summary, " +
                    "V.Testimonial, V.TestimonialContactEmail, V.TestimonialContactName, V.TestimonialContactOrganisation, " +
                    "VI.VenueImageId, VI.Base64VenueImageString, " +
                    "VT.VenueTypeId, VT.Description as VenueTypeDescription " +
                    "FROM [VenueFinder].[dbo].Venue V " +
                    "LEFT OUTER JOIN [VenueFinder].[dbo].VenueImage VI " +
                    "ON VI.VenueId = V.VenueId " +
                    "LEFT OUTER JOIN [VenueFinder].[dbo].VenueType VT " +
                    "ON VT.VenueTypeId = V.VenueTypeId " +
                    "WHERE V.VenueId = @venueId;", new { venueId });

                if (venueDtos == null || venueDtos.Count() == 0)
                    throw new HttpStatusCodeResponseException(HttpStatusCode.NotFound);

                return venueDtos.ToList();
            }
        }        

        public async Task<List<VenueDto>> GetVenuesAsync()
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                IEnumerable<VenueDto> venueDtos = await con.QueryAsync<VenueDto>("SELECT V.VenueId, V.Title, V.Description, V.MUrl, V.Summary, " +
                    "V.Testimonial, V.TestimonialContactEmail, V.TestimonialContactName, V.TestimonialContactOrganisation, " +
                    "VI.VenueImageId, VI.Base64VenueImageString, " +
                    "VT.VenueTypeId, VT.Description as VenueTypeDescription " +
                    "FROM [VenueFinder].[dbo].Venue V " +
                    "LEFT OUTER JOIN [VenueFinder].[dbo].VenueImage VI " +
                    "ON VI.VenueId = V.VenueId " +
                    "LEFT OUTER JOIN [VenueFinder].[dbo].VenueType VT " +
                    "ON VT.VenueTypeId = V.VenueTypeId;");

                if (venueDtos == null || venueDtos.Count() == 0)
                    throw new HttpStatusCodeResponseException(HttpStatusCode.NotFound);

                return venueDtos.ToList();
            }
        }

        public async Task<bool> EditVenueAsync(VenueDto venue)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                bool updateVenueResult = await con.UpdateAsync(venue);

                if (!updateVenueResult)
                    throw new HttpStatusCodeResponseException(HttpStatusCode.NotModified);

                return updateVenueResult;
            }
        }

        public async Task<bool> DeleteVenueAsync(Guid venueId)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {  
                bool deleteVenueResult = await con.DeleteAsync(new VenueDto { VenueId = venueId });

                if (!deleteVenueResult)
                    throw new HttpStatusCodeResponseException(HttpStatusCode.NotModified);

                return deleteVenueResult;
            }
        }        
    }
}
