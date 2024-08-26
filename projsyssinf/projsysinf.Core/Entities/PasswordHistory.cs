using profsysinf.Core.Aggregates;

namespace profsysinf.Core.Entities;

public class PasswordHistory
{
    public int IdHistory { get; set; }
    public int UserId { get; set; }
    public string Password { get; set; }
    public DateTime Tmstmp { get; set; }

    public User User { get; set; }

}