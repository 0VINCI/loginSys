namespace projsysinf.Application.Services
{
    public interface IJwtTokenService
    {
        string GenerateToken(string userId, string email, IEnumerable<string> roles);
    }
}