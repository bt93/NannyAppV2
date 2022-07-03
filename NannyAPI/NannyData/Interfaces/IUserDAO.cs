using NannyModels.Enumerations;
using NannyModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NannyData.Interfaces
{
    public interface IUserDAO
    {
        /// <summary>
        /// Gets a User by their ID
        /// </summary>
        /// <param name="id">The user id</param>
        /// <returns>The User</returns>
        public ApplicationUser GetUserByID(int id);

        /// <summary>
        /// Gets a User by their associated children
        /// </summary>
        /// <param name="id">The user id</param>
        /// <returns>The Users</returns>
        public ICollection<ApplicationUser> GetUsersByChildID(int childID);

        /// <summary>
        /// Gets the user info for login
        /// </summary>
        /// <param name="userInput">The users email or username</param>
        /// <returns>The User</returns>
        public ApplicationUser GetUserForLogin(string userInput);

        /// <summary>
        /// Gets the user that are connected to other users by their children
        /// </summary>
        /// <param name="userID">The users id</param>
        /// <param name="role">The users role</param>
        /// <returns>A collection of users</returns>
        public ICollection<ApplicationUser> GetUserConnectedByChild(int userID, Role role);

        /// <summary>
        /// Determines if the username or emailaddress exists
        /// </summary>
        /// <param name="userName">The username</param>
        /// <param name="emailAddress">The email</param>
        /// <returns>The usercheck</returns>
        public UserCheck DoesUserExist(string userName, string emailAddress);

        /// <summary>
        /// Adds new user to the database
        /// </summary>
        /// <param name="user">The user to add</param>
        /// <returns>If insert was successful</returns>
        public int AddNewUser(ApplicationUser user);
    }
}
