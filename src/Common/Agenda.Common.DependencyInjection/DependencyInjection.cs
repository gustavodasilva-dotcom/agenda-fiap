using Agenda.Common.DependencyInjection.Options;
using Agenda.Common.Helpers.DependencyInstaller;
using Agenda.Common.Helpers.EndpointInstaller;
using Agenda.Common.Helpers.MigrationApplier;
using Agenda.Common.Shared.Extensions;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;

namespace Agenda.Common.DependencyInjection;

public static partial class DependencyInjection
{
    public static void RegisterApp(this WebApplicationBuilder builder)
    {
        builder.Services.InstallDependencies(
            builder.Configuration,
            Modules.Contatos.CrossCutting.DependencyInjection.AssemblyReference.Assembly,
            Modules.Eventos.CrossCutting.DependencyInjection.AssemblyReference.Assembly,
            Modules.Notificacoes.CrossCutting.DependencyInjection.AssemblyReference.Assembly);

        builder.Services.InstallEndpoints(
            Modules.Contatos.Endpoints.AssemblyReference.Assembly,
            Modules.Eventos.Endpoints.AssemblyReference.Assembly);

        builder.Services.AddMassTransit(busConfigurator =>
        {
            busConfigurator.AddConsumers(
                Modules.Contatos.Application.AssemblyReference.Assembly,
                Modules.Eventos.Application.AssemblyReference.Assembly,
                Modules.Notificacoes.Application.AssemblyReference.Assembly);

            busConfigurator.SetKebabCaseEndpointNameFormatter();

            var messagingSettings = builder.Configuration
                .GetSection(MessageBrokerOptions.Position)
                .Get<MessageBrokerOptions>() ??
                throw new InvalidOperationException($"Missing configuration for {MessageBrokerOptions.Position}.");

            busConfigurator.AddConfigureEndpointsCallback((context, name, configurator) =>
            {
                configurator.UseMessageRetry(retryFilter => retryFilter
                    .Immediate(messagingSettings.NumberOfRetries));

                KillSwitchOptions killSwitchSettings = messagingSettings.KillSwitch;

                configurator.UseKillSwitch(config => config
                    .SetActivationThreshold(killSwitchSettings.ActivationThreshold)
                    .SetTripThreshold(killSwitchSettings.TripThreshold)
                    .SetRestartTimeout(m: killSwitchSettings.RestartMinutesTimeout));
            });

            busConfigurator.UsingRabbitMq((context, configurator) =>
            {
                configurator.Host(messagingSettings.Host, host =>
                {
                    host.Username(messagingSettings.Username);
                    host.Password(messagingSettings.Password);
                });

                configurator.ConfigureEndpoints(context);
            });
        });
    }

    public static void UseApp(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.ApplyMigrations(
                Modules.Contatos.Persistence.AssemblyReference.Assembly,
                Modules.Eventos.Persistence.AssemblyReference.Assembly);
        }

        app.UseStaticFiles(new StaticFileOptions
        {
            FileProvider = new PhysicalFileProvider(
                app.Environment.ContentRootPath.ConcatenarCaminho("Contents")),
            RequestPath = "/resources"
        });

        app.MapEndpoints();
    }
}
