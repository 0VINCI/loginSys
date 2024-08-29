using System.Threading.Tasks;
using profsysinf.Core.Aggregates;
using profsysinf.Core.Events;
using profsysinf.Core.Exceptions;
using profsysinf.Core.Repositories;
using projsysinf.Application.Events;
using projsysinf.Application.Services;

namespace projsysinf.Application.Commands
{
    public class RegisterCommandHandler : ICommandHandler<RegisterCommand, string>
    {
        private readonly IUserRepository _userRepository;
        private readonly IEventDispatcher _eventDispatcher;

        public RegisterCommandHandler(IUserRepository userRepository, IEventDispatcher eventDispatcher)
        {
            _userRepository = userRepository;
            _eventDispatcher = eventDispatcher;
        }

        public async Task<string> HandleAsync(RegisterCommand command)
        {
            var existingUser = await _userRepository.GetByEmailAsync(command.Email);
            if (existingUser != null)
            {
                throw new UserAlreadyExistsException();
            }

            var isPasswordValid = CheckCorrectPassword(command.Password, command.ConfirmPassword);
            if (!isPasswordValid)
            {
                throw new PasswordNotTheSameException();
            }

            var newUser = new User
            {
                Email = command.Email,
                Password = command.Password,
                IsActive = true
            };
            
            newUser.Register(command.Password);
            await _userRepository.SaveAsync(newUser);
            
            var registerEvent = new UserRegisterEvent(newUser.IdUser);
            await _eventDispatcher.DispatchAsync(registerEvent);
            
            return newUser.IdUser.ToString();
        }

        private bool CheckCorrectPassword(string inputPassword, string confirmPassword)
        {
            return inputPassword == confirmPassword;
        }
    }
}