using System.Security.Claims;

namespace NannyAPI.Miscellaneous.Errors
{
    public static class UserErrors
    {
        /// <summary>
        /// Check if the user is null
        /// </summary>
        /// <param name="claimsPrincipal">The user</param>
        /// <exception cref="Exception"></exception>
        public static void UserNullCheck(this ClaimsPrincipal claimsPrincipal)
        {
            string id = claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);

            if (claimsPrincipal is null || id is null || id.Equals(string.Empty))
            {
                throw new Exception("Not Logged in");
            }
        }

        /// <summary>
        /// Checks if the verified user matches an id
        /// </summary>
        /// <param name="claimsPrincipal">The verified user</param>
        /// <param name="idToCheck">The id</param>
        /// <exception cref="Exception"></exception>
        public static void CheckUserIdentity(this ClaimsPrincipal claimsPrincipal, int idToCheck)
        {
            claimsPrincipal.UserNullCheck();

            string id = claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);

            if (idToCheck < 0)
            {
                throw new Exception("Must send a valid user id");
            }

            if (Int32.TryParse(id, out int result))
            {
                if (result != idToCheck) { throw new Exception("User not authorized to perform this task"); }
            }
        }
    }
}
