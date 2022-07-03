using NannyData.Interfaces;
using NannyModels.Models;

namespace NannyAPI.GraphQL.Users
{
    [ExtendObjectType(extendsType: typeof(Query))]
    public class UserQueries
    {
        private readonly IUserDAO _userDAO;

        public UserQueries(IUserDAO userDAO)
        {
            _userDAO = userDAO;
        }

        public  ApplicationUser GetUser(int id)
        {
            return _userDAO.GetUserByID(id);
        }
    }
}
