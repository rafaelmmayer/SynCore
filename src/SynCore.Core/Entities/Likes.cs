using SynCore.Core.Entities.Common;

namespace SynCore.Core.Entities;

public class Like : BaseEntity
{
    public Guid PostId { get; set; }
    public Post Post { get; set; }
    
    public Guid UserId { get; set; }
    public User User { get; set; }
}