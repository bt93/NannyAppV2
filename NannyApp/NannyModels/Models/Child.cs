﻿using NannyModels.Enumerations;
using NannyModels.Extensions;

namespace NannyModels.Models
{
    public class Child
    {
        /// <summary>
        /// Gets or sets the ChildID
        /// </summary>
        /// <value>
        /// The ChildID
        /// </value>
        public int ChildID { get; set; }

        /// <summary>
        /// Gets or sets the childs first name
        /// </summary>
        /// <value>
        /// The childs first name
        /// </value>
        public string? FirstName { get; set; }

        /// <summary>
        /// Gets or sets the childs last name
        /// </summary>
        /// <value>
        /// The childs last name
        /// </value>
        public string? LastName { get; set; }

        /// <summary>
        /// Gets or sets the childs GenderID
        /// </summary>
        /// <value>
        /// The childs GenderID
        /// </value>
        public Gender GenderID { get; set; }

        /// <summary>
        /// Gets or sets the childs GenderName
        /// </summary>
        /// <value>
        /// The childs GenderName
        /// </value>
        public string? GenderName { get { return GenderID.GetDisplayName(); } }

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
        public decimal RatePerHour { get; set; }

        /// <summary>
        /// Gets or sets if the child needs diapers
        /// </summary>
        /// <value>
        /// If the child needs diapers
        /// </value>
        public bool NeedsDiapers { get; set; }

        /// <summary>
        /// Gets or sets the childs is active
        /// </summary>
        /// <value>
        /// The child is active
        /// </value>
        public bool IsActive { get; set; }
    }
}
