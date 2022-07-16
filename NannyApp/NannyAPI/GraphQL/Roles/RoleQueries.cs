using HotChocolate.AspNetCore.Authorization;
using NannyAPI.Miscellaneous.Errors;
using NannyData.Interfaces;
using NannyModels.Enumerations;
using System.Security.Claims;

namespace NannyAPI.GraphQL.Roles
{
    /// <summary>
    /// The Role Queries
    /// </summary>
    [ExtendObjectType(extendsType: typeof(Query))]
    public class RoleQueries
    {
        private readonly IRoleDAO _roleDAO;

        public RoleQueries(IRoleDAO roleDAO)
        {
            _roleDAO = roleDAO;
        }

        /// <summary>
        /// Gets the current users roles
        /// </summary>
        /// <param name="claimsPrincipal">The verfied user</param>
        /// <returns>A collection of roles</returns>
        [Authorize]
        [GraphQLDescription("Gets the current users roles")]
        public ICollection<Role> GetMyRoles(ClaimsPrincipal claimsPrincipal)
        {
            claimsPrincipal.UserNullCheck();
            string id = claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);

            return _roleDAO.GetRolesByUserID(Int32.Parse(id));
        }

        /// <summary>
        /// Gets a users role by their id. Only allowed for admin.
        /// </summary>
        /// <param name="userID">The users id</param>
        /// <returns>A collection of roles</returns>
        [Authorize(Roles = new[] { "Admin" })]
        [GraphQLDescription("Gets the roles for a user by their id. Only allowed by admins.")]
        public ICollection<Role> GetRolesByID(int userID)
        {
            return _roleDAO.GetRolesByUserID(userID);
        }
    }
}
