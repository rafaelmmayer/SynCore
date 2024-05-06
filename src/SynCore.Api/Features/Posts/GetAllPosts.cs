using MediatR;
using Microsoft.EntityFrameworkCore;
using SynCore.Api.Data;

namespace SynCore.Api.Features.Posts;

public static class GetAllPosts
{
    public class Command : IRequest<Response[]>
    {
        
    }

    public class Response
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        
        public int NLikes { get; set; }
        public int NComments { get; set; }
        
        public User User { get; set; }
    }
    
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
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
            var posts = await _appDbContext.Posts
                .AsNoTracking()
                .Include(p => p.User)
                .ToListAsync(cancellationToken);

            var res = posts
                .Select(p => new Response()
                {
                    Id = p.Id,
                    Content = p.Content,
                    NComments = 0,
                    NLikes = 0,
                    User = new User()
                    {
                        Id = p.UserId,
                        Name = $"{p.User.Name} { p.User.LastName}"
                    }
                })
                .ToArray();

            return res;
        }
    }
}