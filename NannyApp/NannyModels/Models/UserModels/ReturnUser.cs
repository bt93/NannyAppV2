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
        /// Gets or sets the EmailAddress
        /// </summary>
        /// <value>
        /// The EmailAddress
        /// </value>
        public string? EmailAddress { get; set; }

        /// <summary>
        /// Gets or sets the UserName
        /// </summary>
        /// <value>
        /// The UserName
        /// </value>
        public string? UserName { get; set; }

        /// <summary>
        /// Gets or sets the Role
        /// </summary>
        /// <value>
        /// The Role
        /// </value>
        public ICollection<Role>? Roles { get; set; } = new List<Role>();

        /// <summary>
        /// Gets or sets the Token
        /// </summary>
        /// <value>
        /// The Token
        /// </value>
        public string? Token { get; set; }
    }
}
