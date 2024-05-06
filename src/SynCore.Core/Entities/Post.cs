using SynCore.Core.Entities.Common;

namespace SynCore.Core.Entities;

public class Post : BaseEntity
{
    public string Content { get; set; }
    
    public Guid UserId { get; set; }
    public User User { get; set; }
}