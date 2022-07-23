using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NannyAPI.GraphQL.Users;
using NannyAPI.Security;
using NannyData.Interfaces;
using NannyModels.Enumerations;
using NannyModels.Models.AddressModels;
using NannyModels.Models.UserModels;

namespace NannyAPI.Authentication
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IUserDAO _userDAO;
        private readonly IRoleDAO _roleDAO;
        private readonly IPasswordHasher _passwordHasher;
        private readonly ITokenGenerator _tokenGenerator;

        public AuthenticationController(IUserDAO userDAO, IRoleDAO roleDAO, IPasswordHasher passwordHasher, ITokenGenerator tokenGenerator)
        {
            _userDAO = userDAO;
            _roleDAO = roleDAO;
            _passwordHasher = passwordHasher;
            _tokenGenerator = tokenGenerator;
        }

        [HttpPost("login")]
        public IActionResult Login(LoginInput input)
        {
            var userCheck = _userDAO.GetUserForLogin(input.userNameOrEmail);

            if (userCheck.UserID == 0 || userCheck is null) { return BadRequest("Username or password incorect."); }

            if (_passwordHasher.VerifyHashMatch(userCheck.Password ?? string.Empty, input.password, userCheck.Salt ?? string.Empty))
            {
                List<Role> roles = new List<Role>();
                roles.AddRange(_roleDAO.GetRolesByUserID(userCheck.UserID));

                var token = _tokenGenerator.GenerateToken(userCheck.UserID, userCheck.UserName ?? string.Empty, roles);

                var returnUser = new ReturnUser()
                {
                    UserID = userCheck.UserID,
                    Token = token
                };

                return Ok(returnUser);
            }

            return BadRequest("Username or password incorect.");
        }

        [HttpPost("register")]
        public IActionResult Register(RegisterInput input)
        {
            if (input is null || input?.user.Addresses.Count == 0) { return BadRequest("User input is null."); }

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
                    Token = token
                };

                return Ok(returnUser);
            }

            return BadRequest("Something went wrong, please try again later");
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
