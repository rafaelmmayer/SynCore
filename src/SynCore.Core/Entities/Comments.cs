using SynCore.Core.Entities.Common;

namespace SynCore.Core.Entities;

public class Comment : BaseEntity
{
    public string Text { get; set; }

    public Guid PostId { get; set; }
    public Post Post { get; set; }
}