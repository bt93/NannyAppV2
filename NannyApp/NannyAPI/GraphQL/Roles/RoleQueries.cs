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

        [Authorize]
        public List<Role> GetMyRoles(ClaimsPrincipal claimsPrincipal)
        {
            claimsPrincipal.UserNullCheck();
            string id = claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);

            return _roleDAO.GetRolesByUserID(Int32.Parse(id));
        }

        [Authorize(Roles = new[] { "Admin" })]
        public List<Role> GetRolesByID(int userID)
        {
            return _roleDAO.GetRolesByUserID(userID);
        }
    }
}
