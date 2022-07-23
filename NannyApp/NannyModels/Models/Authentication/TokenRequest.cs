using System.ComponentModel.DataAnnotations;

namespace NannyModels.Models.Authentication
{
    public class TokenRequest
    {
        /// <summary>
        /// Gets or sets the old token
        /// </summary>
        /// <value>
        /// The old token
        /// </value>
        [Required]
        public string? Token { get; set; }

        /// <summary>
        /// Gets or sets the refresh token
        /// </summary>
        /// <value>
        /// The refresh token
        /// </value>
        [Required]
        public string? RefreshToken { get; set; }
    }
}
