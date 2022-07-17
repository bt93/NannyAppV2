using HotChocolate.AspNetCore.Authorization;
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

        /// <summary>
        /// Adds new child
        /// </summary>
        /// <param name="child">The child</param>
        /// <param name="claimsPrincipal">The verified user</param>
        /// <returns>The Child</returns>
        /// <exception cref="Exception"></exception>
        [Authorize]
        [GraphQLDescription("Adds a new child and connects to current user")]
        public Child AddChild(ChildInput child, ClaimsPrincipal claimsPrincipal)
        {
            claimsPrincipal.UserNullCheck();
            string id = claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);

            int childID = _childDAO.AddNewChild(child, int.Parse(id));

            if (childID > 0)
            {
                return MapChild(child, childID);
            }

            throw new Exception("Something went wrong");
        }

        /// <summary>
        /// Updates an existing child. Must be connected to child or a admin to update.
        /// </summary>
        /// <param name="child">The child</param>
        /// <param name="childID">The child id</param>
        /// <param name="claimsPrincipal">The verified user</param>
        /// <returns>The child</returns>
        /// <exception cref="Exception"></exception>
        [Authorize]
        [GraphQLDescription("Updates an existing child. Must be connected to child or a admin to update.")]
        public Child UpdateChild(ChildInput child, int childID, ClaimsPrincipal claimsPrincipal)
        {
            claimsPrincipal.UserNullCheck();
            string id = claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);

            bool updateSuccessful = _childDAO.UpdateChild(child, childID, int.Parse(id));

            if (updateSuccessful)
            {
                return MapChild(child, childID);
            }

            throw new Exception("Unable to update. Likely not connected to this child or child does not exist.");
        }

        private Child MapChild(ChildInput child, int childID)
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
