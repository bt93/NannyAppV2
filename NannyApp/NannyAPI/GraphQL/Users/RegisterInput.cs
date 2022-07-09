using NannyModels.Models;
using NannyModels.Models.User;

namespace NannyAPI.GraphQL.Users
{
    public record RegisterInput(ApplicationUserInput user);
}
