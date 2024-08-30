using profsysinf.Core.Aggregates;
using profsysinf.Core.Entities;
using profsysinf.Core.Events;
using profsysinf.Core.Exceptions;
using profsysinf.Core.Repositories;
using projsysinf.Application.Services;

namespace projsysinf.Application.Commands
{
    public class ChangePasswordCommandHandler(IUserRepository userRepository, IEventDispatcher eventDispatcher)
        : ICommandHandler<ChangePasswordCommand, string>
    {
        public async Task<string> HandleAsync(ChangePasswordCommand command)
        {
            var user = await userRepository.GetByEmailWithHistoryAsync(command.Email);
            if (user == null)
            {
                throw new UserNotFoundException();
            }

            var isPasswordCorrect = CheckCorrectOldPassword(user, command.OldPassword);
            if (!isPasswordCorrect)
            {
                throw new InvalidPasswordException();
            }
            
            var isNewPasswordValid = CheckCorrectNewPassword(user, command.NewPassword);
            if (!isNewPasswordValid)
            {
                throw new PasswordHasBeenUsedException();
            }

            user.Password = command.NewPassword;

            user.PasswordHistories.Add(new PasswordHistory
            {
                UserId = user.IdUser,
                Password = command.NewPassword,
                Tmstmp = DateTime.UtcNow
            });

            await userRepository.SaveAsync(user);

            var changePasswordEvent = new ChangePasswordEvent(user.IdUser);
            await eventDispatcher.DispatchAsync(changePasswordEvent);

            return "Password changed successfully.";
        }

        private bool CheckCorrectNewPassword(User user, string newPassword)
        {
            foreach (var passwordHistory in user.PasswordHistories)
            {
                if (passwordHistory.Password == newPassword)
                {
                    return false;
                }
            }
            return true;
        }
        
        private bool CheckCorrectOldPassword(User user, string oldPassword)
        {
            return user.Password == oldPassword;
        }
    }
}