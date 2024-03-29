﻿using SynCore.Api.Common.Exceptions;

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
        catch (AppException e)
        {
            _logger.LogError(e, "Status: {Status} - Mensagem: {Message}", e.Status, e.Message);
            
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