using Microsoft.EntityFrameworkCore;
using profsysinf.Core.Aggregates;
using profsysinf.Core.Repositories;
namespace projsysinf.Infrastructure.Repositories
{
    public class UserRepository(ApplicationDbContext context) : IUserRepository
    {
        public async Task<User> GetByEmailAsync(string email)
        {
            return await context.Users.SingleOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User> GetByEmailWithHistoryAsync(string email)
        {
            return await context.Users
                .Include(u => u.PasswordHistories)
                .SingleOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User> GetByEmailWithLogsAsync(string email)
        {
            return await context.Users
                .Include(u => u.Logs)
                .SingleOrDefaultAsync(u => u.Email == email);
        }

        public async Task SaveAsync(User user)
        {
            if (user.IdUser == 0)
            {
                context.Users.Add(user);
            }
            else
            {
                context.Users.Update(user);
            }
    
            await context.SaveChangesAsync();
        }
        public async Task<DateTime?> GetLastFailedLoginAsync(int userId)
        {
            var lastFailedLog = await context.Logs
                .Where(log => log.UserId == userId && log.OperationTypeId == 3)
                .OrderByDescending(log => log.Tmstmp)
                .FirstOrDefaultAsync();

            return lastFailedLog?.Tmstmp;
        }
    }
}