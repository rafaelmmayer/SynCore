using Carter;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Serilog;
using SynCore.Api.Data;
using SynCore.Api.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((hostBuilder, loggerConfiguration) =>
{
    loggerConfiguration.ReadFrom.Configuration(hostBuilder.Configuration);
});
builder.Services.AddMediatR(s =>
{
    s.RegisterServicesFromAssembly(typeof(Program).Assembly);
});
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("Postgres"));
});
builder.Services.AddCarter();
builder.Services.AddControllers();
builder.Services.AddScoped<ExceptionMiddleware>();
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
    });
builder.Services.AddAuthorization();
builder.Services.AddCors();

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

app.UseAuthentication();
app.UseAuthorization();

if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.MapFallbackToFile("index.html");

app.MapControllers();

app.Run();
