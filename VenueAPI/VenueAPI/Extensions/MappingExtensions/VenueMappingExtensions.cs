using System.Collections.Generic;
using System.Linq;
using VLibraries.APIModels;

namespace VenueAPI.Extensions
{
    public static class VenueMappingExtensions
    {
        public static List<VenueResponse> MapSpacesToVenues(this List<VenueDto> venueDtos, List<SpaceResponse> unmappedSpaces)
        {
            List<VenueResponse> mappedVenues = new List<VenueResponse>();

            List<List<VenueDto>> venueGrouping = venueDtos.GroupVenuesByVenueId();

            List<List<SpaceResponse>> spaceGrouping = unmappedSpaces.GroupSpacesByVenueId();

            foreach (List<VenueDto> groupedVenues in venueGrouping)
            {
                List<SpaceResponse> matchingSpaces = groupedVenues.GetMatchingSpaceGroupings(spaceGrouping);

                VenueResponse mappedVenue = groupedVenues.MapDtoToResponse(matchingSpaces);

                mappedVenues.Add(mappedVenue);
            }

            return mappedVenues;
        }

        public static List<SpaceResponse> GetMatchingSpaceGroupings(this List<VenueDto> groupedVenues, List<List<SpaceResponse>> spacesGroupedByVenueId)
        {
            List<SpaceResponse> spacesToBeMapped = new List<SpaceResponse>();

            foreach (List<SpaceResponse> spaceGrouping in spacesGroupedByVenueId)
            {
                if (spaceGrouping.FirstOrDefault().VenueId == groupedVenues.FirstOrDefault().VenueId)
                {
                    return spaceGrouping;
                }
            }

            return new List<SpaceResponse>();
        }        
    }
}
