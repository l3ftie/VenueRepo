using Dapper.Contrib.Extensions;
using System;

namespace VLibraries.APIModels
{
    [Table("VenueType")]
    public class VenueTypeDto
    {
        public Guid VenueTypeId { get; set; }
        public string Description { get; set; }        
    }
}
