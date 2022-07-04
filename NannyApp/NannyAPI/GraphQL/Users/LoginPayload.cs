using NannyModels.Models;

namespace NannyAPI.GraphQL.Users
{
    /// <summary>
    /// The return info for a verified user
    /// </summary>
    /// <param name="ReturnUser">The user along with their token</param>
    public record LoginPayload(ReturnUser ReturnUser);
}
