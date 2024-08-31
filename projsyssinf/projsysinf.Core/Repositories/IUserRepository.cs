using System.Threading.Tasks;
using profsysinf.Core.Aggregates;

namespace profsysinf.Core.Repositories;

public interface IUserRepository
{
    Task<User> GetByEmailAsync(string email);
    Task SaveAsync(User user);
    Task<User> GetByEmailWithHistoryAsync(string email);
    Task<DateTime?> GetLastFailedLoginAsync(int userId);
}