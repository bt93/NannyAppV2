namespace NannyModels.Models.UserModels
{
    /// <summary>
    /// Check for username and email
    /// </summary>
    public class UserCheck
    {
        /// <summary>
        /// Gets or sets if the username exists
        /// </summary>
        /// <value>
        /// If the username exists
        /// </value>
        public bool UserNameExists { get; set; }

        /// <summary>
        /// Gets or sets if the email exists
        /// </summary>
        /// <value>
        /// If the email exists
        /// </value>
        public bool EmailAddressExists { get; set; }
    }
}
