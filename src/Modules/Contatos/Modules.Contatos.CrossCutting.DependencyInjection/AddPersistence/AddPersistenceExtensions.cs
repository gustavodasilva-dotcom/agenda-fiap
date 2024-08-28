using Agenda.Modules.Contatos.Domain.Abstractions;
using Agenda.Modules.Contatos.Persistence;
using Agenda.Modules.Contatos.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Agenda.Modules.Contatos.CrossCutting.DependencyInjection.AddPersistence;

internal static class AddPersistenceExtensions
{
    private static IServiceCollection AddEntityFrameworkCore(
        this IServiceCollection services, IConfiguration configuration)
        => services.AddDbContext<ContatosDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("Default")));

    private static IServiceCollection AddRepositories(
        this IServiceCollection services)
        => services
            .AddScoped<ContatosDbContext>()
            .AddScoped<IUnitOfWork, UnitOfWork>()
            .AddScoped<IContatoRepository, ContatoRepository>();

    public static IServiceCollection AddPersistence(
        this IServiceCollection services, IConfiguration configuration)
        => services
            .AddEntityFrameworkCore(configuration)
            .AddRepositories();
}
