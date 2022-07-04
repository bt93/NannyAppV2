using HotChocolate.AspNetCore.Authorization;
using NannyAPI.Miscellaneous.Errors;
using NannyData.Interfaces;
using NannyModels.Enumerations;
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

        public UserQueries(IUserDAO userDAO, ClaimsPrincipal claimsPrincipal)
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
            claimsPrincipal.UserNullCheck();
            string id = claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);

            return _userDAO.GetUserByID(Int32.Parse(id));
        }

        [Authorize(Roles = new[] { "Admin" } )]
        public ApplicationUser GetUser(int userID)
        {
            var result = _userDAO.GetUserByID(userID);
            return result.UserID > 0 ? result : throw new Exception("User does not exist");
        }

        [Authorize(Roles = new[] { "Caretaker" })]
        public ICollection<ApplicationUser> GetMyParents(ClaimsPrincipal claimsPrincipal)
        {
            claimsPrincipal.UserNullCheck();
            string id = claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);
            string role = claimsPrincipal.FindFirstValue(ClaimTypes.Role);

            if (id is null)
            {
                throw new UnauthorizedAccessException("User not logged in");
            }

            Role roleID;

            if (Enum.TryParse(role, out roleID))
            {
                return _userDAO.GetUserConnectedByChild(Int32.Parse(id), roleID);
            }

            throw new Exception("Something went wrong");
        }
    }
}
