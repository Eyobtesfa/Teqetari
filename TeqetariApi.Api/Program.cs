using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using TeqetariApi.Infrastructure.Persistence;
using TeqetariApi.Infrastructure.Services.Employees;
using TeqetariApi.Infrastructure.Services.Employers;
using TeqetariApi.Infrastructure.Services.JobPosts;
using TeqetariApi.Application.Interfaces;
using Microsoft.AspNetCore.Antiforgery;




var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddOpenApi();
builder.Services.AddScoped<IRegisterEmployeeService, EmployeeRegisterService>();
builder.Services.AddScoped<IRegisterEmployerService, EmployerRegisterService>();
builder.Services.AddScoped<IPostJobService, PostJobService>();
builder.Services.AddDbContext<TeqetariDbContext>(options =>
options.UseNpgsql(builder.Configuration.GetConnectionString("TeqetariDatabase")));
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(
            new System.Text.Json.Serialization.JsonStringEnumConverter()
        );
    });


var allowedOrigins = builder.Configuration
    .GetSection("AllowedOrigins").Get<string[]>()
    ?? ["http://localhost:4200"];



builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp", policy =>
    {
        policy.WithOrigins(allowedOrigins)
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials()
              .SetPreflightMaxAge(TimeSpan.FromMinutes(10));
    });
});


builder.Services.AddAntiforgery(options =>
{
    options.HeaderName = "X-XSRF-TOKEN";
});

var app = builder.Build();

// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseRouting();
app.UseCors("AllowAngularApp");

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Use(async (context, next) =>
{
    if (context.Request.Cookies.ContainsKey("teqetari_id"))
    {
        var antiforgery = context.RequestServices.GetRequiredService<IAntiforgery>();
        var tokens = antiforgery.GetAndStoreTokens(context);
        context.Response.Cookies.Append("XSRF-TOKEN", tokens.RequestToken!, new CookieOptions
        {
            HttpOnly = false,
            Secure = !builder.Environment.IsDevelopment(),
            SameSite = SameSiteMode.Strict
        });
    }
    await next(context);
});

app.Run();
