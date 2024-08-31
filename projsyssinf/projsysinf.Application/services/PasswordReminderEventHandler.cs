using profsysinf.Core.Aggregates;
using projsysinf.Infrastructure;
using profsysinf.Core.Events;
using projsysinf.Application.Events;

namespace projsysinf.Application.Services
{
    public class PasswordReminderEventHandler(ApplicationDbContext context) : IEventHandler<PasswordReminderEvent>
    {
        public async Task HandleAsync(PasswordReminderEvent eventItem)
        {
            var log = new Log
            {
                UserId = eventItem.UserId,
                OperationTypeId = 5,
                Tmstmp = eventItem.OccurredOn
            };

            context.Logs.Add(log);
            await context.SaveChangesAsync();
        }
    }
}