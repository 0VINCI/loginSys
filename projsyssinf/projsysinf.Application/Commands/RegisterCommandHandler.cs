using profsysinf.Core.Aggregates;
using profsysinf.Core.Events;
using profsysinf.Core.Exceptions;
using profsysinf.Core.Repositories;
using projsysinf.Application.Services;

namespace projsysinf.Application.Commands
{
    public class RegisterCommandHandler(IUserRepository userRepository, IEventDispatcher eventDispatcher)
        : ICommandHandler<RegisterCommand, string>
    {
        public async Task<string> HandleAsync(RegisterCommand command)
        {
            var existingUser = await userRepository.GetByEmailAsync(command.Email);
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
            };
            
            newUser.Register(command.Password);
            await userRepository.SaveAsync(newUser);
            
            var registerEvent = new UserRegisterEvent(newUser.IdUser);
            await eventDispatcher.DispatchAsync(registerEvent);
            
            return newUser.IdUser.ToString();
        }

        private bool CheckCorrectPassword(string inputPassword, string confirmPassword)
        {
            return inputPassword == confirmPassword;
        }
    }
}