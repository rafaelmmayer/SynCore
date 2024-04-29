namespace SynCore.Api.Controllers.Classes.Requests;

public class AddClassRequest
{
    public string Name { get; set; }
    public int Absences { get; set; }
    public int Total { get; set; }
    public Time[] Times { get; set; }

    public class Time
    {
        public DayOfWeek DayOfWeek { get; set; }
        
        public string StartHour { get; set; }
        public string StartMinute { get; set; }
    
        public string EndHour { get; set; }
        public string EndMinute { get; set; }
    }
}