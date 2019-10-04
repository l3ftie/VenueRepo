using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VenueAPI.DAL;
using VenueAPI.Extensions;
using VLibraries.APIModels;

namespace VenueAPI.BLL
{
    class VenueProvider : IVenueProvider
    {
        private readonly IVenueRepository _venueRepo;
        private readonly IVenueImageRepository _venueImageRepo;
        private readonly ISpaceProvider _spaceProvider;

        public VenueProvider(IVenueRepository venueRepo, IVenueImageRepository venueImageRepo, ISpaceProvider spaceProvider)
        {
            _venueRepo = venueRepo;
            _venueImageRepo = venueImageRepo;
            _spaceProvider = spaceProvider;
        }

        public async Task<VenueResponse> AddVenueAsync(VenueRequest venue)
        {
            Guid insertedSpaceId = await _venueRepo.AddVenueAsync(venue);

            return await GetVenueAsync(insertedSpaceId);
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

            return venueDtos.MapSpacesToVenues(spaces);
        }        

        public async Task<VenueResponse> EditVenueAsync(VenueRequest venue, Guid venueId)
        {
            await _venueRepo.EditVenueAsync(venue.MapRequestToDto(venueId));

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
