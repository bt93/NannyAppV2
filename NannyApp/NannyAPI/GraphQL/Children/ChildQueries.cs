using HotChocolate.AspNetCore.Authorization;
using NannyData.Interfaces;
using NannyModels.Models.ChildModels;
using System.Security.Claims;

namespace NannyAPI.GraphQL.Children
{
    [ExtendObjectType(extendsType: typeof(Query))]
    public class ChildQueries
    {
        private readonly IChildDAO _childDAO;

        public ChildQueries(IChildDAO childDAO)
        {
            _childDAO = childDAO;
        }

        [Authorize]
        public ICollection<Child> GetMyChildren(ClaimsPrincipal claimsPrincipal)
        {
            string id = claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);
            return _childDAO.GetChildrenByUserID(Int32.Parse(id));
        }

        [Authorize]
        public Child GetChild(int childID, ClaimsPrincipal claimsPrincipal)
        {
            string id = claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);
            return _childDAO.GetChildByID(childID, Int32.Parse(id));
        }
    }
}
