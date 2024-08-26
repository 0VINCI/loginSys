using System;
using profsysinf.Core.Entities;
using profsysinf.Core.Events;
using profsysinf.Core.Exceptions;
using projsysinf.Core.Aggregates;

namespace profsysinf.Core.Aggregates;

public class User : AggregateRoot
{
    private const int MaxFailedLoginAttempts = 3;
    private int _failedLoginAttemts;
    private DateTime? _lockoutEnd;
    
    public int IdUser { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public bool IsActive { get; set; }
    public ICollection<Log> Logs { get; set; }
    public ICollection<PasswordHistory> PasswordHistories { get; set; }

    public void SignIn(string password)
    {
        if (!IsActive)
        {
            throw new UserIsLockedException();
        }

        ResetFailedLoginAttempts();
        AddDomainEvent(new UserSignedInEvent(IdUser));
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
    }

    private void ResetFailedLoginAttempts()
    {
        _failedLoginAttemts = 0;
        _lockoutEnd = null;
    }
}