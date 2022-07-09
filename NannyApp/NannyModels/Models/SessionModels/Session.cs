using NannyModels.Models.ChildModels;

namespace NannyModels.Models.SessionModels
{
    public class Session
    {
        /// <summary>
        /// Gets or sets the SessionID
        /// </summary>
        /// <value>
        /// The SessionID
        /// </value>
        public int SessionID { get; set; }

        /// <summary>
        /// Gets or sets the ChildID
        /// </summary>
        /// <value>
        /// The ChildID
        /// </value>
        public int ChildID { get; set; }

        /// <summary>
        /// Gets or sets the Child
        /// </summary>
        /// <value>
        /// The Child
        /// </value>
        public Child Child { get; set; } = new Child();

        /// <summary>
        /// Gets or sets the drop off time
        /// </summary>
        /// <value>
        /// The drop off time
        /// </value>
        public DateTimeOffset DropOff { get; set; }

        /// <summary>
        /// Gets or sets the pick up time
        /// </summary>
        /// <value>
        /// The pick up time
        /// </value>
        public DateTimeOffset PickUp { get; set; }

        /// <summary>
        /// Gets or sets the Notes
        /// </summary>
        /// <value>
        /// The Notes
        /// </value>
        public string? Notes { get; set; }
    }
}
