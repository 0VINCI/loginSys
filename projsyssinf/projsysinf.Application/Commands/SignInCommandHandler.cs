using System.Threading.Tasks;
using profsysinf.Core.Events;
using profsysinf.Core.Exceptions;
using profsysinf.Core.Repositories;
using projsysinf.Application.Events;
using projsysinf.Application.Services;

namespace projsysinf.Application.Commands
{
    public class SignInCommandHandler : ICommandHandler<SignInCommand, string>
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtTokenService _jwtTokenService;
        private readonly IEventDispatcher _eventDispatcher;
        public DateTime? _lockoutEnd;

        public SignInCommandHandler(IUserRepository userRepository, IJwtTokenService jwtTokenService, IEventDispatcher eventDispatcher)
        {
            _userRepository = userRepository;
            _jwtTokenService = jwtTokenService;
            _eventDispatcher = eventDispatcher;
        }

        public async Task<string> HandleAsync(SignInCommand command)
        {
            var user = await _userRepository.GetByEmailAsync(command.Email);
            if (user == null)
            {
                throw new UserNotFoundException();
            }

            Console.WriteLine("xdd",_lockoutEnd);
            Console.WriteLine(DateTime.UtcNow.AddHours(2));

            if (!user.IsActive && user._lockoutEnd.HasValue && user._lockoutEnd <= DateTime.UtcNow.AddHours(2))
            {
                user.ResetFailedLoginAttempts();
                await _userRepository.SaveAsync(user);
            }
            
            try
            {
                var isPasswordValid = VerifyPassword(command.Password, user.Password);
                if (!isPasswordValid)
                {
                    user.RegisterFailedLoginAttempt();
                    await _userRepository.SaveAsync(user);
                    throw new InvalidPasswordException();
                }
                if (!user.IsActive && user._lockoutEnd.HasValue && user._lockoutEnd <= DateTime.UtcNow.AddHours(2))
                {
                    user.ResetFailedLoginAttempts();
                    await _userRepository.SaveAsync(user);
                }

                user.SignIn(command.Password);
                var signInEvent = new UserSignedInEvent(user.IdUser);
                await _eventDispatcher.DispatchAsync(signInEvent);
            }
            catch (InvalidPasswordException)
            {
                await _userRepository.SaveAsync(user);
                await _eventDispatcher.DispatchAsync(new UserFailedLoginEvent(user.IdUser));
                if (!user.IsActive)
                {
                    await _eventDispatcher.DispatchAsync(new UserLockedOutEvent(user.IdUser));
                }

                throw;
            }

            await _userRepository.SaveAsync(user);
            return _jwtTokenService.GenerateToken(user.IdUser.ToString(), user.Email, new[] { "User" });
        }

        private bool VerifyPassword(string inputPassword, string storedPassword)
        {
            return inputPassword == storedPassword;
        }
    }
}