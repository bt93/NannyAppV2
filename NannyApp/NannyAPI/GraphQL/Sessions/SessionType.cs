using HotChocolate.AspNetCore.Authorization;
using NannyAPI.Miscellaneous.Errors;
using NannyData.Interfaces;
using NannyModels.Models.ChildModels;
using NannyModels.Models.SessionModels;
using System.Security.Claims;

namespace NannyAPI.GraphQL.Sessions
{
    public class SessionType : ObjectType<Session>
    {
        protected override void Configure(IObjectTypeDescriptor<Session> descriptor)
        {
            descriptor.Description("The sessions on file");
            descriptor.Field(s => s.Child)
                .ResolveWith<Resolvers>(s => s.GetChildByID(default!, default!, default!))
                .Description("Gets the child by the child id");
        }

        public class Resolvers
        {
            /// <summary>
            /// Gets the Child by session
            /// </summary>
            /// <param name="session">The session</param>
            /// <param name="childDAO">The child dao</param>
            /// <param name="claimsPrincipal">The verified user</param>
            /// <returns></returns>
            [Authorize]
            public Child GetChildByID([Parent] Session session, [Service] IChildDAO childDAO, ClaimsPrincipal claimsPrincipal)
            {
                claimsPrincipal.UserNullCheck();
                string id = claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);

                return childDAO.GetChildByID(session.ChildID, int.Parse(id));
            }
        }
    }
}
