using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SynCore.Api.Common.Exceptions;
using SynCore.Api.Data;
using SynCore.Core.Entities;

namespace SynCore.Api.Features.Auth;

public static class SignUp
{
    public class Command : IRequest<User>
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Cpf { get; set; }
        public string CollegeName { get; set; }
        public string Password { get; set; }
    }
    
    public class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("Nome não pode ser vazio");
            
            RuleFor(c => c.LastName)
                .NotEmpty().WithMessage("Sobrenome não pode ser vazio");
            
            RuleFor(c => c.Cpf).Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("CPF inválido")
                .IsValidCPF().WithMessage("CPF inválido");

            RuleFor(c => c.Email)
                .NotEmpty().WithMessage("E-mail inválido")
                .EmailAddress().WithMessage("E-mail inválido");
            
            RuleFor(c => c.CollegeName)
                .NotEmpty().WithMessage("Faculdade não pode ser vazio");
            
            RuleFor(p => p.Password).Cascade(CascadeMode.Continue)
                .NotEmpty().WithMessage("Senha não pode ser vazia.")
                .MinimumLength(8).WithMessage("Senha deve conter pelo menos 8 caracteres.")
                .Matches("[A-Z]+").WithMessage("Senha deve conter pelo menos uma letra maiúscula.")
                .Matches("[a-z]+").WithMessage("Senha deve conter pelo menos uma letra minúscula.")
                .Matches("[0-9]+").WithMessage("Senha deve conter pelo menos um número.");
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
            
            if (await _appDbContext.Users.AsNoTracking().AnyAsync(u => u.Cpf == request.Cpf, cancellationToken))
            {
                throw new AppException(StatusCodes.Status409Conflict, "cpf já cadastrado");
            }
            
            if (await _appDbContext.Users.AsNoTracking().AnyAsync(u => u.Email == request.Email, cancellationToken))
            {
                throw new AppException(StatusCodes.Status409Conflict, "e-mail já cadastrado");
            }
            
            var user = new User
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                LastName = request.LastName,
                Email = request.Email,
                Cpf = request.Cpf,
                CollegeName = request.CollegeName
            };
            var hasPassword = new Password()
            {
                Id = Guid.NewGuid(),
                Value = BCrypt.Net.BCrypt.HashPassword(request.Password),
                User = user,
                UserId = user.Id
            };

            await _appDbContext.Users.AddAsync(user, cancellationToken);
            await _appDbContext.Passwords.AddAsync(hasPassword, cancellationToken);
            await _appDbContext.SaveChangesAsync(cancellationToken);

            return user;
        }
    }
}