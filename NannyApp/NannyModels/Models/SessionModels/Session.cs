using NannyModels.Models.ChildModels;

namespace NannyModels.Models.SessionModels
{
    public class Session
    {
        public int SessionID { get; set; }
        public int ChildID { get; set; }
        public Child Child { get; set; } = new Child();
        public DateTimeOffset DropOff { get; set; }
        public DateTimeOffset PickUp { get; set; }
        public string? Notes { get; set; }
    }
}
