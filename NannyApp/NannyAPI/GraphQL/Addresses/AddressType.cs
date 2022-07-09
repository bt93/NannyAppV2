using NannyData.Interfaces;
using NannyModels.Models;
using HotChocolate.Types;
using NannyModels.Models.User;

namespace NannyAPI.GraphQL.Addresses
{
    /// <summary>
    /// The Address ObjectType
    /// </summary>
    public class AddressType : ObjectType<Address>
    {
        protected override void Configure(IObjectTypeDescriptor<NannyModels.Models.Address> descriptor)
        {
            descriptor.Description("The user addresses on file");
            descriptor.Field(a => a.User)
                .ResolveWith<Resolvers>(a => a.GetUserByID(default!, default!))
                .Description("Gets the User by the Users id");

            // Descriptions
            descriptor.Field(a => a.Address1).Description("The first address line");
            descriptor.Field(a => a.Address2).Description("The second address line");
            descriptor.Field(a => a.Address3).Description("The third address line");
            descriptor.Field(a => a.Address4).Description("The fourth address line");
            descriptor.Field(a => a.Locality).Description("The city/town/village");
            descriptor.Field(a => a.Region).Description("The state/province");
            descriptor.Field(a => a.PostalCode).Description("The PostalCode/Zipcode");
            descriptor.Field(a => a.County).Description("The County");
            descriptor.Field(a => a.Country).Description("The Country");
            descriptor.Field(a => a.UserID).Description("The UserID of the associated user");
            descriptor.Field(a => a.User).Description("The associated user");
        }

        public class Resolvers
        {

            public ApplicationUser GetUserByID([Parent] Address address, [Service] IUserDAO userDAO)
            {
                return userDAO.GetUserByID(address.UserID);
            }
        }
    }
}
