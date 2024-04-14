using FluentValidation;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SynCore.Api.Common.Exceptions;
using SynCore.Api.Data;
using SynCore.Core.Entities;

namespace SynCore.Api.Features.Posts
{
    public class AddPost
    {
        public class Command : IRequest<Post>
        {
            public string Title { get; set; }
            public string Content { get; set; }
            public Guid UserId { get; set; }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator()
            {
                RuleFor(c => c.Title)
                    .NotEmpty().WithMessage("Nome não pode ser vazio");

                RuleFor(c => c.Content)
                    .NotEmpty().WithMessage("Conteúdo não pode ser vazio");
            }
        }

        public class Handler : IRequestHandler<Command, Post>
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

            public async Task<Post> Handle(Command request, CancellationToken cancellationToken)
            {
                await _validator.ValidateAndThrowAsync(request, cancellationToken);

                var existPost = await _appDbContext.Posts
                    .AsNoTracking()
                    .AnyAsync(c => c.Title == request.Title, cancellationToken);

                if (existPost)
                {
                    throw new AppException(StatusCodes.Status409Conflict, "Post já cadastrado");
                }

                var newPost = request.Adapt<Post>();
                newPost.Id = Guid.NewGuid();

                await _appDbContext.Posts.AddAsync(newPost, cancellationToken);
                await _appDbContext.SaveChangesAsync(cancellationToken);

                return newPost;
            }
        }

    }
}
