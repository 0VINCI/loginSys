using profsysinf.Core.Events;
using profsysinf.Core.Exceptions;
using projsysinf.Core.Aggregates;

namespace profsysinf.Core.Aggregates;

public class User : AggregateRoot
{
    private const int MaxFailedLoginAttempts = 3;
    private int _failedLoginAttemts;
    private DateTime? _lockoutEnd;
    
    public Guid UserId { get; private set; }
    public string Email { get; private set; }
    public string Password { get; private set; }
    public bool IsActive => _lockoutEnd.HasValue && _lockoutEnd > DateTime.UtcNow;

    public void SignIn(string password)
    {
        if (!IsActive)
        {
            throw new UserIsLockedException();
        }

        ResetFailedLoginAttempts();
        AddDomainEvent(new UserSignedInEvent(UserId));
    }

    public void RegisterFailedLoginAttempt()
    {
        _failedLoginAttemts++;
        if (_failedLoginAttemts >= MaxFailedLoginAttempts)
        {
            Lock();
        }
    }

    private void Lock()
    {
        _lockoutEnd = DateTime.UtcNow.AddMinutes(3);
        //domainEvent
    }

    private void ResetFailedLoginAttempts()
    {
        _failedLoginAttemts = 0;
        _lockoutEnd = null;
    }
}