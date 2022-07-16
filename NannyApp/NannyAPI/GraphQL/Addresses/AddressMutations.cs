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

        /// <summary>
        /// Adds a new address
        /// </summary>
        /// <param name="address">The Address</param>
        /// <param name="claimsPrincipal">The verified user</param>
        /// <returns>The address</returns>
        /// <exception cref="Exception"></exception>
        [Authorize]
        [GraphQLDescription("Adds a new address and connects to the current user")]
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

        /// <summary>
        /// Updates an address
        /// </summary>
        /// <param name="address">The address</param>
        /// <param name="addressID">The address id</param>
        /// <param name="claimsPrincipal">The verified user</param>
        /// <returns>The address</returns>
        /// <exception cref="Exception"></exception>
        [Authorize]
        [GraphQLDescription("Updates a users address")]
        public Address UpdateAddress(AddressInput address, int addressID, ClaimsPrincipal claimsPrincipal)
        {
            claimsPrincipal.UserNullCheck();
            string id = claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);

            bool success = _addressDAO.UpdateAddress(address, int.Parse(id), addressID);

            if (success)
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
