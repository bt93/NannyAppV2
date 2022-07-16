using NannyModels.Models.UserModels;

namespace NannyAPI.GraphQL.Users
{
    /// <summary>
    /// The registration payload
    /// </summary>
    /// <param name="returnUser">A returned user</param>
    public record RegisterPayload(ReturnUser returnUser);
}
