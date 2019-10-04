﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VenueAPI.DAL;
using VLibraries.APIModels;

namespace VenueAPI.BLL
{
    class SpaceImageProvider : ISpaceImageProvider
    {
        private readonly ISpaceImageRepository _spaceImageRepo;

        public SpaceImageProvider(ISpaceImageRepository spaceImageRepo)
        {
            _spaceImageRepo = spaceImageRepo;
        }

        public async Task<List<SpaceImageDto>> AddSpaceImagesAsync(List<string> base64EncodedVenueImages, Guid venueId, Guid spaceId)
        {
            List<SpaceImageDto> spaceImageDtos = new List<SpaceImageDto>();

            foreach (string img in base64EncodedVenueImages)
            {
                spaceImageDtos.Add(new SpaceImageDto
                {
                    Base64SpaceImageString = img,
                    SpaceId = spaceId
                });
            }

            await _spaceImageRepo.AddSpaceImagesAsync(spaceImageDtos);

            return await GetSpaceImagesAsync(venueId, spaceId);

        }

        public async Task<List<SpaceImageDto>> GetSpaceImagesAsync(Guid venueId, Guid spaceId)
        {
            return await _spaceImageRepo.GetSpaceImagesAsync(venueId, spaceId);
        }

        public async Task<bool> DeleteSpaceImagesAsync(List<Guid> spaceImageIds, Guid venueId, Guid spaceId)
        {
            List<SpaceImageDto> spaceImageDtos = new List<SpaceImageDto>();

            spaceImageIds.ForEach(x => spaceImageDtos.Add(new SpaceImageDto { SpaceId = spaceId, SpaceImageId = x }));

            return await _spaceImageRepo.DeleteSpaceImagesAsync(spaceImageDtos);
        }
    }
}
