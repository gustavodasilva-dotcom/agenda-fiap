using Microsoft.Extensions.DependencyInjection;

namespace Agenda.Modules.Eventos.CrossCutting.DependencyInjection.Application;

internal static partial class ApplicationExtensions
{
    public static IServiceCollection AddApplication(
        this IServiceCollection services)
    {
        services.AddMediatR(config
            => config.RegisterServicesFromAssembly(
                   Eventos.Application.AssemblyReference.Assembly));

        return services;
    }
}
