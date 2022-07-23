namespace NannyModels.Models.Authentication
{
    /// <summary>
    /// Login input from the user
    /// </summary>
    /// <param name="userNameOrEmail">The username or email</param>
    /// <param name="password">The users password</param>
    public record LoginInput(string userNameOrEmail, string password);
}
