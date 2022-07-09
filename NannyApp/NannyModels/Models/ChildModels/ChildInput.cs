using NannyModels.Enumerations;
using System.ComponentModel.DataAnnotations;

namespace NannyModels.Models.ChildModels
{
    public class ChildInput
    {
        /// <summary>
        /// Gets or sets the childs first name
        /// </summary>
        /// <value>
        /// The childs first name
        /// </value>
        [Required(ErrorMessage = "{0} is required")]
        [MaxLength(80)]
        public string? FirstName { get; set; }

        /// <summary>
        /// Gets or sets the childs last name
        /// </summary>
        /// <value>
        /// The childs last name
        /// </value>
        [Required(ErrorMessage = "{0} is required")]
        [MaxLength(80)]
        public string? LastName { get; set; }

        /// <summary>
        /// Gets or sets the childs GenderID
        /// </summary>
        /// <value>
        /// The childs GenderID
        /// </value>
        [Required(ErrorMessage = "{0} is required")]
        public Gender GenderID { get; set; }

        /// <summary>
        /// Gets or sets the childs Date Of Birth
        /// </summary>
        /// <value>
        /// The childs Date Of Birth
        /// </value>
        public DateTimeOffset DateOfBirth { get; set; }

        /// <summary>
        /// Gets or sets the childs Rate Per Hour
        /// </summary>
        /// <value>
        /// The childs Rate Per Hour
        /// </value>
        [Required(ErrorMessage = "{0} is required")]        
        public decimal RatePerHour { get; set; }

        /// <summary>
        /// Gets or sets if the child needs diapers
        /// </summary>
        /// <value>
        /// If the child needs diapers
        /// </value>
        [Required(ErrorMessage = "{0} is required")]
        public bool NeedsDiapers { get; set; }
    }
}
