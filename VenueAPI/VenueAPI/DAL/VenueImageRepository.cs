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
using VLibraries.APIModels;
using VLibraries.CustomExceptions;

namespace VenueAPI.DAL
{
    public class VenueImageRepository : IVenueImageRepository
    {
        private readonly string _connectionString;

        public VenueImageRepository(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("LocalSqlServer");
        }

        public async Task<int> AddVenueImagesAsync(List<VenueImageDto> venueImageDtos)
        {          
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                int resultCount = await con.InsertAsync(venueImageDtos);

                if (resultCount == 0)
                    throw new HttpStatusCodeResponseException(HttpStatusCode.NotModified, $"Error inserting Venue Images:\n{JsonConvert.SerializeObject(venueImageDtos)}\n");

                return resultCount;
            }
        }

        public async Task<List<VenueImageDto>> GetVenueImagesAsync(Guid venueId, bool requestSpecificallyForVenueImages = true)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                IEnumerable<VenueImageDto> venueImages = await con.QueryAsync<VenueImageDto>("SELECT * FROM VenueImage WHERE venueId = @venueId", new { venueId });

                if (venueImages == null || (requestSpecificallyForVenueImages && venueImages.Count() == 0))
                    throw new HttpStatusCodeResponseException(HttpStatusCode.NotFound);

                return venueImages.ToList();
            }
        }        

        public async Task<bool> DeleteVenueImagesAsync(List<VenueImageDto> venueImageDtos)
        {            
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                bool result = await con.DeleteAsync(venueImageDtos);

                if (!result)
                    throw new HttpStatusCodeResponseException(HttpStatusCode.NotModified);

                return true;
            }
        }

        //Only at Repo level
        //public async Task<List<VenueImageDto>> GetVenueImagesAsync(List<Guid> venueIds, bool requestSpecificallyForVenueImages = true)
        //{
        //    using (SqlConnection con = new SqlConnection(_connectionString))
        //    {
        //        IEnumerable<VenueImageDto> venueImages = await con.QueryAsync<VenueImageDto>("SELECT * FROM VenueImage WHERE venueId IN @venueIds", new { venueIds });

        //        if (venueImages == null || (requestSpecificallyForVenueImages && venueImages.Count() == 0))
        //            throw new HttpStatusCodeResponseException(HttpStatusCode.NotFound);

        //        return venueImages.ToList();
        //    }
        //}
    }
}
