namespace projsysinf.Application.Commands
{
    public class ChangePasswordCommand(string email, string oldPassword, string newPassword)
    {
        public string Email { get; } = email;
        public string OldPassword { get; } = oldPassword;
        public string NewPassword { get; } = newPassword;
    }
}