using MediatR;
using Microsoft.EntityFrameworkCore;
using SynCore.Api.Data;
using SynCore.Core.Entities;

namespace SynCore.Api.Features.Classes;

public static class GetAllClasses
{
    public class Command : IRequest<Class[]>
    {
        
    }
    
    public class Handler : IRequestHandler<Command, Class[]>
    {
        private readonly AppDbContext _appDbContext;

        public Handler(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Class[]> Handle(Command request, CancellationToken cancellationToken)
        {
            var classes = await _appDbContext.Classes
                .AsNoTracking()
                .Include(d => d.Times)
                .ToListAsync(cancellationToken);

            return classes.ToArray();
        }
    }
}