using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SynCore.Api.Common.Exceptions;
using SynCore.Api.Data;

namespace SynCore.Api.Features.Posts;

public static class DeletePost
{
    public class Command : IRequest
    {
        public Guid Id { get; set; }
    }

    public class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleFor(c => c.Id)
                .NotEmpty().WithMessage("Id não pode ser vazio");
        }
    }

    public class Handler : IRequestHandler<Command>
    {
        private readonly IValidator<Command> _validator;
        private readonly AppDbContext _appDbContext;

        public Handler(IValidator<Command> validator, AppDbContext appDbContext)
        {
            _validator = validator;
            _appDbContext = appDbContext;
        }

        public async Task Handle(Command request, CancellationToken cancellationToken)
        {
            await _validator.ValidateAndThrowAsync(request, cancellationToken);

            var postObj = await _appDbContext.Posts
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

            if (postObj is null)
            {
                throw new AppException(StatusCodes.Status404NotFound, "Post não encontrado");
            }

            _appDbContext.Posts.Remove(postObj);
            await _appDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}