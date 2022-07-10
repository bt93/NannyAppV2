using HotChocolate.AspNetCore.Authorization;
using NannyAPI.Miscellaneous.Errors;
using NannyData.Interfaces;
using NannyModels.Enumerations;
using NannyModels.Models.UserModels;
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

        [Authorize]
        public ICollection<ApplicationUser> GetMyParents(ClaimsPrincipal claimsPrincipal)
        {
            claimsPrincipal.UserNullCheck();
            string id = claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);

            return _userDAO.GetUserConnectedByChild(Int32.Parse(id), Role.Parent);

            throw new Exception("Something went wrong");
        }

        [Authorize]
        public ICollection<ApplicationUser> GetMyCaretakers(ClaimsPrincipal claimsPrincipal)
        {
            claimsPrincipal.UserNullCheck();
            string id = claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);

            return _userDAO.GetUserConnectedByChild(Int32.Parse(id), Role.Caretaker);

            throw new Exception("Something went wrong");
        }
    }
}
