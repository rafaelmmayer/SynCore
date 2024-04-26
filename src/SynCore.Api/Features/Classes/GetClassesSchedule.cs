using MediatR;
using Microsoft.EntityFrameworkCore;
using SynCore.Api.Data;

namespace SynCore.Api.Features.Classes;

public static class GetClassesSchedule
{
    public class Response
    {
        public DayOfWeek DayOfWeek { get; set; }
        public Class[] Classes { get; set; }
        
        public class Class
        {
            public string Name { get; set; } 
            public int Absences { get; set; }
            public int Total { get; set; }
            public string Freq 
                => ((Total == 0 ? 0 : Absences / (double)Total) * 100).ToString("F1");
            
            public DayOfWeek DayOfWeek { get; set; }
            
            public string StartHour { get; set; }
            public string StartMinute { get; set; }
            public string StartTime => $"{StartHour}h{StartMinute}";
    
            public string EndHour { get; set; }
            public string EndMinute { get; set; }
            public string EndTime => $"{EndHour}h{EndMinute}";
        }
    }

    public class Command : IRequest<Response[]>
    {
        
    }
    
    public class Handler : IRequestHandler<Command, Response[]>
    {
        private readonly AppDbContext _appDbContext;

        public Handler(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Response[]> Handle(Command request, CancellationToken cancellationToken)
        {
            var classes = await _appDbContext.Classes
                .AsNoTracking()
                .Include(d => d.Times)
                .ToListAsync(cancellationToken);

            var res = new List<Response.Class>();

            foreach (var c in classes)
            {
                foreach (var t in c.Times)
                {
                    res.Add(new Response.Class()
                    {
                        Name = c.Name,
                        Absences = c.Absences,
                        Total = c.Total,
                        DayOfWeek = t.DayOfWeek,
                        StartHour = t.StartHour,
                        StartMinute = t.StartMinute,
                        EndHour = t.EndHour,
                        EndMinute = t.EndMinute
                    });
                }
            }
            
            return res
                .OrderBy(r => r.DayOfWeek)
                .ThenBy(r => r.StartHour)
                .ThenBy(r => r.StartMinute)
                .GroupBy(r => r.DayOfWeek)
                .Select(r => new Response()
                {
                    DayOfWeek = r.Key,
                    Classes = r.ToArray()
                })
                .ToArray();
        }
    }
}