using FluentValidation;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SynCore.Api.Data;
using SynCore.Core.Entities;

namespace SynCore.Api.Features.Posts
{
    public class AddPost
    {
        public class Command : IRequest<Response>
        {
            public string Content { get; set; }
            public Guid UserId { get; set; }
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

        public class Validator : AbstractValidator<Command>
        {
            public Validator()
            {
                RuleFor(c => c.Content)
                    .NotEmpty().WithMessage("Conteúdo não pode ser vazio");
            }
        }

        public class Handler : IRequestHandler<Command, Response>
        {
            private readonly IValidator<Command> _validator;
            private readonly AppDbContext _appDbContext;

            public Handler(
                IValidator<Command> validator,
                AppDbContext appDbContext)
            {
                _validator = validator;
                _appDbContext = appDbContext;
            }

            public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
            {
                await _validator.ValidateAndThrowAsync(request, cancellationToken);

                var newPost = request.Adapt<Post>();
                newPost.Id = Guid.NewGuid();

                await _appDbContext.Posts.AddAsync(newPost, cancellationToken);
                await _appDbContext.SaveChangesAsync(cancellationToken);

                var user = await _appDbContext.Users
                    .Select(u => new { u.Id, u.Name, u.LastName })
                    .FirstAsync(
                        u => u.Id == request.UserId, 
                        cancellationToken);

                var res = new Response()
                {
                    Id = newPost.Id,
                    Content = newPost.Content,
                    NComments = 0,
                    NLikes = 0,
                    User = new User()
                    {
                        Id = user.Id,
                        Name = $"{user.Name} {user.LastName}"
                    }
                };

                return res;
            }
        }

    }
}
