using System;
using System.Collections.Generic;
using System.Text;

namespace VLibraries.APIModels
{
    public class Venue
    {
        public Guid VenueId { get; set; }
        public string Description { get; set; }
        public string MUrl { get; set; }
        public List<Space> Spaces {get;set;}
    }
}
