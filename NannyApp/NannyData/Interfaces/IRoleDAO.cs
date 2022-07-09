using NannyModels.Enumerations;

namespace NannyData.Interfaces
{
    public interface IRoleDAO
    {
        /// <summary>
        /// Gets the Roles by a user
        /// </summary>
        /// <param name="userID">The user id</param>
        /// <returns>The roles</returns>
        public List<Role> GetRolesByUserID(int userID);
    }
}
