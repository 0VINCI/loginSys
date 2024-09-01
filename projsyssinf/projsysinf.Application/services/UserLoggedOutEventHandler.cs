using System.Threading.Tasks;
using profsysinf.Core.Aggregates;
using profsysinf.Core.Events;
using profsysinf.Core.Repositories;
using projsysinf.Infrastructure;

namespace projsysinf.Application.Events
{
    public class UserLoggedOutEventHandler : IEventHandler<UserLoggedOutEvent>
    {
        private readonly ApplicationDbContext _context;

        public UserLoggedOutEventHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task HandleAsync(UserLoggedOutEvent eventItem)
        {
            var log = new Log
            {
                UserId = eventItem.UserId,
                OperationTypeId = 6,
                Tmstmp = eventItem.OccurredOn
            };

            _context.Logs.Add(log);
            await _context.SaveChangesAsync();
        }
    }
}