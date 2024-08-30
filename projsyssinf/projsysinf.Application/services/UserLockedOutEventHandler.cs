using profsysinf.Core.Aggregates;
using projsysinf.Infrastructure;
using profsysinf.Core.Events;
using projsysinf.Application.Events;

namespace projsysinf.Application.Services
{
    public class UserLockedOutEventHandler(ApplicationDbContext context) : IEventHandler<UserLockedOutEvent>
    {
        public async Task HandleAsync(UserLockedOutEvent eventItem)
        {
            var log = new Log
            {
                UserId = eventItem.UserId,
                OperationTypeId = 7,
                Tmstmp = eventItem.OccurredOn
            };

            context.Logs.Add(log);
            await context.SaveChangesAsync();
        }
    }
}