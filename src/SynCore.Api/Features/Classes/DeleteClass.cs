using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SynCore.Api.Common.Exceptions;
using SynCore.Api.Data;

namespace SynCore.Api.Features.Classes;

public static class DeleteClass
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

            var classObj = await _appDbContext.Classes
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

            if (classObj is null)
            {
                throw new AppException(StatusCodes.Status404NotFound, "máteria não encontrada");
            }

            _appDbContext.Classes.Remove(classObj);
            await _appDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}