using System.ComponentModel.DataAnnotations;

namespace NannyModels.Models.AddressModels
{
    public class AddressInput
    {
        /// <summary>
        /// Gets or sets the Address line 1
        /// </summary>
        /// <value>
        /// The Address line 1
        /// </value>
        [Required(ErrorMessage = "{0} is required")]
        [MaxLength(200)]
        public string? Address1 { get; set; }

        /// <summary>
        /// Gets or sets the Address line 2
        /// </summary>
        /// <value>
        /// The Address line 2
        /// </value>
        [MaxLength(200)]
        public string? Address2 { get; set; }

        /// <summary>
        /// Gets or sets the Address line 3
        /// </summary>
        /// <value>
        /// The Address line 3
        /// </value>
        [MaxLength(200)]
        public string? Address3 { get; set; }

        /// <summary>
        /// Gets or sets the Address line 4
        /// </summary>
        /// <value>
        /// The Address line 4
        /// </value>
        [MaxLength(200)]
        public string? Address4 { get; set; }

        /// <summary>
        /// Gets or sets the Locality (City, Town, etc)
        /// </summary>
        /// <value>
        /// The Locality
        /// </value>
        [Required(ErrorMessage = "{0} is required")]
        [MaxLength(200)]
        public string? Locality { get; set; }

        /// <summary>
        /// Gets or sets the Region (State, Provence, etc)
        /// </summary>
        /// <value>
        /// The Region
        /// </value>
        [Required(ErrorMessage = "{0} is required")]
        [MaxLength(200)]
        public string? Region { get; set; }

        /// <summary>
        /// Gets or sets the PostalCode (ZipCode)
        /// </summary>
        /// <value>
        /// The Region
        /// </value>
        [Required(ErrorMessage = "{0} is required")]
        [MaxLength(10)]
        public string? PostalCode { get; set; }

        /// <summary>
        /// Gets or sets the County 
        /// </summary>
        /// <value>
        /// The County
        /// </value>
        [Required(ErrorMessage = "{0} is required")]
        [MaxLength(60)]
        public string? County { get; set; }

        /// <summary>
        /// Gets or sets the Country 
        /// </summary>
        /// <value>
        /// The Country
        /// </value>
        [Required(ErrorMessage = "{0} is required")]
        [MaxLength(60)]
        public string? Country { get; set; }
    }
}
