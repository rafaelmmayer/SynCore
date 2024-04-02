using System.Web;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SynCore.Api.Common.Exceptions;
using SynCore.Api.Data;
using SynCore.Api.Services.Email;
using SynCore.Core.Entities;

namespace SynCore.Api.Features.Auth;

public static class EmailPasswordReset
{
    public class Command : IRequest
    {
        public string Email { get; set; }
    }

    public class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleFor(c => c.Email)
                .NotEmpty().WithMessage("E-mail inválido")
                .EmailAddress().WithMessage("E-mail inválido");
        }
    }
    
    public class Handler : IRequestHandler<Command>
    {
        private readonly IValidator<Command> _validator;
        private readonly AppDbContext _appDbContext;
        private readonly IEmailService _emailService;
        private readonly ILogger<Handler> _logger;

        public Handler(
            IValidator<Command> validator, 
            AppDbContext appDbContext, 
            IEmailService emailService,
            ILogger<Handler> logger)
        {
            _validator = validator;
            _appDbContext = appDbContext;
            _emailService = emailService;
            _logger = logger;
        }

        public async Task Handle(Command request, CancellationToken cancellationToken)
        {
            await _validator.ValidateAndThrowAsync(request, cancellationToken);
            
            var userId = await _appDbContext.Users
                .AsNoTracking()
                .Where(u => u.Email == request.Email)
                .Select(u => new { u.Id })
                .FirstOrDefaultAsync(cancellationToken);

            if (userId is null)
            {
                return;
            }
            
            var token = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
            
            var passwordResetToken = new PasswordResetToken
            {
                Id = Guid.NewGuid(),
                Token = token,
                TokenExpiry = DateTime.UtcNow.AddHours(3),
                UserId = userId.Id
            };
            
            await _appDbContext.PasswordResetTokens.AddAsync(passwordResetToken, cancellationToken);
            await _appDbContext.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Token: {Token}", token);

            var urlToken = HttpUtility.UrlEncode(token);
            await _emailService.SendEmailPasswordReset(urlToken);
        }
    }
}