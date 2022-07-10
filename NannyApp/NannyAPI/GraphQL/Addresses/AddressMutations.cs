using HotChocolate.AspNetCore.Authorization;
using NannyAPI.Miscellaneous.Errors;
using NannyData.Interfaces;
using NannyModels.Models.AddressModels;
using System.Security.Claims;

namespace NannyAPI.GraphQL.Addresses
{
    [ExtendObjectType(extendsType: typeof(Mutation))]
    public class AddressMutations
    {
        private readonly IAddressDAO _addressDAO;

        public AddressMutations(IAddressDAO addressDAO)
        {
            _addressDAO = addressDAO;
        }

        [Authorize]
        public Address AddNewAddress(AddressInput address, ClaimsPrincipal claimsPrincipal)
        {
            claimsPrincipal.UserNullCheck();
            string id = claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);

            int addressID = _addressDAO.AddNewAddress(address, int.Parse(id));

            if (addressID > 0)
            {
                return MapAddress(address, addressID, int.Parse(id));
            }

            throw new Exception("Something went wrong");
        }

        private Address MapAddress(AddressInput address, int addressID, int userID)
        {
            return new Address()
            {
                AddressID = addressID,
                Address1 = address.Address1,
                Address2 = address.Address2,
                Address3 = address.Address3,
                Address4 = address.Address4,
                Locality = address.Locality,
                Region = address.Region,
                County = address.County,
                Country = address.Country,
                PostalCode = address.PostalCode,
                UserID = userID,
            };
        }
    }
}
