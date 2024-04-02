using SynCore.Core.Entities.Common;

namespace SynCore.Core.Entities;

public class PasswordResetToken : BaseEntity
{
    public string Token { get; set; }
    public DateTime TokenExpiry { get; set; }
    public bool WasUsed { get; set; }

    public Guid UserId { get; set; }
    public User User { get; set; }
}