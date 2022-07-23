using NannyModels.Enumerations;

namespace NannyModels.Models.UserModels
{
    /// <summary>
    /// The returned user on successful login
    /// </summary>
    public class ReturnUser
    {
        /// <summary>
        /// Gets or sets the UserID
        /// </summary>
        /// <value>
        /// The UserID
        /// </value>
        public int UserID { get; set; }

        /// <summary>
        /// Gets or sets the Token
        /// </summary>
        /// <value>
        /// The Token
        /// </value>
        public string? Token { get; set; }
    }
}
