using System.Threading.Tasks;
using profsysinf.Core.Aggregates;
using projsysinf.Infrastructure;
using profsysinf.Core.Entities;
using profsysinf.Core.Events;
using projsysinf.Application.Events;

namespace projsysinf.Application.Services
{
    public class UserFailedLoginEventHandler : IEventHandler<UserFailedLoginEvent>
    {
        private readonly ApplicationDbContext _context;

        public UserFailedLoginEventHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task HandleAsync(UserFailedLoginEvent eventItem)
        {
            var log = new Log
            {
                UserId = eventItem.UserId,
                OperationTypeId = 3,
                Tmstmp = eventItem.OccurredOn
            };

            _context.Logs.Add(log);
            await _context.SaveChangesAsync();
        }
    }
}