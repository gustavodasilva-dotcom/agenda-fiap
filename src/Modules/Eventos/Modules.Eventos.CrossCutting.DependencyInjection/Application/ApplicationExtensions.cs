using Microsoft.Extensions.DependencyInjection;

namespace Agenda.Modules.Eventos.CrossCutting.DependencyInjection.Application;

internal static partial class ApplicationExtensions
{
    public static IServiceCollection AddApplication(
        this IServiceCollection services)
    {
        var assembly = Eventos.Application.AssemblyReference.Assembly;

        services.AddAutoMapper(assembly);

        services.AddMediatR(config
            => config.RegisterServicesFromAssembly(assembly));

        return services;
    }
}
