using SynCore.Core.Entities.Common;

namespace SynCore.Core.Entities;

public class ClassTime : BaseEntity
{
    public DayOfWeek DayOfWeek { get; set; }
    
    public string StartHour { get; set; }
    public string StartMinute { get; set; }
    
    public string EndHour { get; set; }
    public string EndMinute { get; set; }

    public Guid ClassId { get; set; }
    public Class Class { get; set; }
}