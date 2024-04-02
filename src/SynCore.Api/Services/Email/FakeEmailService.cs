namespace SynCore.Api.Services.Email;

public class FakeEmailService : IEmailService
{
    private readonly ILogger<FakeEmailService> _logger;

    public FakeEmailService(ILogger<FakeEmailService> logger)
    {
        _logger = logger;
    }

    public Task SendEmailPasswordReset(string token)
    {
        _logger.LogInformation("Token url: {EmailPasswordResetToken}", token);
        
        return Task.CompletedTask;
    }
}