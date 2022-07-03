namespace NannyAPI.GraphQL.Users
{
    public record LoginInput(string userNameOrEmail, string password);
}
