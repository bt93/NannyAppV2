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

        /// <summary>
        /// Gets the current users children
        /// </summary>
        /// <param name="claimsPrincipal">The verified user</param>
        /// <returns>A collection of children</returns>
        [Authorize]
        public ICollection<Child> GetMyChildren(ClaimsPrincipal claimsPrincipal)
        {
            string id = claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);
            return _childDAO.GetChildrenByUserID(Int32.Parse(id));
        }

        /// <summary>
        /// Gets a child by thier id. Only returns child if connected to user.
        /// </summary>
        /// <param name="childID">The child id</param>
        /// <param name="claimsPrincipal">The verified user</param>
        /// <returns>The child</returns>
        [Authorize]
        public Child GetChild(int childID, ClaimsPrincipal claimsPrincipal)
        {
            string id = claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);
            return _childDAO.GetChildByID(childID, Int32.Parse(id));
        }
    }
}
