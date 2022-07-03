using HotChocolate.AspNetCore.Authorization;
using NannyData.Interfaces;
using NannyModels.Models;
using System.Security.Claims;

namespace NannyAPI.GraphQL.Users
{
    /// <summary>
    /// The User Queries
    /// </summary>
    [ExtendObjectType(extendsType: typeof(Query))]
    public class UserQueries
    {
        private readonly IUserDAO _userDAO;

        public UserQueries(IUserDAO userDAO)
        {
            _userDAO = userDAO;
        }

        /// <summary>
        /// Gets the current User By their ID
        /// </summary>
        /// <returns>The User</returns>
        [Authorize]
        public ApplicationUser GetCurrentUser(ClaimsPrincipal claimsPrincipal)
        {
            string id = claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);
            return _userDAO.GetUserByID(Int32.Parse(id));
        }

        [Authorize(Roles = new[] { "Admin" } )]
        public ApplicationUser GetUser(int userID)
        {
            return _userDAO.GetUserByID(userID);
        }
    }
}
