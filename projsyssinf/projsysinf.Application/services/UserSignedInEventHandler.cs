using projsysinf.Infrastructure;
using profsysinf.Core.Aggregates;
using projsysinf.Application.Events;
using profsysinf.Core.Events;

namespace projsysinf.Application.Services
{
    public class UserSignedInEventHandler(ApplicationDbContext context) : IEventHandler<UserSignedInEvent>
    {
        public async Task HandleAsync(UserSignedInEvent eventItem)
        {
            var log = new Log
            {
                UserId = eventItem.UserId,
                OperationTypeId = 2,
                Tmstmp = eventItem.OccurredOn
            };

            context.Logs.Add(log);
            await context.SaveChangesAsync();
        }
    }
}