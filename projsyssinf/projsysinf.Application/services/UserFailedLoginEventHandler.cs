using profsysinf.Core.Aggregates;
using projsysinf.Infrastructure;
using projsysinf.Application.Events;

namespace projsysinf.Application.Services
{
    public class UserFailedLoginEventHandler(ApplicationDbContext context) : IEventHandler<UserFailedLoginEvent>
    {
        public async Task HandleAsync(UserFailedLoginEvent eventItem)
        {
            var log = new Log
            {
                UserId = eventItem.UserId,
                OperationTypeId = 3,
                Tmstmp = eventItem.OccurredOn
            };

            context.Logs.Add(log);
            await context.SaveChangesAsync();
        }
    }
}