using NannyModels.Enumerations;

namespace NannyModels.Models
{
    public class Child
    {
        public int ChildID { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public Gender GenderID { get; set; }

        public string? GenderName { get { return "Female"; } }

        public DateTimeOffset DateOfBirth { get; set; }

        public decimal RatePerHour { get; set; }

        public bool NeedsDiapers { get; set; }

        public bool IsActive { get; set; }

        public string? ImageURL { get; set; }
    }
}
