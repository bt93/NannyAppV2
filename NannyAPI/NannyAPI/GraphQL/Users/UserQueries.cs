using NannyData.Interfaces;
using NannyModels.Models;

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
        public  ApplicationUser GetUser()
        {
            return _userDAO.GetUserByID(1);
        }
    }
}
