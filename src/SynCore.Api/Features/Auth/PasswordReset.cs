using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SynCore.Api.Common.Exceptions;
using SynCore.Api.Data;

namespace SynCore.Api.Features.Auth;

public static class PasswordReset
{
    public class Command : IRequest
    {
        public string Token { get; set; }
        public string Password { get; set; }
    }
    
    public class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleFor(c => c.Token)
                .NotNull()
                .NotEmpty();

            RuleFor(p => p.Password).Cascade(CascadeMode.Continue)
                .NotEmpty().WithMessage("Não pode ser vazia.")
                .MinimumLength(8).WithMessage("Deve conter pelo menos 8 caracteres.")
                .Matches(@"[A-Z]+").WithMessage("Deve conter pelo menos uma letra maiúscula.")
                .Matches(@"[a-z]+").WithMessage("Deve conter pelo menos uma letra minúscula.")
                .Matches(@"[0-9]+").WithMessage("Deve conter pelo menos um número.");
        }
    }
    
    public class Handler : IRequestHandler<Command>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IValidator<Command> _validator;

        public Handler(AppDbContext appDbContext, IValidator<Command> validator)
        {
            _appDbContext = appDbContext;
            _validator = validator;
        }
        
        public async Task Handle(Command request, CancellationToken cancellationToken)
        {
            await _validator.ValidateAndThrowAsync(request, cancellationToken);
            
            var passwordResetToken = await _appDbContext.PasswordResetTokens
                .Where(t => t.Token == request.Token)
                .FirstOrDefaultAsync(cancellationToken);
            
            if (passwordResetToken is null 
                || passwordResetToken.TokenExpiry < DateTime.UtcNow
                || passwordResetToken.WasUsed)
            {
                throw new AppException(StatusCodes.Status400BadRequest, "link inválido"); 
            }
            
            var password = await _appDbContext.Passwords
                .FirstOrDefaultAsync(u => u.UserId == passwordResetToken.UserId, cancellationToken);

            if (password is null)
            {
                throw new AppException(StatusCodes.Status404NotFound, "usuário não encontrado"); 
            }
            
            var newPassword = BCrypt.Net.BCrypt.HashPassword(request.Password);
            password.Value = newPassword;

            passwordResetToken.WasUsed = true;
            
            await _appDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}