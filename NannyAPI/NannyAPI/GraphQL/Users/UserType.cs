using NannyData.Interfaces;
using NannyModels.Models;
using NannyModels.Enumerations;

namespace NannyAPI.GraphQL.Users
{
    public class UserType : ObjectType<ApplicationUser>
    {
        protected override void Configure(IObjectTypeDescriptor<ApplicationUser> descriptor)
        {
            descriptor
                .Field("userID")
                .Resolve(context =>
                {
                    var claimsPrincipal = context.GetUser();
                    return claimsPrincipal;
                });

            descriptor.Description("The user information on file");
            descriptor.Field(u => u.Addresses)
                .ResolveWith<Resolvers>(u => u.GetAddressByUserID(default!, default!))
                .Description("Gets the address by the Users id");

            descriptor
                .Field("role")
                .Argument("roleID", a => a.Type<RoleType>())
                .Resolve(context => context.ArgumentValue<Role>("roleID"));

            // Descriptions
            descriptor.Field(u => u.UserID).Description("The Users id");
            descriptor.Field(u => u.FirstName).Description("The Users first name");
            descriptor.Field(u => u.LastName).Description("The Users last name");
            descriptor.Field(u => u.UserName).Description("The Users user name");
            descriptor.Field(u => u.EmailAddress).Description("The Users email address");
            descriptor.Field(u => u.PhoneNumber).Description("The Users phone number");
            descriptor.Field(u => u.RoleID).Description("The Users role");
            descriptor.Field(u => u.RoleID).Description("The Users addresses");
        }

        public class Resolvers
        {

            public ICollection<Address> GetAddressByUserID([Parent] ApplicationUser user, [Service] IAddressDAO addressDAO)
            {
                return addressDAO.GetAddressesByUserID(user.UserID);
            }
        }

        public class RoleType : EnumType<Role> { }
    }
}
