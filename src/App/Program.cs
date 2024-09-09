using Agenda.App.Constants;
using Agenda.Common.DependencyInjection;
using Agenda.Common.Helpers.Middlewares;
using Prometheus;

namespace Agenda.App;

public partial class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.RegisterApp();

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddExceptionHandler<ExceptionHandler>();
        builder.Services.AddProblemDetails();

        builder.Services.UseHttpClientMetrics();

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
        }

        app.UseApp();

        app.UseHttpsRedirection();

        app.UseExceptionHandler();

        app.UseMetricServer();
        app.UseHttpMetrics();

        app.Run();
    }
}
