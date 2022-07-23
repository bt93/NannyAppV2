using HotChocolate.AspNetCore.Authorization;
using NannyAPI.Miscellaneous.Errors;
using NannyAPI.Security;
using NannyData.Interfaces;
using NannyModels.Enumerations;
using NannyModels.Models.AddressModels;
using NannyModels.Models.UserModels;
using System.Security.Claims;

namespace NannyAPI.GraphQL.Users
{
    /// <summary>
    /// The User Mutations
    /// </summary>
    [ExtendObjectType(extendsType: typeof(Mutation))]
    public class UserMutations
    {
        private readonly IUserDAO _userDAO;
        private readonly IRoleDAO _roleDAO;
        private readonly IPasswordHasher _passwordHasher;
        private readonly ITokenGenerator _tokenGenerator;

        public UserMutations(IUserDAO userDAO, IRoleDAO roleDAO, IPasswordHasher passwordHasher, ITokenGenerator tokenGenerator)
        {
            _userDAO = userDAO;
            _roleDAO = roleDAO;
            _passwordHasher = passwordHasher;
            _tokenGenerator = tokenGenerator;
        }

        /// <summary>
        /// Updates a users info
        /// </summary>
        /// <param name="firstName">Users first name</param>
        /// <param name="lastName">Users last name</param>
        /// <param name="phoneNumber">Users phone number</param>
        /// <param name="claimsPrincipal">The verified user</param>
        /// <returns></returns>
        [Authorize]
        [GraphQLDescription("Updates current user")]
        public bool UpdateUser(string firstName, string lastName, string phoneNumber, ClaimsPrincipal claimsPrincipal)
        {
            claimsPrincipal.UserNullCheck();
            string id = claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);

            return _userDAO.UpdateUser(Int32.Parse(id), firstName, lastName, phoneNumber);
        }

        /// <summary>
        /// Verifies the user
        /// </summary>
        /// <param name="userID">The user id</param>
        /// <param name="claimsPrincipal">The verified user</param>
        /// <returns>True if verified</returns>
        [Authorize]
        [GraphQLDescription("Verifies the user.")]
        public bool VerifyUser(int userID, ClaimsPrincipal claimsPrincipal)
        {
            claimsPrincipal.CheckUserIdentity(userID);
            return _userDAO.VerifyUser(userID);
        }

        /// <summary>
        /// Deactivates a user"
        /// </summary>
        /// <param name="claimsPrincipal">The verified user</param>
        /// <returns>If successful</returns>
        [Authorize]
        [GraphQLDescription("Deactivates a user")]
        public bool DeactivateUser(ClaimsPrincipal claimsPrincipal)
        {
            claimsPrincipal.UserNullCheck();
            string id = claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);

            return _userDAO.DeactivateUser(int.Parse(id));
        }

        /// <summary>
        /// Activates a deactive user
        /// </summary>
        /// <param name="claimsPrincipal">The verified user</param>
        /// <returns>If successful</returns>
        [Authorize]
        [GraphQLDescription("Activates a deactive user")]
        public bool ActivateUser(ClaimsPrincipal claimsPrincipal)
        {
            claimsPrincipal.UserNullCheck();
            string id = claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);

            return _userDAO.ActivateUser(int.Parse(id));
        }

        [Authorize]
        [GraphQLDescription("Updates a users password.")]
        public bool UpdateUserPassword(PasswordUpdateInput input, ClaimsPrincipal claimsPrincipal)
        {
            claimsPrincipal.UserNullCheck();
            string id = claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);

            PasswordHash currentPassword = _userDAO.GetUserPassword(int.Parse(id));

            if (_passwordHasher.VerifyHashMatch(currentPassword.Password ?? string.Empty, input.CurrentPassword ?? string.Empty, currentPassword.Salt ?? string.Empty))
            {
                PasswordHash newPassword = _passwordHasher.ComputeHash(input.NewPassword ?? string.Empty);

                return _userDAO.UpdateUserPassword(newPassword, int.Parse(id));
            }

            throw new Exception("Current Password is incorrect.");
        }
    }
}
