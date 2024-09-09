using Microsoft.Extensions.DependencyInjection;

namespace Agenda.Modules.Contatos.CrossCutting.DependencyInjection.Application;

internal static partial class ApplicationExtensions
{
    public static IServiceCollection AddApplication(
        this IServiceCollection services)
    {
        services.AddMediatR(config
            => config.RegisterServicesFromAssembly(
                   Contatos.Application.AssemblyReference.Assembly));

        return services;
    }
}
