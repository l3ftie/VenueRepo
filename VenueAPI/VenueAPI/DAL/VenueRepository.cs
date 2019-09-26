using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Dapper;
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

        public async Task<Venue> AddVenueAsync(Venue venue)
        {
            string insertVenueSql =
            "DECLARE @TempTable table([VenueId] [uniqueidentifier]); " +
            "INSERT INTO Venue (Description, MUrl) " +
            "   OUTPUT INSERTED.[VenueId] INTO @TempTable " +
            "VALUES (@Description, @MUrl);" +
            "SELECT [VenueId] FROM @TempTable;";

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                IEnumerable<Guid> results = await con.QueryAsync<Guid>(insertVenueSql, venue);

                if (results.Count() == 0)
                    throw new HttpStatusCodeResponseException(HttpStatusCode.NotModified);

                Guid insertedVenueId = results.FirstOrDefault();

                return new Venue();
            }
        }
    }
}
