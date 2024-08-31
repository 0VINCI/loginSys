using profsysinf.Core.Entities;
using profsysinf.Core.Events;
using profsysinf.Core.Exceptions;
using projsysinf.Core.Aggregates;

namespace profsysinf.Core.Aggregates
{
    public class User : AggregateRoot
    {
        private const int MaxFailedLoginAttempts = 3;

        public int IdUser { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int FailedLoginAttempts { get; set; } = 0;
        public ICollection<Log> Logs { get; set; } = new List<Log>();
        public ICollection<PasswordHistory> PasswordHistories { get; set; } = new List<PasswordHistory>();

        public void SignIn(string password)
        {
            if (!VerifyPassword(password))
            {
                RegisterFailedLoginAttempt();
                throw new InvalidPasswordException();
            }

            ResetFailedLoginAttempts();
            AddDomainEvent(new UserSignedInEvent(IdUser));
        }

        public void RegisterFailedLoginAttempt()
        {
            FailedLoginAttempts++;

            if (FailedLoginAttempts >= MaxFailedLoginAttempts)
            {
                AddDomainEvent(new UserLockedOutEvent(IdUser));
            }
        }

        public void ResetFailedLoginAttempts()
        {
            FailedLoginAttempts = 0;
        }

        public void Register(string password)
        {
            Password = password;

            PasswordHistories.Add(new PasswordHistory
            {
                Password = Password,
                Tmstmp = DateTime.UtcNow
            });

            AddDomainEvent(new UserRegisterEvent(IdUser));
        }
        
        private bool VerifyPassword(string password)
        {
            return Password == password;
        }
    }
}