namespace projsysinf.Application.Commands
{
    public class PasswordReminderCommand(string email)
    {
        public string Email { get; } = email;
    }
}