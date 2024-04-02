using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SynCore.Api.Common.Exceptions;
using SynCore.Api.Data;
using SynCore.Core.Entities;
using ValidationException = FluentValidation.ValidationException;

namespace SynCore.Api.Features.Auth;

public static class SignIn
{
    public class Command : IRequest<User>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleFor(c => c.Email)
                .NotEmpty().WithMessage("E-mail inválido")
                .EmailAddress().WithMessage("E-mail inválido");

            RuleFor(c => c.Password)
                .NotEmpty().WithMessage("Senha inválida");
        }
    }
    
    public class Handler : IRequestHandler<Command, User>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IValidator<Command> _validator;

        public Handler(AppDbContext appDbContext, IValidator<Command> validator)
        {
            _appDbContext = appDbContext;
            _validator = validator;
        }

        public async Task<User> Handle(Command request, CancellationToken cancellationToken)
        {
            await _validator.ValidateAndThrowAsync(request, cancellationToken);
            
            var user = await _appDbContext.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Email == request.Email, cancellationToken);

            if (user is null)
            {
                throw new AppException(StatusCodes.Status404NotFound, "usuário não encontrado");
            }

            var password = await _appDbContext.Passwords
                .AsNoTracking()
                .FirstAsync(p => p.UserId == user.Id, cancellationToken);
            
            if (!BCrypt.Net.BCrypt.Verify(request.Password, password.Value))
            {
                throw new AppException(StatusCodes.Status404NotFound, "usuário não encontrado");
            }
            
            return user;
        }
    }
}