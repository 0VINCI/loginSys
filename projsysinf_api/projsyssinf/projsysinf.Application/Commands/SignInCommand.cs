namespace projsysinf.Application.Commands
{
    public class SignInCommand(string email, string password)
    {
        public string Email { get; } = email;
        public string Password { get; } = password;
    }
}