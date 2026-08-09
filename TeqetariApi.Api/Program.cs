using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using TeqetariApi.Infrastructure.Persistence;
using TeqetariApi.Infrastructure.Services.Employees;
using TeqetariApi.Infrastructure.Services.Employers;
using TeqetariApi.Infrastructure.Services.JobPosts;
using TeqetariApi.Application.Interfaces;




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




builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp", policy =>
    {
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});



var app = builder.Build();

// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}


app.UseCors("AllowAngularApp");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
