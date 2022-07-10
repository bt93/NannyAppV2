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

        public Child AddChild(ChildInput child, ClaimsPrincipal claimsPrincipal)
        {
            claimsPrincipal.UserNullCheck();
            string id = claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);

            int childID = _childDAO.AddNewChild(child, int.Parse(id));

            if (childID > 0)
            {
                return MapChild(child, childID, int.Parse(id));
            }

            throw new Exception("Something went wrong");
        }

        private Child MapChild(ChildInput child, int childID, int userID)
        {
            return new Child()
            {
                ChildID = childID,
                FirstName = child.FirstName,
                LastName = child.LastName,
                GenderID = child.GenderID,
                DateOfBirth = child.DateOfBirth,
                RatePerHour = child.RatePerHour,
                NeedsDiapers = child.NeedsDiapers,
                IsActive = true
            };
        }
    }
}
