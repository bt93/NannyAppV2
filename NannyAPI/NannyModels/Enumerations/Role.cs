using System.ComponentModel.DataAnnotations;

namespace NannyModels.Enumerations
{
    public enum Role
    {
        [Display(Name = "Uninitialized")]
        Uninitialized = 0,
        [Display(Name = "Caretaker")]
        Caretaker = 1,
        [Display(Name = "Parent")]
        Parent = 2,
        [Display(Name = "Administrator")]
        Admin = 3,
    }
}
