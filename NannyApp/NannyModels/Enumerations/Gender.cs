using System.ComponentModel.DataAnnotations;

namespace NannyModels.Enumerations
{
    public enum Gender
    {
        [Display(Name = "Male")]
        M = 1,
        [Display(Name = "Female")]
        F = 2,
        [Display(Name = "Nonbinary")]
        N = 3,
        [Display(Name = "Other")]
        O = 4,
    }
}
