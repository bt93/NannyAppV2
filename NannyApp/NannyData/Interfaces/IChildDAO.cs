using NannyModels.Models.ChildModels;

namespace NannyData.Interfaces
{
    public interface IChildDAO
    {
        /// <summary>
        /// Gets the children associated with a user
        /// </summary>
        /// <param name="userID">The users id</param>
        /// <returns>A collection of children</returns>
        public ICollection<Child> GetChildrenByUserID(int userID);

        /// <summary>
        /// Gets a single child by thier ID, also requires UserID
        /// </summary>
        /// <param name="childID">The childs id</param>
        /// <param name="userID">The users id</param>
        /// <returns>A child</returns>
        public Child GetChildByID(int childID, int userID);
    }
}
