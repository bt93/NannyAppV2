﻿using HotChocolate.AspNetCore.Authorization;
using NannyAPI.Miscellaneous.Errors;
using NannyAPI.Security;
using NannyAPI.Security.Models;
using NannyData.Interfaces;
using NannyModels.Enumerations;
using NannyModels.Models;
using NannyModels.Models.User;
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
        public LoginPayload Login(LoginInput input)
        {
            var userCheck = _userDAO.GetUserForLogin(input.userNameOrEmail);

            if (userCheck.UserID == 0 || userCheck is null) { throw new UnauthorizedAccessException("Username or password incorect."); }

            if (_passwordHasher.VerifyHashMatch(userCheck.Password ?? string.Empty, input.password, userCheck.Salt ?? string.Empty))
            {
                List<Role> roles = new List<Role>();
                roles.AddRange(_roleDAO.GetRolesByUserID(userCheck.UserID));
                Role roleToUse;

                if (roles.Contains(Role.Admin)) { roleToUse = Role.Admin; }
                else { roleToUse = roles.FirstOrDefault(); }

                var token = _tokenGenerator.GenerateToken(userCheck.UserID, userCheck.UserName ?? string.Empty, roleToUse);
                
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
                var token = _tokenGenerator.GenerateToken(userID, input?.user.UserName ?? string.Empty, input?.user.RoleID ?? Role.Uninitialized);

                var roles = new List<Role>();
                roles.Add(input?.user.RoleID ?? Role.Uninitialized);

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

        [Authorize]
        public bool UpdateUser(string firstName, string lastName, string phoneNumber, ClaimsPrincipal claimsPrincipal)
        {
            claimsPrincipal.UserNullCheck();
            string id = claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);

            return _userDAO.UpdateUser(Int32.Parse(id), firstName, lastName, phoneNumber);
        }

        [Authorize]
        public bool VerifyUser(int userID, ClaimsPrincipal claimsPrincipal)
        {
            claimsPrincipal.CheckUserIdentity(userID);
            return _userDAO.VerifyUser(userID);
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
