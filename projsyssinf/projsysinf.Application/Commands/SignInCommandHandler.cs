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
        public DateTime? _lockoutEnd;

        public async Task<string> HandleAsync(SignInCommand command)
        {
            var user = await userRepository.GetByEmailAsync(command.Email);
            if (user == null)
            {
                throw new UserNotFoundException();
            }
            
            if (!user.IsActive && user._lockoutEnd.HasValue && user._lockoutEnd <= DateTime.UtcNow.AddHours(2))
            {
                user.ResetFailedLoginAttempts();
                await userRepository.SaveAsync(user);
            }
            
            try
            {
                var isPasswordValid = VerifyPassword(command.Password, user.Password);
                if (!isPasswordValid)
                {
                    user.RegisterFailedLoginAttempt();
                    await userRepository.SaveAsync(user);
                    throw new InvalidPasswordException();
                }
                if (!user.IsActive && user._lockoutEnd.HasValue && user._lockoutEnd <= DateTime.UtcNow.AddHours(2))
                {
                    user.ResetFailedLoginAttempts();
                    await userRepository.SaveAsync(user);
                }

                user.SignIn(command.Password);
                var signInEvent = new UserSignedInEvent(user.IdUser);
                await eventDispatcher.DispatchAsync(signInEvent);
            }
            catch (InvalidPasswordException)
            {
                await userRepository.SaveAsync(user);
                await eventDispatcher.DispatchAsync(new UserFailedLoginEvent(user.IdUser));
                if (!user.IsActive)
                {
                    await eventDispatcher.DispatchAsync(new UserLockedOutEvent(user.IdUser));
                }

                throw;
            }

            await userRepository.SaveAsync(user);
            return jwtTokenService.GenerateToken(user.IdUser.ToString(), user.Email, new[] { "User" });
        }

        private bool VerifyPassword(string inputPassword, string storedPassword)
        {
            return inputPassword == storedPassword;
        }
    }
}