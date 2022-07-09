using HotChocolate;
using NannyModels.Enumerations;
using System.ComponentModel.DataAnnotations;

namespace NannyModels.Models.User
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
        public ICollection<Role> RoleID { get; set; } = new List<Role>();


        /// <summary>
        /// Gets or sets if the is is verified
        /// </summary>
        /// <value>
        /// The Users Role
        /// </value>
        [GraphQLIgnore]
        public bool IsVerified { get; set; }

        /// <summary>
        /// Gets or sets if the user is active
        /// </summary>
        /// <value>
        /// If the user is activee
        /// </value>
        public bool IsActive { get; set; }

        /// <summary>
        /// Gets or sets a list of the users addresses
        /// </summary>
        /// <value>
        /// The Users Addresses
        /// </value>
        public ICollection<Address> Addresses { get; set; } = new List<Address>();

        /// <summary>
        /// Gets or sets a list of the users Children
        /// </summary>
        /// <value>
        /// The Users Children
        /// </value>
        public ICollection<Child> Children { get; set; } = new List<Child>();

        /// <summary>
        /// Gets or sets a list of parents. Only available to caretakers
        /// </summary>
        /// <value>
        /// The Users parents
        /// </value>

        public ICollection<ApplicationUser> Parents { get; set; } = new List<ApplicationUser>();

        /// <summary>
        /// Gets or sets a list of caretakers. Only available to parents
        /// </summary>
        /// <value>
        /// The Users caretakers
        /// </value>
        public ICollection<ApplicationUser> Caretakers { get; set; } = new List<ApplicationUser>();
    }
}
