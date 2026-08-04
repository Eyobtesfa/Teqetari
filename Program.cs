using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using TeqetariApi.Data;
using TeqetariApi.Services.Employees;
using TeqetariApi.Services.Employers;




var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddOpenApi();
builder.Services.AddScoped<IRegisterEmployeeService, EmployeeRegisterService>();
builder.Services.AddScoped<IRegisterEmployerService, EmployerRegisterService>();
builder.Services.AddDbContext<TeqetariDbContext>(options =>
options.UseNpgsql(builder.Configuration.GetConnectionString("TeqetariDatabase")));
builder.Services.AddControllers();
var app = builder.Build();

// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
