using NannyAPI.Security;
using NannyData.Interfaces;
using NannyModels.Models;

namespace NannyAPI.GraphQL.Users
{
    /// <summary>
    /// The User Mutations
    /// </summary>
    [ExtendObjectType(extendsType: typeof(Mutation))]
    public class UserMutations
    {
        private readonly IUserDAO _userDAO;
        private readonly IPasswordHasher _passwordHasher;
        private readonly ITokenGenerator _tokenGenerator;

        public UserMutations(IUserDAO userDAO, IPasswordHasher passwordHasher, ITokenGenerator tokenGenerator)
        {
            _userDAO = userDAO;
            _passwordHasher = passwordHasher;
            _tokenGenerator = tokenGenerator;
        }

        /// <summary>
        /// Checks if user exsits by their username or email address, checks if the passwordhash matches what is in the db
        /// and sends a token if it does.
        /// </summary>
        /// <param name="input">The username/email and password</param>
        /// <returns>The user</returns>
        /// <exception cref="UnauthorizedAccessException">If the user doesn't exist or password is incorrect</exception>
        public LoginPayload Login(LoginInput input)
        {
            var userCheck = _userDAO.GetUserForLogin(input.userNameOrEmail);

            if (userCheck.UserID == 0 || userCheck is null)
            {
                throw new UnauthorizedAccessException("Username or password incorect.");
            }

            if (_passwordHasher.VerifyHashMatch(userCheck.Password, input.password, userCheck.Salt))
            {
                var token = _tokenGenerator.GenerateToken(userCheck.UserID, userCheck.UserName, userCheck.RoleID);
                
                var returnUser = new ReturnUser()
                {
                    UserID = userCheck.UserID,
                    EmailAddress = userCheck.EmailAddress,
                    UserName = userCheck.UserName,
                    Role = userCheck.RoleID,
                    Token = token
                };

                return new LoginPayload(returnUser);
            }

            throw new UnauthorizedAccessException("Username or password incorect.");
        }


    }
}
