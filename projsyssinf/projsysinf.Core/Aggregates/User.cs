using System;
using profsysinf.Core.Entities;
using profsysinf.Core.Events;
using profsysinf.Core.Exceptions;
using projsysinf.Application.Events;
using projsysinf.Core.Aggregates;

namespace profsysinf.Core.Aggregates
{
    public class User : AggregateRoot
    {
        private const int MaxFailedLoginAttempts = 3;
        public DateTime? _lockoutEnd;

        public int IdUser { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; } = true;
        public int FailedLoginAttempts { get; set; } = 0; 
        public ICollection<Log> Logs { get; set; }
        public ICollection<PasswordHistory> PasswordHistories { get; set; }
        

        public void SignIn(string password)
        {
            if (_lockoutEnd.HasValue && _lockoutEnd > DateTime.UtcNow.AddHours(2))
            {
                throw new UserIsLockedException();
            }
            
            if (_lockoutEnd.HasValue && _lockoutEnd <= DateTime.UtcNow.AddHours(2))
            {
                ResetFailedLoginAttempts();
            }
            
            if (!VerifyPassword(password))
            {
                RegisterFailedLoginAttempt();
                AddDomainEvent(new UserFailedLoginEvent(IdUser));
                throw new InvalidPasswordException();
            }

            ResetFailedLoginAttempts();
            AddDomainEvent(new UserSignedInEvent(IdUser));
        }

        private bool VerifyPassword(string password)
        {
            return Password == password;
        }

        public void RegisterFailedLoginAttempt()
        {
            FailedLoginAttempts++;

            if (FailedLoginAttempts >= MaxFailedLoginAttempts)
            {
                Lock();
            }
        }

        private void Lock()
        {
            _lockoutEnd = DateTime.UtcNow.AddMinutes(3);
            IsActive = false;
            AddDomainEvent(new UserLockedOutEvent(IdUser));
        }

        public void ResetFailedLoginAttempts()
        {
            FailedLoginAttempts = 0;
            _lockoutEnd = null;
            IsActive = true;
        }
    }
}