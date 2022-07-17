using HotChocolate.AspNetCore.Authorization;
using NannyAPI.Miscellaneous.Errors;
using NannyAPI.Security;
using NannyAPI.Security.Models;
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
        /// Checks if user exsits by their username or email address, checks if the passwordhash matches what is in the db
        /// and sends a token if it does.
        /// </summary>
        /// <param name="input">The username/email and password</param>
        /// <returns>The user</returns>
        /// <exception cref="UnauthorizedAccessException">If the user doesn't exist or password is incorrect</exception>
        [GraphQLDescription("Verifies a user and returns a token")]
        public LoginPayload Login(LoginInput input)
        {
            var userCheck = _userDAO.GetUserForLogin(input.userNameOrEmail);

            if (userCheck.UserID == 0 || userCheck is null) { throw new UnauthorizedAccessException("Username or password incorect."); }

            if (_passwordHasher.VerifyHashMatch(userCheck.Password ?? string.Empty, input.password, userCheck.Salt ?? string.Empty))
            {
                List<Role> roles = new List<Role>();
                roles.AddRange(_roleDAO.GetRolesByUserID(userCheck.UserID));

                var token = _tokenGenerator.GenerateToken(userCheck.UserID, userCheck.UserName ?? string.Empty, roles);
                
                var returnUser = new ReturnUser()
                {
                    UserID = userCheck.UserID,
                    EmailAddress = userCheck.EmailAddress,
                    UserName = userCheck.UserName,
                    Roles = roles,
                    Token = token
                };

                return new LoginPayload(returnUser);
            }

            throw new UnauthorizedAccessException("Username or password incorect.");
        }

        /// <summary>
        /// Checks if the user already exists. If not, hashes the password, inserts the new ueser and then sends a token
        /// </summary>
        /// <param name="input">The new users input </param>
        /// <returns>The register payload</returns>
        /// <exception cref="Exception"></exception>
        /// <exception cref="UnauthorizedAccessException"></exception>
        [GraphQLDescription("Registers a user and sends a token")]
        public RegisterPayload Register(RegisterInput input)
        {
            if (input is null || input?.user.Addresses.Count == 0) { throw new Exception("User input is null."); }

            UserCheck checkUser = _userDAO.DoesUserExist(input?.user.UserName ?? string.Empty, input?.user.EmailAddress ?? string.Empty);

            if (checkUser.UserNameExists) { throw new UnauthorizedAccessException("This username already exists."); }
            if (checkUser.EmailAddressExists) { throw new UnauthorizedAccessException("This email address already exists."); }


            var hashedPassword = _passwordHasher.ComputeHash(input?.user.Password ?? string.Empty);
           
            var userID = _userDAO.AddNewUser(MapUser(input ?? new RegisterInput(new ApplicationUserInput()), hashedPassword));
            
            if (userID > 0)
            {

                var roles = new List<Role>();
                roles.Add(input?.user.RoleID ?? Role.Uninitialized);

                var token = _tokenGenerator.GenerateToken(userID, input?.user.UserName ?? string.Empty, roles);

                var returnUser = new ReturnUser()
                {
                    UserID = userID,
                    EmailAddress = input?.user.EmailAddress,
                    UserName = input?.user.UserName,
                    Roles = roles,
                    Token = token
                };

                return new RegisterPayload(returnUser);
            }

            throw new Exception("Something went wrong, please try again later");
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

        [Authorize]
        [GraphQLDescription("Activates a deactive user")]
        public bool ActivateUser(ClaimsPrincipal claimsPrincipal)
        {
            claimsPrincipal.UserNullCheck();
            string id = claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);

            return _userDAO.ActivateUser(int.Parse(id));
        }

        private ApplicationUser MapUser(RegisterInput input, PasswordHash hashedPassword)
        {
            var address = new Address()
            {
                Address1 = input.user.Addresses.First().Address1,
                Address2 = input.user.Addresses.First().Address2,
                Address3 = input.user.Addresses.First().Address3,
                Address4 = input.user.Addresses.First().Address4,
                Locality = input.user.Addresses.First().Locality,
                Region = input.user.Addresses.First().Locality,
                PostalCode = input.user.Addresses.First().PostalCode,
                Country = input.user.Addresses.First().Country,
                County = input.user.Addresses.First().County,
            };

            var addresses = new List<Address>();
            var roles = new List<Role>();
            addresses.Add(address);
            roles.Add(input.user.RoleID);

            var user = new ApplicationUser()
            {
                FirstName = input.user.FirstName,
                LastName = input.user.LastName,
                UserName = input.user.UserName,
                EmailAddress = input.user.EmailAddress,
                Password = hashedPassword.Password,
                Salt = hashedPassword.Salt,
                PhoneNumber = input.user.PhoneNumber,
                Roles = roles,
                IsVerified = false,
                Addresses = addresses,
            };

            return user;
        }
    }
}
