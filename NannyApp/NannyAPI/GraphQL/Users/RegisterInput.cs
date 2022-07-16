using NannyModels.Models;
using NannyModels.Models.UserModels;

namespace NannyAPI.GraphQL.Users
{
    /// <summary>
    /// The register input
    /// </summary>
    /// <param name="user">User info</param>
    public record RegisterInput(ApplicationUserInput user);
}
