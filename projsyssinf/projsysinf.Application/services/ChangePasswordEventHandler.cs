using profsysinf.Core.Aggregates;
using projsysinf.Infrastructure;
using profsysinf.Core.Events;
using projsysinf.Application.Events;

namespace projsysinf.Application.Services
{
    public class ChangePasswordEventHandler(ApplicationDbContext context) : IEventHandler<ChangePasswordEvent>
    {
        public async Task HandleAsync(ChangePasswordEvent eventItem)
        {
            var log = new Log
            {
                UserId = eventItem.UserId,
                OperationTypeId = 4,
                Tmstmp = eventItem.OccurredOn
            };

            context.Logs.Add(log);
            await context.SaveChangesAsync();
        }
    }
}