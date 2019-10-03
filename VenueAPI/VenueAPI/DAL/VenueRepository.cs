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
        
        public async Task<VenueResponse> AddVenueAsync(VenueRequest venue)
        {
            string insertVenueSql =
            "DECLARE @TempTable table([VenueId] [uniqueidentifier]); " +
            "INSERT INTO Venue (Title, Description, Summary, Testimonial, TestimonialContactName, " +
            "TestimonialContactOrganisation, TestimonialContactEmail, MUrl, VenueTypeId) " +
            "   OUTPUT INSERTED.[VenueId] INTO @TempTable " +
            "VALUES (@Title, @Description, @Summary, @Testimonial, @TestimonialContactName, " +
            "@TestimonialContactOrganisation, @TestimonialContactEmail, @MUrl, @VenueTypeId);" +
            "SELECT [VenueId] FROM @TempTable;";

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                Guid insertedVenueId = await con.QueryFirstOrDefaultAsync<Guid>(insertVenueSql, venue);

                if (insertedVenueId == Guid.Empty)
                    throw new HttpStatusCodeResponseException(HttpStatusCode.NotModified, $"Error inserting Venue:\n{JsonConvert.SerializeObject(venue)}");

                return await GetVenueAsync(insertedVenueId);
            }
        }
        
        public async Task<VenueResponse> GetVenueAsync(Guid venueId)
        {
            VenueResponse model = new VenueResponse();

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

                List<SpaceResponse> spaces = await _spaceRepo.GetSpacesAsync(venueId, false);

                model = venueDtos.MapDtoToResponse(spaces);

                return model;
            }
        }        

        public async Task<List<VenueResponse>> GetVenuesAsync()
        {
            List<VenueResponse> models = new List<VenueResponse>();

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

                List<SpaceResponse> spaces = await _spaceRepo.GetSpacesAsync(venueDtos.Select(x => x.VenueId).Distinct().ToList(), false);

                List<List<VenueDto>> venueDtosGroupedById = venueDtos.GroupBy(x => x.VenueId).Select(y => y.ToList()).ToList();

                List<List<SpaceResponse>> spacesGroupedByVenueId = spaces.GroupBy(x => x.VenueId).Select(y => y.ToList()).ToList();

                foreach (List<VenueDto> groupedVenues in venueDtosGroupedById)
                {
                    List<SpaceResponse> spacesToBeMapped = new List<SpaceResponse>();                    

                    foreach (List<SpaceResponse> spaceGrouping in spacesGroupedByVenueId)
                    {
                        if (spaceGrouping.FirstOrDefault().VenueId == groupedVenues.FirstOrDefault().VenueId)
                            spacesToBeMapped = spaceGrouping;
                    }

                    VenueResponse model = groupedVenues.MapDtoToResponse(spacesToBeMapped);

                    models.Add(model);
                }               

                return models;
            }
        }

        public async Task<VenueResponse> EditVenueAsync(VenueRequest venue, Guid venueId)
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
                TestimonialContactOrganisation = venue.TestimonialContactOrganisation,
                VenueTypeId = venue.VenueTypeId
            };

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                bool updateVenueResult = await con.UpdateAsync(venueDto);

                if (!updateVenueResult)
                    throw new HttpStatusCodeResponseException(HttpStatusCode.NotModified);

                VenueResponse model = await GetVenueAsync(venueId);

                return model;
            }
        }

        public async Task<bool> DeleteVenueAsync(Guid venueId)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                //Delete in Order so as not to violate SQL table constraints
                List<SpaceResponse> spaces = await _spaceRepo.GetSpacesAsync(venueId, false);

                spaces.ForEach(async space => await _spaceRepo.DeleteSpaceAsync(venueId, space.SpaceId));
                
                List<VenueImageDto> venueImageDtos = await _venueImageRepo.GetVenueImagesAsync(venueId, false);
                if (venueImageDtos.Count() > 0)
                    await con.DeleteAsync(venueImageDtos);

                bool deleteVenueResult = await con.DeleteAsync(new VenueDto { VenueId = venueId });
                if (!deleteVenueResult)
                    throw new HttpStatusCodeResponseException(HttpStatusCode.NotModified);

                return deleteVenueResult;
            }
        }        
    }
}
