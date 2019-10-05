using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VLibraries.APIModels;
using VLibraries.HttpClientWrapper;
using VLibraries.SharedCode.Extensions;

namespace VLibraries.APIAbstractions
{
    public interface ILocationIqProxy
    {
        Task<LocationIqReverseResponse> GetLocationDetailsAsync(string postcode);
    }
}
