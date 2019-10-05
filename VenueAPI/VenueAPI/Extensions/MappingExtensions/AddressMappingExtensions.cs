using System.Text;
using VLibraries.APIModels;

namespace VenueAPI.Extensions
{
    public static class AddressExtensions
    {
        public static VenueAddress MapAddressProperties(this LocationIqReverseResponse locationIqResponse, string buildingNameOrNumber)
        {
            VenueAddress mappedAddress = new VenueAddress
            {
                Postcode = locationIqResponse.Address.Postcode,
                Road = locationIqResponse.Address.Road,
                Town = locationIqResponse.Address.Town,
                Suburb = locationIqResponse.Address.Suburb,
                Village = locationIqResponse.Address.Village,
                County = locationIqResponse.Address.County,
                State = locationIqResponse.Address.State,
                Country = locationIqResponse.Address.Country,
                BuildingNameOrNumber = buildingNameOrNumber
            };

            mappedAddress.DisplayName = mappedAddress.MapDisplayNameProperties();

            return mappedAddress;
        }

        public static string MapDisplayNameProperties(this VenueAddress address)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(AppendAsDisplayName(address.BuildingNameOrNumber))
            .Append(AppendAsDisplayName(address.Road))
            .Append(AppendAsDisplayName(address.Village))
            .Append(AppendAsDisplayName(address.Suburb))
            .Append(AppendAsDisplayName(address.Town))
            .Append(AppendAsDisplayName(address.County))
            .Append(AppendAsDisplayName(address.State))
            .Append(AppendAsDisplayName(address.Country));

            sb.Remove(sb.Length - 2, 2); //Remove trailing comma

            return sb.ToString();
        }

        private static string AppendAsDisplayName(string input)
        {
            return input == null || string.IsNullOrEmpty(input)
                ? ""
                : input + ", ";
        }
    }
}
