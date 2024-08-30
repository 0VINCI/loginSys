using profsysinf.Core.Aggregates;
using projsysinf.Infrastructure;
using profsysinf.Core.Events;
using projsysinf.Application.Events;

namespace projsysinf.Application.Services
{
    public class UserRegisterEventHandler(ApplicationDbContext context) : IEventHandler<UserRegisterEvent>
    {
        public async Task HandleAsync(UserRegisterEvent eventItem)
        {
            var log = new Log
            {
                UserId = eventItem.UserId,
                OperationTypeId = 1,
                Tmstmp = eventItem.OccurredOn
            };

            context.Logs.Add(log);
            await context.SaveChangesAsync();
        }
    }
}