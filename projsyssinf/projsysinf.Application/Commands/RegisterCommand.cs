namespace projsysinf.Application.Commands
{
    public class RegisterCommand(string email, string password, string confirmPassword)
    {
        public string Email { get; } = email;
        public string Password { get; } = password;
        public string ConfirmPassword { get; } = confirmPassword;
    }
}