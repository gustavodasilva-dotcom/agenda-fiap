﻿using Agenda.FIAP.Api.Domain.Abstractions;
using Agenda.FIAP.Api.Infrastructure.Data.Context;
using Agenda.FIAP.Api.Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Agenda.FIAP.Api.Infrastructure;

/// <summary>
/// Classe responsável por injetar as dependências de todos os serviços relacionados à camada de Infrastructure.
/// </summary>
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<DataContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        },
        ServiceLifetime.Scoped);

        services.AddScoped<DataContext, DataContext>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IContatoRepository, ContatoRepository>();

        return services;
    }
}
