using System.Text.Json.Serialization;

namespace NannyModels.Models.Authentication
{
    public class AuthenticationResult
    {
        /// <summary>
        /// Gets or sets the auth token
        /// </summary>
        /// <value>
        /// The auth token
        /// </value>
        [JsonIgnore]
        public string? Token { get; set; }

        /// <summary>
        /// Gets or sets the refresh token
        /// </summary>
        /// <value>
        /// The refresh token
        /// </value>
        [JsonIgnore]
        public string? RefreshToken { get; set; }

        /// <summary>
        /// Gets or sets the user id
        /// </summary>
        /// <value>
        /// The user id
        /// </value>
        [JsonIgnore]
        public int UserID { get; set; }

        /// <summary>
        /// Gets or sets if request was successful
        /// </summary>
        /// <value>
        /// If request was successful
        /// </value>
        public bool Success { get; set; }

        /// <summary>
        /// Gets or sets a list of errors
        /// </summary>
        /// <value>
        /// A list of errors
        /// </value>
        public List<string>? Errors { get; set; }
    }
}
