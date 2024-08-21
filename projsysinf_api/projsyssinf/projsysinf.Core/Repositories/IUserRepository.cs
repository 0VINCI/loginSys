using profsysinf.Core.Aggregates;

namespace profsysinf.Core.Repositories;

public interface IUserRepository
{
    Task<User> GetByEmailAsync(string email);
    Task SaveAsync(User user);
}