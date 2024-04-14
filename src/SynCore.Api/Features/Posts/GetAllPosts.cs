using MediatR;
using Microsoft.EntityFrameworkCore;
using SynCore.Api.Data;
using SynCore.Core.Entities;

namespace SynCore.Api.Features.Posts;

public static class GetAllPosts
{
    public class Command : IRequest<Post[]>
    {
        
    }
    
    public class Handler : IRequestHandler<Command, Post[]>
    {
        private readonly AppDbContext _appDbContext;

        public Handler(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Post[]> Handle(Command request, CancellationToken cancellationToken)
        {
            var posts = await _appDbContext.Posts
                .AsNoTracking()
                .Include(p => p.User)
                .ToListAsync(cancellationToken);

            return posts.ToArray();
        }
    }
}