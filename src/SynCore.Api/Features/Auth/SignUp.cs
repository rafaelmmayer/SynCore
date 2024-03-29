﻿using FluentValidation;
using MediatR;
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
                .NotNull()
                .NotEmpty();
            
            RuleFor(c => c.LastName)
                .NotNull()
                .NotEmpty();
            
            RuleFor(c => c.Cpf)
                .NotNull()
                .NotEmpty()
                .IsValidCPF();

            RuleFor(c => c.Email)
                .NotNull()
                .NotEmpty()
                .EmailAddress();
            
            RuleFor(c => c.CollegeName)
                .NotNull()
                .NotEmpty();
            
            RuleFor(p => p.Password).Cascade(CascadeMode.Continue)
                .NotEmpty().WithMessage("Não pode ser vazia.")
                .MinimumLength(8).WithMessage("Deve conter pelo menos 8 caracteres.")
                .Matches("[A-Z]+").WithMessage("Deve conter pelo menos uma letra maiúscula.")
                .Matches("[a-z]+").WithMessage("Deve conter pelo menos uma letra minúscula.")
                .Matches("[0-9]+").WithMessage("Deve conter pelo menos um número.");
        }
    }
    
    public class Handler : IRequestHandler<Command, User>
    {
        private readonly AppDbContext _appDbContext;

        public Handler(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<User> Handle(Command request, CancellationToken cancellationToken)
        {
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