using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VenueAPI.DAL;
using VenueAPI.Extensions;
using VenueAPI.Services.LocationIq;
using VLibraries.APIModels;
using VLibraries.APIModels.GeoJson;

namespace VenueAPI.BLL
{
    class VenueProvider : IVenueProvider
    {
        private readonly IVenueRepository _venueRepo;
        private readonly IVenueImageRepository _venueImageRepo;
        private readonly ISpaceProvider _spaceProvider;
        private readonly ILocationIqProvider _locationIqProvider;

        public VenueProvider(IConfiguration config, IVenueRepository venueRepo, IVenueImageRepository venueImageRepo, ISpaceProvider spaceProvider, ILocationIqProvider locationIqProvider)
        {
            _venueRepo = venueRepo;
            _venueImageRepo = venueImageRepo;
            _spaceProvider = spaceProvider;
            _locationIqProvider = locationIqProvider;
        }

        public async Task<VenueResponse> AddVenueAsync(VenueRequest venue)
        {
            LocationIqReverseResponse locationResponse = await _locationIqProvider.GetLocationDetailsAsync(venue.Postcode);

            VenueAddress venueAddress = locationResponse.MapAddressProperties(venue.BuildingNameOrNumber);

            VenueDto venueDto = venue.MapRequestToDto(Guid.Empty, venueAddress);

            Guid insertedVenueId = await _venueRepo.AddVenueAsync(venueDto);

            VenueLocation venueLocation = new VenueLocation
            {
                Location = locationResponse.MapToGeoJson(),
                VenueId = insertedVenueId
            };

            await _locationIqProvider.AddGeoLocation(venueLocation);

            return await GetVenueAsync(insertedVenueId);
        }

        public async Task<VenueResponse> GetVenueAsync(Guid venueId)
        {
            List<VenueDto> venueDtos = await _venueRepo.GetVenueAsync(venueId);

            List<SpaceResponse> spaces = await _spaceProvider.GetSpacesAsync(venueId, false);

            return venueDtos.MapDtoToResponse(spaces);
        }

        public async Task<List<VenueResponse>> GetVenuesAsync()
        {
            List<VenueDto> venueDtos = await _venueRepo.GetVenuesAsync();

            List<SpaceResponse> spaces = await _spaceProvider.GetSpacesAsync(venueDtos.Select(x => x.VenueId).Distinct().ToList(), false);

            return venueDtos.MapDtosToResponsesAddingSpaces(spaces);
        }        

        public async Task<VenueResponse> EditVenueAsync(VenueRequest venue, Guid venueId)
        {
            LocationIqReverseResponse locationResponse = await _locationIqProvider.GetLocationDetailsAsync(venue.Postcode);

            VenueAddress venueAddress = locationResponse.MapAddressProperties(venue.BuildingNameOrNumber);

            VenueDto venueDto = venue.MapRequestToDto(Guid.Empty, venueAddress);

            await _venueRepo.EditVenueAsync(venueDto);

            return await GetVenueAsync(venueId);
        }

        public async Task<bool> DeleteVenueAsync(Guid venueId)
        {
            //Delete in Order so as not to violate SQL table constraints
            List<SpaceResponse> spaces = await _spaceProvider.GetSpacesAsync(venueId, false);

            spaces.ForEach(async space => await _spaceProvider.DeleteSpaceAsync(venueId, space.SpaceId));

            List<VenueImageDto> venueImageDtos = await _venueImageRepo.GetVenueImagesAsync(venueId, false);

            if (venueImageDtos.Count() > 0)
                await _venueImageRepo.DeleteVenueImagesAsync(venueImageDtos);

            return await _venueRepo.DeleteVenueAsync(venueId);
        }        
    }
}
