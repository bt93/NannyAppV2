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

        [Authorize]
        public Session GetSessionByID(int sessionID, ClaimsPrincipal claimsPrincipal)
        {
            claimsPrincipal.UserNullCheck();
            string id = claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);

            return _sessionDAO.GetSessionByID(sessionID, int.Parse(id));
        }
    }
}
