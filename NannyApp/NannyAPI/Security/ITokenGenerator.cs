using NannyModels.Enumerations;

namespace NannyAPI.Security
{
    public interface ITokenGenerator
    {
        /// <summary>
        /// Generates a new authentication token.
        /// </summary>
        /// <param name="userID">The users id</param>
        /// <param name="userName">The usersName</param>
        /// <returns>The token</returns>
        string GenerateToken(int userID, string userName);

        /// <summary>
        /// Generates a new authentication token.
        /// </summary>
        /// <param name="userID">The users id</param>
        /// <param name="userName">The usersName</param>
        /// <param name="role">The role</param>
        /// <returns></returns>
        string GenerateToken(int userID, string userName, ICollection<Role> roles);
    }
}
