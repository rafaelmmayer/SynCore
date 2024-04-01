using SynCore.Core.Entities.Common;

namespace SynCore.Core.Entities;

public class Class : BaseEntity
{
    public string Name { get; set; } 
    public int Absences { get; set; }
    public int Total { get; set; }
    public bool IsActive { get; set; }
    
    public ICollection<ClassTime> Times { get; set; }
    
    public Guid UserId { get; set; }
    public User User { get; set; }
}