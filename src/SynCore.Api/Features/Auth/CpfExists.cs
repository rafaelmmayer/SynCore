using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SynCore.Api.Data;

namespace SynCore.Api.Features.Auth;

public static class CpfExists
{
    public class Command : IRequest<bool>
    {
        public string Cpf { get; set; }
    }
    
    public class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleFor(c => c.Cpf)
                .NotEmpty().WithMessage("Cpf inválido")
                .IsValidCPF().WithMessage("Cpf inválido");
        }
    }
    
    public class Handler : IRequestHandler<Command, bool>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IValidator<Command> _validator;

        public Handler(AppDbContext appDbContext, IValidator<Command> validator)
        {
            _appDbContext = appDbContext;
            _validator = validator;
        }

        public async Task<bool> Handle(Command request, CancellationToken cancellationToken)
        {
            await _validator.ValidateAndThrowAsync(request, cancellationToken);

            return await _appDbContext.Users.AnyAsync(u => u.Cpf == request.Cpf, cancellationToken);
        }
    }
}