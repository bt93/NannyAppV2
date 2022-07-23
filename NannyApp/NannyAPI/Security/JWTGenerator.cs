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

        public JWTGenerator(string jwtSecret, IRefreshTokenDAO refreshTokenDAO)
        {
            _jwtSecret = jwtSecret;
            _refreshTokenDAO = refreshTokenDAO;
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
                new Claim(JwtRegisteredClaimNames.Name, userName),
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
                DateAdded = DateTime.Now,
                DateExpired = DateTime.Now.AddMinutes(20),
                Token = RandomString(25) + Guid.NewGuid().ToString(),
            };

            _refreshTokenDAO.AddRefreshToken(refreshToken);

            return new AuthenticationResult()
            {
                Token = jwtToken,
                Success = true,
                RefreshToken = refreshToken.Token
            };
        }

        private string RandomString(int length)
        {
            var random = new Random();
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
