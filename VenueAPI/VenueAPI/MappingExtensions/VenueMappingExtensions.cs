using System.Collections.Generic;
using System.Linq;
using VLibraries.APIModels;

namespace VenueAPI.MappingExtensions
{
    public static class VenueMappingExtensions
    {
        public static List<VenueResponse> MapSpacesToVenues(this List<VenueDto> venueDtos, List<SpaceResponse> unmappedSpaces)
        {
            List<VenueResponse> mappedVenues = new List<VenueResponse>();

            List<List<VenueDto>> venueGrouping = venueDtos.GroupVenuesByVenueId();

            List<List<SpaceResponse>> spaceGrouping = unmappedSpaces.GroupSpacesByVenueId();

            foreach (List<VenueDto> venue in venueGrouping)
            {
                VenueResponse mappedVenue = venue.MapSpaceGroupingToVenue(spaceGrouping);

                if (mappedVenue != null)
                    mappedVenues.Add(mappedVenue);
            }

            return mappedVenues;
        }

        public static VenueResponse MapSpaceGroupingToVenue(this List<VenueDto> groupedVenues, List<List<SpaceResponse>> spacesGroupedByVenueId)
        {
            List<SpaceResponse> spacesToBeMapped = new List<SpaceResponse>();

            foreach (List<SpaceResponse> spaceGrouping in spacesGroupedByVenueId)
            {
                if (spaceGrouping.FirstOrDefault().VenueId == groupedVenues.FirstOrDefault().VenueId)
                {
                    //spacesToBeMapped = spaceGrouping;
                    VenueResponse venue = groupedVenues.MapDtoToResponse(spacesToBeMapped);

                    return venue;
                }
            }

            return null;
        }        
    }
}
