using SynCore.Core.Entities.Common;

namespace SynCore.Core.Entities;

public class ChatMessage : BaseEntity
{
    public string Text { get; set; }
    
    public Guid ToId { get; set; }
    public User To { get; set; }
    
    public Guid FromId { get; set; }
    public User From { get; set; }
}