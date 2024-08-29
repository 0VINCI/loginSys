using System.Threading.Tasks;
using profsysinf.Core.Aggregates;
using projsysinf.Infrastructure;
using profsysinf.Core.Entities;
using profsysinf.Core.Events;
using projsysinf.Application.Events;

namespace projsysinf.Application.Services
{
    public class UserRegisterEventHandler : IEventHandler<UserRegisterEvent>
    {
        private readonly ApplicationDbContext _context;

        public UserRegisterEventHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task HandleAsync(UserRegisterEvent eventItem)
        {
            var log = new Log
            {
                UserId = eventItem.UserId,
                OperationTypeId = 1,
                Tmstmp = eventItem.OccurredOn
            };

            _context.Logs.Add(log);
            await _context.SaveChangesAsync();
        }
    }
}