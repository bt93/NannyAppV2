using Microsoft.IdentityModel.Tokens;
using NannyModels.Enumerations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace NannyAPI.Security
{
    public class JWTGenerator : ITokenGenerator
    {
        private readonly string _jwtSecret;

        public JWTGenerator(string jwtSecret)
        {
            _jwtSecret = jwtSecret;
        }

        /// <inheritdoc />
        public string GenerateToken(int userID, string userName)
        {
            return GenerateToken(userID, userName, Role.Uninitialized);
        }

        /// <inheritdoc />
        public string GenerateToken(int userID, string userName, Role role)
        {
            List<Claim> claims = new List<Claim>()
            {
                new Claim("sub", userID.ToString()),
                new Claim(ClaimTypes.Name, userName),
            };

            if (!role.Equals(Role.Uninitialized))
            {
                claims.Add(new Claim(ClaimTypes.Role, role.ToString()));
            }

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                IssuedAt = DateTime.UtcNow,
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtSecret)), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
