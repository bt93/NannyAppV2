using NannyAPI.Security;
using NannyData.Interfaces;
using NannyModels.Models;

namespace NannyAPI.GraphQL.Users
{
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
