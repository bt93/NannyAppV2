using NannyModels.Models;
using NannyModels.Models.UserModels;

namespace NannyAPI.GraphQL.Users
{
    public record RegisterInput(ApplicationUserInput user);
}
