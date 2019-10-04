using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VLibraries.APIModels;

namespace VenueAPI.MappingExtensions
{
    public static class LinqExtensions
    {
        public static List<List<SpaceResponse>> GroupSpacesByVenueId(this List<SpaceResponse> spaces)
        {
            return spaces.GroupBy(x => x.VenueId).Select(y => y.ToList()).ToList();
        }

        public static List<List<SpaceDto>> GroupSpacesByVenueId(this List<SpaceDto> spaces)
        {
            return spaces.GroupBy(x => x.VenueId).Select(y => y.ToList()).ToList();
        }

        public static List<List<SpaceDto>> GroupSpacesBySpaceId(this List<SpaceDto> spaces)
        {
            return spaces.GroupBy(x => x.VenueId).Select(y => y.ToList()).ToList();
        }

        public static List<List<VenueDto>> GroupVenuesByVenueId(this List<VenueDto> venueDtos)
        {
            return venueDtos.GroupBy(x => x.VenueId).Select(y => y.ToList()).ToList();
        }
    }
}
