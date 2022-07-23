using Microsoft.IdentityModel.Tokens;
using NannyData.Interfaces;
using NannyModels.Enumerations;
using NannyModels.Models.Authentication;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace NannyAPI.Security
{
    public class JWTGenerator : ITokenGenerator
    {
        private readonly string _jwtSecret;
        private readonly IRefreshTokenDAO _refreshTokenDAO;
        private readonly TokenValidationParameters _validationParameters;

        public JWTGenerator(string jwtSecret, IRefreshTokenDAO refreshTokenDAO, TokenValidationParameters validationParameters)
        {
            _jwtSecret = jwtSecret;
            _refreshTokenDAO = refreshTokenDAO;
            _validationParameters = validationParameters;
        }

        /// <inheritdoc />
        public AuthenticationResult GenerateToken(int userID, string userName)
        {
            return GenerateToken(userID, userName, new List<Role>());
        }

        /// <inheritdoc />
        public AuthenticationResult GenerateToken(int userID, string userName, ICollection<Role> roles)
        {
            List<Claim> claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Sub, userID.ToString()),
                new Claim(ClaimTypes.Name, userName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role.ToString()));
            }

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                IssuedAt = DateTime.UtcNow,
                Expires = DateTime.UtcNow.AddMinutes(15),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtSecret)), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = tokenHandler.WriteToken(token);

            var refreshToken = new RefreshToken()
            {
                JWTID = token.Id,
                IsUsed = false,
                IsRevoked = false,
                UserID = userID,
                DateAdded = DateTime.UtcNow,
                DateExpired = DateTime.UtcNow.AddMinutes(20),
                Token = RandomString(25) + Guid.NewGuid().ToString(),
            };

            int rowCount = _refreshTokenDAO.AddRefreshToken(refreshToken);

            return new AuthenticationResult()
            {
                Token = jwtToken,
                Success = rowCount > 0,
                RefreshToken = refreshToken.Token,
                UserID = userID
            };
        }

        public AuthenticationResult VerifyToken(TokenRequest request)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            try
            {
                // Check to make sure token meets validation params
                var principal = tokenHandler.ValidateToken(request.Token, _validationParameters, out var token);

                // check if token has valid securit algorithm
                if (token is JwtSecurityToken securityToken)
                {
                    var result = securityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase);

                    if (result is false)
                    {
                        return null;
                    }
                }
                
                // Get utc timestamp from token
                long.TryParse(principal.Claims?.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Exp)?.Value, out var utcDateExpired);

                if (utcDateExpired <= 0)
                {
                    return new AuthenticationResult()
                    {
                        Errors = new List<string>() { "Refresh token not present" },
                        Success = false
                    };
                }

                // Convert time stamp into date we can use
                //var dateExpired = UnixTimeStampToDateTime(utcDateExpired);

                //Send error if not yet expired
                //if (dateExpired > DateTime.UtcNow)
                //{
                //    return new AuthenticationResult()
                //    {
                //        Errors = new List<string>() { "Cannot refresh token since this token has not expired yet." },
                //        Success = false
                //    };
                //}

                var storedToken = _refreshTokenDAO.GetRefreshToken(request.RefreshToken ?? string.Empty);

                // Checks if null
                if (storedToken is null)
                {
                    return new AuthenticationResult()
                    {
                        Errors = new List<string>() { "Refresh token does not exist." },
                        Success = false
                    };
                }

                // Check if the token is expired
                if (DateTimeOffset.Now > storedToken.DateExpired)
                {
                    return new AuthenticationResult()
                    {
                        Errors = new List<string>() { "Refresh token is expired, user must log in again." },
                        Success = false
                    };
                }

                // If its been used
                if (storedToken.IsUsed)
                {
                    return new AuthenticationResult()
                    {
                        Errors = new List<string>() { "Refresh token has been used and cannot be re-used." },
                        Success = false
                    };
                }

                // If its been revoked
                if (storedToken.IsRevoked)
                {
                    return new AuthenticationResult()
                    {
                        Errors = new List<string>() { "Refresh token has been revoked." },
                        Success = false
                    };
                }

                // Gets the jwt token id
                var jwtTokenID = principal.Claims?.SingleOrDefault(x => x.Type.Equals(JwtRegisteredClaimNames.Jti))?.Value;

                if (!storedToken?.JWTID?.Equals(jwtTokenID) ?? false)
                {
                    return new AuthenticationResult()
                    {
                        Errors = new List<string>() { "Refresh token does not match." },
                        Success = false
                    };
                }

                int rowCount = _refreshTokenDAO.SetRefreshTokenToIsUsed(storedToken?.TokenID ?? 0);

                if (rowCount == 0)
                {
                    return new AuthenticationResult()
                    {
                        Errors = new List<string>() { "There was an issue refreshing the token. Please login again." },
                        Success = false
                    };
                }

                List<Role> roles = new List<Role>();
                var userName = principal.Claims?.SingleOrDefault(x => x.Type.Equals(ClaimTypes.Name))?.Value;
                var rolesToUse = principal.Claims?.Where(x => x.Type.Equals(ClaimTypes.Role));
                if (rolesToUse?.Any() ?? false)
                {
                    foreach (var role in rolesToUse.ToArray())
                    {
                        if (Enum.TryParse(typeof(Role), role.Value, out object? parsedRole))
                        {
                            roles.Add((Role)parsedRole);
                        }
                    }
                }

                return GenerateToken(storedToken?.UserID ?? 0, userName ?? string.Empty, roles);
            }
            catch(Exception ex)
            {
                return new AuthenticationResult()
                {
                    Errors = new List<string>() { "There was an issue refreshing the token. Please login again.", ex.Message },
                    Success = false
                };
            }
        }

        private string RandomString(int length)
        {
            var random = new Random();
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToUniversalTime();
            return dtDateTime;
        }
    }
}
