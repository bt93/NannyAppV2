using NannyData.Interfaces;
using NannyModels.Enumerations;
using HotChocolate.AspNetCore.Authorization;
using NannyModels.Models.UserModels;
using NannyModels.Models.AddressModels;
using NannyModels.Models.ChildModels;

namespace NannyAPI.GraphQL.Users
{
    /// <summary>
    /// The User ObjectType
    /// </summary>
    public class UserType : ObjectType<ApplicationUser>
    {
        protected override void Configure(IObjectTypeDescriptor<ApplicationUser> descriptor)
        {
            descriptor.Description("The user information on file");
            descriptor.Field(u => u.Addresses)
                .ResolveWith<Resolvers>(u => u.GetAddressByUserID(default!, default!))
                .Description("Gets the address by the Users id");

            descriptor.Field(u => u.Children)
                .ResolveWith<Resolvers>(u => u.GetChildrenByUserID(default!, default!))
                .Description("Gets the users children");

            descriptor.Field(u => u.Roles)
                .Argument("roles", a => a.Type<RoleType>())
                .Resolve(context => context.ArgumentValue<Role>("roles"))
                .ResolveWith<Resolvers>(u => u.GetRolesByUserID(default!, default!))
                .Description("Gets the users roles");

            // Descriptions
            descriptor.Field(u => u.UserID).Description("The Users id");
            descriptor.Field(u => u.FirstName).Description("The Users first name");
            descriptor.Field(u => u.LastName).Description("The Users last name");
            descriptor.Field(u => u.UserName).Description("The Users user name");
            descriptor.Field(u => u.EmailAddress).Description("The Users email address");
            descriptor.Field(u => u.PhoneNumber).Description("The Users phone number");
            descriptor.Field(u => u.Roles).Description("The Users roles");
            descriptor.Field(u => u.Addresses).Description("The Users addresses");
        }

        public class Resolvers
        {
            /// <summary>
            /// Gets all the users addresses
            /// </summary>
            /// <param name="user">The user</param>
            /// <param name="addressDAO">The address dao</param>
            /// <returns>Collection of addresses</returns>
            [Authorize]
            public ICollection<Address> GetAddressByUserID([Parent] ApplicationUser user, [Service] IAddressDAO addressDAO)
            {
                return addressDAO.GetAddressesByUserID(user.UserID);
            }

            /// <summary>
            /// Gets all the childrent connected to the user
            /// </summary>
            /// <param name="user">The user</param>
            /// <param name="childDAO">The child dao</param>
            /// <returns>The children</returns>
            [Authorize]
            public ICollection<Child> GetChildrenByUserID([Parent] ApplicationUser user, [Service] IChildDAO childDAO)
            {
                return childDAO.GetChildrenByUserID(user.UserID);
            }

            /// <summary>
            /// Gets all of the users roles
            /// </summary>
            /// <param name="user">The user</param>
            /// <param name="roleDAO">The role dao</param>
            /// <returns>The roles</returns>
            [Authorize]
            public ICollection<Role> GetRolesByUserID([Parent] ApplicationUser user, [Service] IRoleDAO roleDAO)
            {
                return roleDAO.GetRolesByUserID(user.UserID);
            }
        }

        public class RoleType : EnumType<Role> { }
    }
}
