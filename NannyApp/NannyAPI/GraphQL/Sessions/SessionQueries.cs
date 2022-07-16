using HotChocolate.AspNetCore.Authorization;
using NannyAPI.Miscellaneous.Errors;
using NannyData.Interfaces;
using NannyModels.Models.SessionModels;
using System.Security.Claims;

namespace NannyAPI.GraphQL.Sessions
{
    [ExtendObjectType(extendsType: typeof(Query))]
    public class SessionQueries
    {
        private readonly ISessionDAO _sessionDAO;

        public SessionQueries(ISessionDAO sessionDAO)
        {
            _sessionDAO = sessionDAO;
        }

        /// <summary>
        /// Gets a session by the id
        /// </summary>
        /// <param name="sessionID">The session id</param>
        /// <param name="claimsPrincipal">The verified user</param>
        /// <returns>The session</returns>
        [Authorize]
        [GraphQLDescription("Gets a session by the id. Only returns session if connected to current user.")]
        public Session GetSessionByID(int sessionID, ClaimsPrincipal claimsPrincipal)
        {
            claimsPrincipal.UserNullCheck();
            string id = claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);

            return _sessionDAO.GetSessionByID(sessionID, int.Parse(id));
        }

        /// <summary>
        /// Gets the logged in users active sessions
        /// </summary>
        /// <param name="claimsPrincipal">The verified user</param>
        /// <returns>A collection of session</returns>
        [Authorize]
        [GraphQLDescription("Gets current users active sessions")]
        public ICollection<Session> GetMyActiveSessions(ClaimsPrincipal claimsPrincipal)
        {
            claimsPrincipal.UserNullCheck();
            string id = claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);

            return _sessionDAO.GetActiveSessionsByUserID(int.Parse(id));
        }
    }
}
