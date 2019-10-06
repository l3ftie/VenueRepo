using System;
using System.Collections.Generic;
using System.Linq;
using VLibraries.APIModels;

namespace VenueAPI.Extensions
{
    public static class VenueMappingExtensions
    {
        public static VenueDto MapRequestToDto(this VenueRequest venue, Guid venueId, VenueAddress venueAddress)
        {
            return new VenueDto
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
                VenueTypeId = venue.VenueTypeId,
                Postcode = venue.Postcode,
                BuildingNameOrNumber = venue.BuildingNameOrNumber,
                Road = venueAddress.Road,
                State = venueAddress.State,
                Country = venueAddress.Country,
                County = venueAddress.County,
                Village = venueAddress.Village,
                Town = venueAddress.Town,
                DisplayName = venueAddress.MapDisplayNameProperties()
            };
        }
            

        public static VenueResponse MapDtoToResponse(this IEnumerable<VenueDto> venueDtos, List<SpaceResponse> spacesToAddToVenue)
        {
            VenueDto venueDto = venueDtos.FirstOrDefault();

            VenueResponse model = new VenueResponse
            {
                VenueId = venueDto.VenueId,
                Description = venueDto.Description,
                MUrl = venueDto.MUrl,
                Summary = venueDto.Summary,
                Title = venueDto.Title,
                Testimonial = venueDto.Testimonial,
                TestimonialContactEmail = venueDto.TestimonialContactEmail,
                TestimonialContactName = venueDto.TestimonialContactName,
                TestimonialContactOrganisation = venueDto.TestimonialContactOrganisation,
                Spaces = spacesToAddToVenue,
                VenueImages = new List<VenueImageDto>(),
                VenueType = new VenueTypeDto
                {
                    VenueTypeId = venueDto.VenueTypeId,
                    Description = venueDto.VenueTypeDescription
                },
                Address = new VenueAddress
                {
                    Postcode = venueDto.Postcode,
                    BuildingNameOrNumber = venueDto.BuildingNameOrNumber,
                    Road = venueDto.Road,
                    Country = venueDto.Country,
                    County = venueDto.County,
                    State = venueDto.State,
                    Suburb = venueDto.Suburb,
                    Town = venueDto.Town,
                    Village = venueDto.Village
                }
            };

            model.Address.DisplayName = model.Address.MapDisplayNameProperties();

            foreach (VenueDto venue in venueDtos)
            {
                if (venue.VenueImageId != Guid.Empty && !model.VenueImages.Any(x => x.VenueImageId == venue.VenueImageId))
                    model.VenueImages.Add(new VenueImageDto
                    {
                        Base64VenueImageString = venue.Base64VenueImageString,
                        VenueId = venue.VenueId,
                        VenueImageId = venue.VenueImageId
                    });
            }

            return model;
        }

        public static List<VenueResponse> MapDtosToResponsesAddingSpaces(this List<VenueDto> unmappedVenues, List<SpaceResponse> unmappedSpaces)
        {
            List<VenueResponse> mappedVenues = new List<VenueResponse>();

            foreach (List<VenueDto> groupedVenues in unmappedVenues.GroupVenuesByVenueId())
            {
                List<SpaceResponse> matchingSpaces = groupedVenues.GetSpacesForVenueId(unmappedSpaces.GroupSpacesByVenueId());

                mappedVenues.Add(groupedVenues.MapDtoToResponse(matchingSpaces));
            }

            return mappedVenues;
        }

        public static List<SpaceResponse> GetSpacesForVenueId(this List<VenueDto> groupedVenues, List<List<SpaceResponse>> spacesGroupedByVenueId)
        {
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
