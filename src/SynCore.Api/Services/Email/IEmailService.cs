namespace SynCore.Api.Services.Email;

public interface IEmailService
{
    Task SendEmailPasswordReset(string token);
}