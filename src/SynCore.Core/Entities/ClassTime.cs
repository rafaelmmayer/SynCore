using SynCore.Core.Entities.Common;

namespace SynCore.Core.Entities;

public class ClassTime : BaseEntity
{
    public DayOfWeek DayOfWeek { get; set; }
    public int Hour { get; set; }
    public int Minute { get; set; }

    public Guid ClassId { get; set; }
    public Class Class { get; set; }
}