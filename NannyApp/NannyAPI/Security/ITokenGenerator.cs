using NannyModels.Enumerations;
using NannyModels.Models.Authentication;

namespace NannyAPI.Security
{
    public interface ITokenGenerator
    {
        /// <summary>
        /// Generates a new authentication token.
        /// </summary>
        /// <param name="userID">The users id</param>
        /// <param name="userName">The usersName</param>
        /// <returns>The authentication token and refresh token</returns>
        AuthenticationResult GenerateToken(int userID, string userName);

        /// <summary>
        /// Generates a new authentication token.
        /// </summary>
        /// <param name="userID">The users id</param>
        /// <param name="userName">The usersName</param>
        /// <param name="role">The role</param>
        /// <returns>The authentication token and refresh token</returns>
        AuthenticationResult GenerateToken(int userID, string userName, ICollection<Role> roles);

        /// <summary>
        /// Verfies the tokens and generates new tokens
        /// </summary>
        /// <param name="request"></param>
        /// <returns>The authentication token and refresh token</returns>
        AuthenticationResult VerifyToken(TokenRequest request);
    }
}
