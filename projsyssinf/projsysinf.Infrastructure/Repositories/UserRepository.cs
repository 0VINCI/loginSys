using Microsoft.EntityFrameworkCore;
using profsysinf.Core.Aggregates;
using profsysinf.Core.Repositories;

namespace projsysinf.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            return await _context.Users.SingleOrDefaultAsync(u => u.Email == email);
        }

        public async Task SaveAsync(User user)
        {
            if (user.IdUser == 0)
            {
                _context.Users.Add(user);
            }
            else
            {
                _context.Users.Update(user);
            }
    
            await _context.SaveChangesAsync();
        }
    }
}