using System.Text.Json.Serialization;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Serilog;
using SynCore.Api.Data;
using SynCore.Api.Middlewares;
using SynCore.Api.Services;
using SynCore.Api.Services.Email;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((hostBuilder, loggerConfiguration) =>
{
    loggerConfiguration.ReadFrom.Configuration(hostBuilder.Configuration);
});

builder.Services.AddMediatR(s =>
{
    s.RegisterServicesFromAssembly(typeof(Program).Assembly);
});

var conn = builder.Configuration.GetConnectionString("Postgres");
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseNpgsql(conn);
});

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });


builder.Services
    .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(op =>
    {
        if (builder.Environment.IsProduction())
        {
            op.Cookie.Domain = builder.Configuration.GetValue<string>("Cookie:Domain");
            op.ExpireTimeSpan = TimeSpan.FromDays(3);
            op.Cookie.MaxAge = op.ExpireTimeSpan;
            op.SlidingExpiration = true;
        }
        op.Cookie.SecurePolicy = CookieSecurePolicy.None;

        op.Events.OnRedirectToAccessDenied =
        op.Events.OnRedirectToLogin = c =>
        {
            c.Response.StatusCode = StatusCodes.Status401Unauthorized;
            return Task.FromResult<object>(null);
        };
    });
builder.Services.AddAuthorization();

builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);

builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<ExceptionMiddleware>();

builder.Services.AddScoped<AuthUserProvider>();

builder.Services.AddTransient<IEmailService, FakeEmailService>();

var app = builder.Build();

app.UseFileServer();

app.UseMiddleware<ExceptionMiddleware>();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("index.html");

app.Run();
