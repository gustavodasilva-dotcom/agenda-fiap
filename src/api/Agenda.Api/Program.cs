using Agenda.Api.Constants;
using Agenda.Api.Extensions;
using Agenda.Api.Middlewares;
using Agenda.Application;
using Agenda.Infrastructure;
using Carter;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();

builder.Services.AddExceptionHandler<ExceptionHandler>();
builder.Services.AddProblemDetails();

builder.Services.AddCarter();

builder.Services.AddCors(options =>
{
    options.AddPolicy(
        Cors.Policy,
        policy => policy
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod());
});

var app = builder.Build();

app.UseCors(Cors.Policy);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    app.ApplyMigrations();
}

app.UseHttpsRedirection();

app.UseExceptionHandler();

app.MapCarter();

app.Run();

public partial class Program { }