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

    public static class RoleExtensions
    {
        public static int GetRoleID(this Role role)
        {
            return (int)role;
        }

        public static Role GetOppositeRole(this Role role)
        {
            switch (role)
            {
                case Role.Caretaker:
                    return Role.Parent;
                case Role.Parent:
                    return Role.Caretaker;
                default:
                    return 0;
            }
        }
    }
}
