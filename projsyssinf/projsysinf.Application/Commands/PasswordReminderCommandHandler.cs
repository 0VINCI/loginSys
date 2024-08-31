using profsysinf.Core.Entities;
using profsysinf.Core.Exceptions;
using profsysinf.Core.Repositories;
using projsysinf.Application.Events;
using projsysinf.Application.Services;

namespace projsysinf.Application.Commands;

public class PasswordReminderCommandHandler(IUserRepository userRepository, IEmailService emailService,
        IEventDispatcher eventDispatcher)
    : ICommandHandler<PasswordReminderCommand, string>
{
    public async Task<string> HandleAsync(PasswordReminderCommand command)
    {
        var user = await userRepository.GetByEmailAsync(command.Email);
        if (user == null)
        {
            throw new UserNotFoundException();
        }

        var resetCode = GenerateResetCode();

        user.Password = resetCode;
        
        await userRepository.SaveAsync(user);

        await emailService.SendEmailAsync(user.Email, "Password Reset Code", $"Your password reset code is: {resetCode}");

        var reminderPasswordEvent = new PasswordReminderEvent(user.IdUser);
        await eventDispatcher.DispatchAsync(reminderPasswordEvent);

        user.PasswordHistories.Add(new PasswordHistory
        {
            UserId = user.IdUser,
            Password = resetCode,
            Tmstmp = DateTime.UtcNow
        });

        await userRepository.SaveAsync(user);

        return "Reset code has been sent to your email.";
    }

    private string GenerateResetCode()
    {
        var random = new Random();
        return random.Next(100000, 999999).ToString();
    }
}