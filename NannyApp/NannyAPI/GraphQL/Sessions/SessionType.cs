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
            descriptor.Field(s => s.Child)
                .ResolveWith<Resolvers>(s => s.GetChildByID(default!, default!, default!))
                .Description("Gets the child by the child id");
        }

        public class Resolvers
        {
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
