﻿using HotChocolate.AspNetCore.Authorization;
using NannyData.Interfaces;
using NannyModels.Models.AddressModels;
using System.Security.Claims;

namespace NannyAPI.GraphQL.Addresses
{
    /// <summary>
    /// The AddressQueries
    /// </summary>
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
        public ICollection<Address> GetAddress(ClaimsPrincipal claimsPrincipal)
        {
            string id = claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);
            return _addressDAO.GetAddressesByUserID(Int32.Parse(id));
        }
    }
}
