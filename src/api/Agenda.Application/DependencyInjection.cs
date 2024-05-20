using Microsoft.Extensions.DependencyInjection;

namespace Agenda.Application;

/// <summary>
/// Classe responsável por injetar as dependências de todos os serviços relacionados à camada de Application.
/// </summary>
public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(configure =>
            configure.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));

        return services;
    }
}
