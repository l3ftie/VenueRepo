using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VenueAPI.DAL;
using VLibraries.APIModels;

namespace VenueAPI.BLL
{
    class SpaceProvider : ISpaceProvider
    {
        private readonly ISpaceRepository _spaceRepo;
        public SpaceProvider(ISpaceRepository spaceRepo)
        {
            _spaceRepo = spaceRepo;
        }

        public async Task<SpaceResponse> AddSpaceAsync(SpaceRequest space, Guid venueId)
        {
            return await _spaceRepo.AddSpaceAsync(space, venueId);
        }

        public async Task<SpaceResponse> GetSpaceAsync(Guid venueId, Guid spaceId)
        {
            return await _spaceRepo.GetSpaceAsync(venueId, spaceId);
        }
        public async Task<List<SpaceResponse>> GetSpacesAsync(Guid venueId)
        {
            return await _spaceRepo.GetSpacesAsync(venueId);
        }

        public async Task<SpaceResponse> EditSpaceAsync(SpaceRequest space, Guid venueId, Guid spaceId)
        {
            return await _spaceRepo.EditSpaceAsync(space, venueId, spaceId);
        }

        public async Task<bool> DeleteSpaceAsync(Guid venueId, Guid spaceId)
        {
            return await _spaceRepo.DeleteSpaceAsync(venueId, spaceId);
        }

        public async Task<SpaceResponse> UpsertSpaceType(Guid venueId, Guid spaceId, SpaceTypeDto spaceType)
        {
            return await _spaceRepo.UpsertSpaceType(venueId, spaceId, spaceType);
        }
    }
}
