using HotChocolate.AspNetCore.Authorization;
using NannyData.Interfaces;
using NannyModels.Models;

namespace NannyAPI.GraphQL.Addresses
{
    [ExtendObjectType(extendsType: typeof(Query))]
    public class AddressQueries
    {
        private readonly IAddressDAO _addressDAO;

        public AddressQueries(IAddressDAO addressDAO)
        {
            _addressDAO = addressDAO;
        }

        [UseSorting]
        [UseFiltering]
        [Authorize]
        public ICollection<Address> GetAddress(int userID)
        {
            return _addressDAO.GetAddressesByUserID(userID);
        }
    }
}
