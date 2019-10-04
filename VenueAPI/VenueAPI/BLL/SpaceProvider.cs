using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VenueAPI.DAL;
using VenueAPI.Extensions;
using VLibraries.APIModels;

namespace VenueAPI.BLL
{
    class SpaceProvider : ISpaceProvider
    {
        private readonly ISpaceRepository _spaceRepo;
        private readonly ISpaceImageRepository _spaceImageRepo;

        public SpaceProvider(ISpaceRepository spaceRepo, ISpaceImageRepository spaceImageRepo)
        {
            _spaceRepo = spaceRepo;
            _spaceImageRepo = spaceImageRepo;
        }

        public async Task<SpaceResponse> AddSpaceAsync(SpaceRequest space, Guid venueId)
        {
            SpaceDto spaceDto = space.MapRequestToDto(venueId, Guid.Empty);

            Guid insertedSpaceId = await _spaceRepo.AddSpaceAsync(spaceDto);

            return await GetSpaceAsync(venueId, insertedSpaceId);
        }        

        public async Task<SpaceResponse> GetSpaceAsync(Guid venueId, Guid spaceId, bool requestSpecificallyForSpaces = true)
        {
            List<SpaceDto> spaceDtos = await _spaceRepo.GetSpaceAsync(venueId, spaceId, requestSpecificallyForSpaces);

            return spaceDtos.MapDtosToResponse();
        }

        public async Task<List<SpaceResponse>> GetSpacesAsync(Guid venueId, bool requestSpecificallyForSpaces = true)
        {
            List<SpaceDto> spaceDtos = await _spaceRepo.GetSpacesAsync(venueId, requestSpecificallyForSpaces);

            return spaceDtos.MapDtosToResponses();
        }        

        public async Task<SpaceResponse> EditSpaceAsync(SpaceRequest space, Guid venueId, Guid spaceId)
        {
            SpaceDto spaceDto = space.MapRequestToDto(venueId, spaceId);

            await _spaceRepo.EditSpaceAsync(spaceDto);

            return await GetSpaceAsync(venueId, spaceId);
        }

        public async Task<bool> DeleteSpaceAsync(Guid venueId, Guid spaceId)
        {
            List<SpaceImageDto> spaceImageDtos = await _spaceImageRepo.GetSpaceImagesAsync(venueId, spaceId);

            if (spaceImageDtos.Count > 0)
                await _spaceImageRepo.DeleteSpaceImagesAsync(spaceImageDtos);

            return await _spaceRepo.DeleteSpaceAsync(venueId, spaceId);
        }


        /// <summary>
        /// This method is not exposed at API level
        /// </summary>
        /// <param name="venueIds"></param>
        /// <param name="requestSpecificallyForSpaces"></param>
        /// <returns></returns>
        public async Task<List<SpaceResponse>> GetSpacesAsync(List<Guid> venueIds, bool requestSpecificallyForSpaces = true)
        {
            List<SpaceDto> spaceDtos = await _spaceRepo.GetSpacesAsync(venueIds, requestSpecificallyForSpaces);

            return spaceDtos.MapDtoGroupedByVenueIdToResponses();
        }
    }
}
