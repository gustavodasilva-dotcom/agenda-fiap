using Agenda.Common.DependencyInjection.Options;
using Agenda.Common.Helpers.DependencyInstaller;
using Agenda.Common.Helpers.EndpointInstaller;
using Agenda.Common.Helpers.MigrationApplier;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Modules.Eventos.Endpoints;

namespace Agenda.Common.DependencyInjection;

public static partial class DependencyInjection
{
    public static void RegisterApp(this WebApplicationBuilder builder)
    {
        builder.Services.InstallDependencies(
            builder.Configuration,
            Modules.Contatos.CrossCutting.DependencyInjection.AssemblyReference.Assembly,
            Modules.Eventos.CrossCutting.DependencyInjection.AssemblyReference.Assembly);

        builder.Services.InstallEndpoints(
            Modules.Contatos.Endpoints.AssemblyReference.Assembly,
            AssemblyReference.Assembly);

        builder.Services.Configure<MessageBrokerOptions>(
            builder.Configuration.GetSection(MessageBrokerOptions.Position));

        builder.Services.AddSingleton(sp =>
            sp.GetRequiredService<IOptions<MessageBrokerOptions>>().Value);

        builder.Services.AddMassTransit(busConfigurator =>
        {
            busConfigurator.AddConsumers(
                Modules.Eventos.Application.AssemblyReference.Assembly);

            busConfigurator.SetKebabCaseEndpointNameFormatter();

            busConfigurator.UsingRabbitMq((context, configurator) =>
            {
                MessageBrokerOptions options = context
                    .GetRequiredService<MessageBrokerOptions>();

                configurator.Host(options.Host, host =>
                {
                    host.Username(options.Username);
                    host.Password(options.Password);
                });

                configurator.ConfigureEndpoints(context);
            });
        });
    }

    public static void UseApp(this WebApplication app)
    {
        if (app.Environment.IsDevelopment() && !IsTestEnvironment())
        {
            app.ApplyMigrations(
                Modules.Contatos.Persistence.AssemblyReference.Assembly,
                Modules.Eventos.Persistence.AssemblyReference.Assembly);
        }

        app.MapEndpoints();
    }

    public static bool IsTestEnvironment() {
        return Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "IntegrationTest";
    }
}
