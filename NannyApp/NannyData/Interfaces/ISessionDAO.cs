using NannyModels.Models.SessionModels;

namespace NannyData.Interfaces
{
    public interface ISessionDAO
    {
        /// <summary>
        /// Gets a Session by the id
        /// </summary>
        /// <param name="sessionID">The session id</param>
        /// <returns>The session</returns>
        public Session GetSessionByID(int sessionID, int userID);

        /// <summary>
        /// Gets the active sessions for a user
        /// </summary>
        /// <param name="userID">The user id</param>
        /// <returns>The collection of active session</returns>
        public ICollection<Session> GetActiveSessionsByUserID(int userID);
    }
}
