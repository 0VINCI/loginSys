using System.Threading.Tasks;
using profsysinf.Core.Exceptions;
using profsysinf.Core.Repositories;
using projsysinf.Application.Services;

namespace projsysinf.Application.Commands
{
    public class SignInCommandHandler(IUserRepository userRepository, IJwtTokenService jwtTokenService)
        : ICommandHandler<SignInCommand, string>
    {
        public async Task<string> HandleAsync(SignInCommand command)
        {
            var user = await userRepository.GetByEmailAsync(command.Email);
            if (user == null)
            {
                throw new UserNotFoundException();
            }

            var isPasswordValid = VerifyPassword(command.Password, user.Password);
            if (!isPasswordValid)
            {
                throw new InvalidPasswordException();
            }

            return jwtTokenService.GenerateToken(user.IdUser.ToString(), user.Email, new[] { "User" });
        }

        private bool VerifyPassword(string inputPassword, string storedPassword)
        {
            return inputPassword == storedPassword;
        }
    }
}