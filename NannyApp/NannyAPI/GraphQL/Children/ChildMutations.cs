using NannyAPI.Miscellaneous.Errors;
using NannyData.Interfaces;
using NannyModels.Models.ChildModels;
using System.Security.Claims;

namespace NannyAPI.GraphQL.Children
{
    [ExtendObjectType(extendsType: typeof(Mutation))]
    public class ChildMutations
    {
        private readonly IChildDAO _childDAO;

        public ChildMutations(IChildDAO childDAO)
        {
            _childDAO = childDAO;
        }

        public bool AddChild(ChildInput child, ClaimsPrincipal claimsPrincipal)
        {
            claimsPrincipal.UserNullCheck();
            string id = claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);

            return _childDAO.AddChild(child, int.Parse(id));
        }
    }
}
