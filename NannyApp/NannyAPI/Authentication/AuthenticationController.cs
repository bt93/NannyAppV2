using Microsoft.AspNetCore.Mvc;
using NannyAPI.Security;
using NannyData.Interfaces;
using NannyModels.Enumerations;
using NannyModels.Models.AddressModels;
using NannyModels.Models.Authentication;
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

                try
                {
                    var token = _tokenGenerator.GenerateToken(userCheck.UserID, userCheck.UserName ?? string.Empty, roles);

                    Response.Cookies.Append("X-Access-Token", token.Token ?? string.Empty, new CookieOptions() { HttpOnly = true, SameSite = SameSiteMode.Strict });
                    Response.Cookies.Append("X-Refresh-Token", token.RefreshToken ?? string.Empty, new CookieOptions() { HttpOnly = true, SameSite = SameSiteMode.Strict });
                    Response.Cookies.Append("X-UserID", token.UserID.ToString(), new CookieOptions() { HttpOnly = true, SameSite = SameSiteMode.Strict });

                    return Ok(token);
                }
                catch (Exception ex)
                {
                    return BadRequest(new AuthenticationResult() { Errors = new List<string> { ex.Message } });
                }
            }

            return BadRequest("Username or password incorect.");
        }

        [HttpPost("refreshtoken")]
        public IActionResult RefreshToken(TokenRequest tokenRequest)
        {
            if (tokenRequest == null) { return BadRequest("No refresh token present."); }

            var tokenVerification = _tokenGenerator.VerifyToken(tokenRequest);

            if (tokenVerification is null)
            {
                return BadRequest(new AuthenticationResult()
                {
                    Errors = new List<string>() { "There was an issue refreshing the token. Please login again." },
                    Success = false
                });
            }

            if (!tokenVerification.Success) { return BadRequest(tokenVerification); }

            Response.Cookies.Append("X-Access-Token", tokenVerification.Token ?? string.Empty, new CookieOptions() { HttpOnly = true, SameSite = SameSiteMode.Strict });
            Response.Cookies.Append("X-Refresh-Token", tokenVerification.RefreshToken ?? string.Empty, new CookieOptions() { HttpOnly = true, SameSite = SameSiteMode.Strict });
            Response.Cookies.Append("X-UserID", tokenVerification.UserID.ToString(), new CookieOptions() { HttpOnly = true, SameSite = SameSiteMode.Strict });

            return Ok(tokenVerification);
        }

        [HttpPost("register")]
        public IActionResult Register(ApplicationUserInput input)
        {
            if (input is null || input?.Addresses.Count == 0) { return BadRequest("User input is null."); }

            UserCheck checkUser = _userDAO.DoesUserExist(input?.UserName ?? string.Empty, input?.EmailAddress ?? string.Empty);

            if (checkUser.UserNameExists) { throw new UnauthorizedAccessException("This username already exists."); }
            if (checkUser.EmailAddressExists) { throw new UnauthorizedAccessException("This email address already exists."); }


            var hashedPassword = _passwordHasher.ComputeHash(input?.Password ?? string.Empty);

            var userID = _userDAO.AddNewUser(MapUser(input ?? new ApplicationUserInput(), hashedPassword));

            if (userID > 0)
            {
                var roles = new List<Role>();
                roles.Add(input?.RoleID ?? Role.Uninitialized);

                var token = _tokenGenerator.GenerateToken(userID, input?.UserName ?? string.Empty, roles);

                Response.Cookies.Append("X-Access-Token", token.Token ?? string.Empty, new CookieOptions() { HttpOnly = true, SameSite = SameSiteMode.Strict });
                Response.Cookies.Append("X-Refresh-Token", token.RefreshToken ?? string.Empty, new CookieOptions() { HttpOnly = true, SameSite = SameSiteMode.Strict });
                Response.Cookies.Append("X-UserID", token.UserID.ToString(), new CookieOptions() { HttpOnly = true, SameSite = SameSiteMode.Strict });

                return Ok(token);
            }

            return BadRequest("Something went wrong, please try again later");
        }

        private ApplicationUser MapUser(ApplicationUserInput input, PasswordHash hashedPassword)
        {
            var address = new Address()
            {
                Address1 = input.Addresses.First().Address1,
                Address2 = input.Addresses.First().Address2,
                Address3 = input.Addresses.First().Address3,
                Address4 = input.Addresses.First().Address4,
                Locality = input.Addresses.First().Locality,
                Region = input.Addresses.First().Locality,
                PostalCode = input.Addresses.First().PostalCode,
                Country = input.Addresses.First().Country,
                County = input.Addresses.First().County,
            };

            var addresses = new List<Address>();
            var roles = new List<Role>();
            addresses.Add(address);
            roles.Add(input.RoleID);

            var user = new ApplicationUser()
            {
                FirstName = input.FirstName,
                LastName = input.LastName,
                UserName = input.UserName,
                EmailAddress = input.EmailAddress,
                Password = hashedPassword.Password,
                Salt = hashedPassword.Salt,
                PhoneNumber = input.PhoneNumber,
                Roles = roles,
                IsVerified = false,
                Addresses = addresses,
            };

            return user;
        }
    }
}
