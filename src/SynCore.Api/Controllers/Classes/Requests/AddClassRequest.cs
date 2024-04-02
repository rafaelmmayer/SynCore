namespace SynCore.Api.Controllers.Classes.Requests;

public class AddClassRequest
{
    public string Name { get; set; }
    public Time[] Times { get; set; }

    public class Time
    {
        public DayOfWeek DayOfWeek { get; set; }
        public int Hour { get; set; }
        public int Minute { get; set; }
    }
}