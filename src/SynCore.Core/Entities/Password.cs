using SynCore.Core.Entities.Common;

namespace SynCore.Core.Entities;

public class Password : BaseEntity
{
    public string Value { get; set; }

    public Guid UserId { get; set; }
    public User User { get; set; }
}