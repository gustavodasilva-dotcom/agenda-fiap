using Microsoft.Extensions.DependencyInjection;

namespace Agenda.FIAP.Api.Infrastructure;

/// <summary>
/// Classe responsável por injetar as dependências de todos os serviços relacionados à camada de Infrastructure.
/// </summary>
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        return services;
    }
}
