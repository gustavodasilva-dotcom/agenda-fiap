using Agenda.FIAP.Api.Application;
using Agenda.FIAP.Api.Constants;
using Agenda.FIAP.Api.Extensions;
using Agenda.FIAP.Api.Infrastructure;
using Agenda.FIAP.Api.Middlewares;
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
