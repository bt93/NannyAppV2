using NannyModels.Models.UserModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NannyModels.Models.Authentication
{
    public class RefreshToken
    {
        /// <summary>
        /// Gets or sets the token id
        /// </summary>
        /// <value>
        /// The token id
        /// </value>
        public int TokenID { get; set; }

        /// <summary>
        /// Gets or sets the user id
        /// </summary>
        /// <value>
        /// The user id
        /// </value>
        public int UserID { get; set; }

        /// <summary>
        /// Gets or sets the token 
        /// </summary>
        /// <value>
        /// The token 
        /// </value>
        public string? Token { get; set; }

        /// <summary>
        /// Gets or sets the jwt id
        /// </summary>
        /// <value>
        /// The jwt id
        /// </value>
        public string? JWTID { get; set; }

        /// <summary>
        /// Gets or sets if the token is already used
        /// </summary>
        /// <value>
        /// If the token is already used
        /// </value>
        public bool IsUsed { get; set; }

        /// <summary>
        /// Gets or sets if the token is revoked
        /// </summary>
        /// <value>
        /// If the token is revoked
        /// </value>
        public bool IsRevoked { get; set; }

        /// <summary>
        /// Gets or sets the date the token was created
        /// </summary>
        /// <value>
        /// The date the token was created
        /// </value>
        public DateTimeOffset DateAdded { get; set; }

        /// <summary>
        /// Gets or sets the date the token was expired
        /// </summary>
        /// <value>
        /// The date the token was expired
        /// </value>
        public DateTimeOffset DateExpired { get; set; }

        /// <summary>
        /// Gets or sets the user the token applies to
        /// </summary>
        /// <value>
        /// The user the token applies to
        /// </value>
        public ApplicationUser User { get; set; } = new ApplicationUser();
    }
}
