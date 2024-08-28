using Microsoft.Extensions.DependencyInjection;

namespace Agenda.Modules.Contatos.CrossCutting.DependencyInjection.AddApplication;

internal static class AddApplicationExtensions
{
    public static IServiceCollection AddApplication(
        this IServiceCollection services)
    {
        services.AddMediatR(config
            => config.RegisterServicesFromAssembly(
                    Application.AssemblyReference.Assembly));

        return services;
    }
}
