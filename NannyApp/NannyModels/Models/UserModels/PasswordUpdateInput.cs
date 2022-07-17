using System.ComponentModel.DataAnnotations;

namespace NannyModels.Models.UserModels
{
    public class PasswordUpdateInput
    {
        /// <summary>
        /// Gets or sets the current user password
        /// </summary>
        /// <value>
        /// The current user password
        /// </value>
        [Required(ErrorMessage = "{0} is required")]
        [StringLength(250, MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string? CurrentPassword { get; set; }

        /// <summary>
        /// Gets or sets the new user password
        /// </summary>
        /// <value>
        /// The new user password
        /// </value>
        [Required(ErrorMessage = "{0} is required")]
        [StringLength(250, MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string? NewPassword { get; set; }

        /// <summary>
        /// Gets or sets the validation for the new password
        /// </summary>
        /// <value>
        /// The validation for the new password
        /// </value>
        [Required(ErrorMessage = "{0} is required")]
        [StringLength(250, MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Compare(nameof(NewPassword), ErrorMessage = "{0} Does not match with {1].")]
        public string? VerifyNewPassword { get; set; }
    }
}
