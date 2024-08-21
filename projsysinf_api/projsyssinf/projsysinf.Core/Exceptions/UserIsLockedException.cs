namespace profsysinf.Core.Exceptions;

public sealed class UserIsLockedException : CustomException
{
    public UserIsLockedException() : base("User is locked.")
    {
    }
    
}