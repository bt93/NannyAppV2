using HotChocolate.AspNetCore.Authorization;
using NannyAPI.Miscellaneous.Errors;
using NannyData.Interfaces;
using NannyModels.Enumerations;
using NannyModels.Models.ChildModels;
using NannyModels.Models.UserModels;
using System.Security.Claims;

namespace NannyAPI.GraphQL.Children
{
    public class ChildType : ObjectType<Child>
    {
        protected override void Configure(IObjectTypeDescriptor<Child> descriptor)
        {
            descriptor.Description("The children on file");
            descriptor
             .Field("genderID")
             .Argument("genderID", a => a.Type<GenderType>())
             .Resolve(context => context.ArgumentValue<Gender>("genderID"));

            descriptor
                .Field(c => c.Parents)
                .ResolveWith<Resolvers>(c => c.GetParentsByChild(default!, default!, default!))
                .Description("Childs a childs parents/gaurdians");

            descriptor
                .Field(c => c.Caretakers)
                .ResolveWith<Resolvers>(c => c.GetCaretakersByChild(default!, default!, default!))
                .Description("Gets the caretakers by the child");
        }
    }

    public class Resolvers
    {
        /// <summary>
        /// Gets a childs parents/gaurdians
        /// </summary>
        /// <param name="child">The child</param>
        /// <param name="userDAO">The user dao</param>
        /// <returns></returns>
        [Authorize]
        public ICollection<ApplicationUser> GetParentsByChild([Parent] Child child, [Service] IUserDAO userDAO, ClaimsPrincipal claimsPrincipal)
        {
            claimsPrincipal.UserNullCheck();
            string id = claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);

            return userDAO.GetUsersByChildID(child.ChildID, int.Parse(id), Role.Parent);
        }

        /// <summary>
        /// Gets the child caretakers
        /// </summary>
        /// <param name="child">Child</param>
        /// <param name="userDAO">The user dao</param>
        /// <param name="claimsPrincipal">The verified user</param>
        /// <returns></returns>
        [Authorize]
        public ICollection<ApplicationUser> GetCaretakersByChild([Parent] Child child, [Service] IUserDAO userDAO, ClaimsPrincipal claimsPrincipal)
        {
            claimsPrincipal.UserNullCheck();
            string id = claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);

            return userDAO.GetUsersByChildID(child.ChildID, int.Parse(id), Role.Caretaker);
        }
    }

    public class GenderType : EnumType<Gender> { }
}
