using Agenda.FIAP.Api.Domain.Abstractions;
using Agenda.FIAP.Api.Infrastructure.Data.Context;
using Agenda.FIAP.Api.Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Agenda.FIAP.Api.Infrastructure;

/// <summary>
/// Classe responsável por injetar as dependências de todos os serviços relacionados à camada de Infrastructure.
/// </summary>
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<DataContext, DataContext>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IContatoRepository, ContatoRepository>();

        return services;
    }
}
