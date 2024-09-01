namespace projsysinf.Application.Commands
{
    public class LogoutCommand
    {
        public int UserId { get; }

        public LogoutCommand(int userId)
        {
            UserId = userId;
        }
    }
}