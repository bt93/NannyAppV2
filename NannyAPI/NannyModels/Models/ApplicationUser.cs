﻿using HotChocolate;
using NannyModels.Enumerations;
using System.ComponentModel.DataAnnotations;

namespace NannyModels.Models
{
    public class ApplicationUser
    {
        /// <summary>
        /// Gets or sets the UserID
        /// </summary>
        /// <value>
        /// The UserID
        /// </value>
        [Key]
        public int UserID { get; set; }

        /// <summary>
        /// Gets or sets the Users First Name
        /// </summary>
        /// <value>
        /// The Users First Name
        /// </value>
        [Required(ErrorMessage = "{0} is required")]
        [MaxLength(80)]
        public string? FirstName { get; set; }

        /// <summary>
        /// Gets or sets the Users Last Name
        /// </summary>
        /// <value>
        /// The Users Last Name
        /// </value>
        [Required(ErrorMessage = "{0} is required")]
        [MaxLength(80)]
        public string? LastName { get; set; }

        /// <summary>
        /// Gets or sets the Users UserName
        /// </summary>
        /// <value>
        /// The Users UserName
        /// </value>
        [Required(ErrorMessage = "{0} is required")]
        [MaxLength(80)]
        public string? UserName { get; set; }

        /// <summary>
        /// Gets or sets the Users Email Address
        /// </summary>
        /// <value>
        /// The Users Email Address
        /// </value>
        [Required(ErrorMessage = "{0} is required")]
        [EmailAddress]
        [MaxLength(120)]
        public string? EmailAddress { get; set; }

        /// <summary>
        /// Gets or sets the Users Password
        /// </summary>
        /// <value>
        /// The Users Password
        /// </value>
        [Required(ErrorMessage = "{0} is required")]
        [StringLength(250, MinimumLength = 6)]
        [GraphQLIgnore]
        public string? Password { get; set; }

        /// <summary>
        /// Gets or sets the Phone Number
        /// </summary>
        /// <value>
        /// The Users Phone Number
        /// </value>
        [Required(ErrorMessage = "{0} is required")]
        [Phone]
        [MaxLength(120)]
        public string? PhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets the salt
        /// </summary>
        /// <value>
        /// The Users salt
        /// </value>
        [GraphQLIgnore]
        public string? Salt { get; set; }

        /// <summary>
        /// Gets or sets the Users Role
        /// </summary>
        /// <value>
        /// The Users Role
        /// </value>
        public Role RoleID { get; set; }


        /// <summary>
        /// Gets or sets if the is is verified
        /// </summary>
        /// <value>
        /// The Users Role
        /// </value>
        [GraphQLIgnore]
        public bool IsVerified { get; set; }

        /// <summary>
        /// Gets or sets a list of the users addresses
        /// </summary>
        /// <value>
        /// The Users Addresses
        /// </value>
        public ICollection<Address> Addresses { get; set; } = new List<Address>();
    }

    public class ReturnUser
    {
        public int UserID { get; set; }
        public string? EmailAddress { get; set; }
        public string? UserName { get; set; }
        public Role? Role { get; set; }
        public string? Token { get; set; }
    }
}
