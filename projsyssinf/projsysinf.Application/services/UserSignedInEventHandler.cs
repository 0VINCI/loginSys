using System.Threading.Tasks;
using projsysinf.Infrastructure;
using profsysinf.Core.Aggregates;
using projsysinf.Application.Events;
using profsysinf.Core.Events;

namespace projsysinf.Application.Services
{
    public class UserSignedInEventHandler : IEventHandler<UserSignedInEvent>
    {
        private readonly ApplicationDbContext _context;

        public UserSignedInEventHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task HandleAsync(UserSignedInEvent eventItem)
        {
            var log = new Log
            {
                UserId = eventItem.UserId,
                OperationTypeId = 2,
                Tmstmp = eventItem.OccurredOn
            };

            _context.Logs.Add(log);
            await _context.SaveChangesAsync();
        }
    }
}