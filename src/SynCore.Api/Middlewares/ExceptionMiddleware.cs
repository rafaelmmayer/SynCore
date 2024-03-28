using SynCore.Api.Common.Exceptions;

namespace SynCore.Api.Middlewares;

public class ExceptionMiddleware : IMiddleware
{
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(ILogger<ExceptionMiddleware> logger)
    {
        _logger = logger;
    }
    
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next.Invoke(context);
        }
        catch (FluentValidation.ValidationException e)
        {
            _logger.LogInformation("Status: {Status} - Mensagem: {Message}", 403, e.Message);
            
            context.Response.StatusCode = 422;
            await context.Response.WriteAsJsonAsync(new
            {
                error = e.Errors
            });
        }
        catch (AppException e)
        {
            _logger.LogInformation("Status: {Status} - Mensagem: {Message}", e.Status, e.Message);
            
            context.Response.StatusCode = e.Status;
            await context.Response.WriteAsync(e.Message);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "erro não mapeado");
            
            context.Response.StatusCode = 500;
            await context.Response.WriteAsync("erro não mapeado");
        }
    }
}