using profsysinf.Core.Events;
using profsysinf.Core.Exceptions;
using profsysinf.Core.Repositories;
using projsysinf.Application.Events;
using projsysinf.Application.Services;

namespace projsysinf.Application.Commands
{
    public class SignInCommandHandler(IUserRepository userRepository, IJwtTokenService jwtTokenService,
            IEventDispatcher eventDispatcher)
        : ICommandHandler<SignInCommand, string>
    {
        public async Task<string> HandleAsync(SignInCommand command)
        {
            var user = await userRepository.GetByEmailAsync(command.Email);
            if (user == null)
            {
                throw new UserNotFoundException();
            }

            var lastFailedLogin = await userRepository.GetLastFailedLoginAsync(user.IdUser);

            if (user.FailedLoginAttempts >= 2 && lastFailedLogin.HasValue && lastFailedLogin.Value.AddMinutes(3) > DateTime.UtcNow)
            {
                var lockedOutEvent = new UserLockedOutEvent(user.IdUser);
                await eventDispatcher.DispatchAsync(lockedOutEvent);
                throw new UserIsLockedException();
            }

            try
            {
                user.SignIn(command.Password);
                await userRepository.SaveAsync(user);

                var signInEvent = new UserSignedInEvent(user.IdUser);
                await eventDispatcher.DispatchAsync(signInEvent);
            }
            catch (InvalidPasswordException)
            {
                await userRepository.SaveAsync(user);
                await eventDispatcher.DispatchAsync(new UserFailedLoginEvent(user.IdUser));
                throw;
            }

            return jwtTokenService.GenerateToken(user.IdUser.ToString(), user.Email, new[] { "User" });
        }
    }
}