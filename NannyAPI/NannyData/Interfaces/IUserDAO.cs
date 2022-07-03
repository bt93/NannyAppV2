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
        /// Gets the user info for login
        /// </summary>
        /// <param name="userInput">The users email or username</param>
        /// <returns>The User</returns>
        public ApplicationUser GetUserForLogin(string userInput);
    }
}
