using System;

namespace VLibraries.APIModels
{
    public class Booking
    {
        public Guid BoookingID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
