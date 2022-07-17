using NannyModels.Enumerations;
using NannyModels.Models.UserModels;

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
        public ICollection<ApplicationUser> GetUsersByChildID(int childID, int userID, Role roleID);

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

        /// <summary>
        /// Updates a users first name, last name and  phone number
        /// </summary>
        /// <param name="userID">The user id</param>
        /// <param name="firstName">The users first name</param>
        /// <param name="lastName">The users last name</param>
        /// <param name="phoneNumber">The users phone number</param>
        /// <returns></returns>
        public bool UpdateUser(int userID, string firstName, string lastName, string phoneNumber);

        /// <summary>
        /// Updates the User to IsVerified true
        /// </summary>
        /// <param name="userID">The user id</param>
        public bool VerifyUser(int userID);

        /// <summary>
        /// Deactivates a user
        /// </summary>
        /// <param name="userID">The user id</param>
        /// <returns>If successful</returns>
        public bool DeactivateUser(int userID);

        /// <summary>
        /// Activates a user
        /// </summary>
        /// <param name="userID">The user id</param>
        /// <returns>If successful</returns>
        public bool ActivateUser(int userID);

        /// <summary>
        /// Updates a users password and salt
        /// </summary>
        /// <param name="passwordHash">The hashed password</param>
        /// <param name="userID">The user id</param>
        /// <returns>If Successful</returns>
        public bool UpdateUserPassword(PasswordHash passwordHash, int userID);

        /// <summary>
        /// Gets a users password and salt
        /// </summary>
        /// <param name="userID">The users id</param>
        /// <returns>The Hashed password and salt</returns>
        public PasswordHash GetUserPassword(int userID);
    }
}
