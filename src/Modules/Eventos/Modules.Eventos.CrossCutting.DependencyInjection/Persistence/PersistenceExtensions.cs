using Agenda.Common.Shared.Abstractions;
using Agenda.Common.Shared.Repositories;
using Agenda.Modules.Contatos.Domain.Abstractions;
using Agenda.Modules.Eventos.Domain.Abstractions;
using Agenda.Modules.Eventos.Domain.Entities;
using Agenda.Modules.Eventos.Persistence;
using Agenda.Modules.Eventos.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Agenda.Modules.Eventos.CrossCutting.DependencyInjection.Persistence;

internal static partial class PersistenceExtensions
{
    private static IServiceCollection AddEntityFrameworkCore(
        this IServiceCollection services, IConfiguration configuration)
        => services.AddDbContext<EventosDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("Default")));

    private static IServiceCollection AddRepositories(
        this IServiceCollection services)
        => services
            .AddScoped<EventosDbContext>()
            .AddKeyedScoped<IUnitOfWork, UnitOfWork<EventosDbContext>>(nameof(Eventos))
            .AddScoped<IEventoRepository, EventoRepository>()
            .AddScoped<IBaseRepository<EventoContato>, BaseRepository<EventosDbContext, EventoContato>>();

    public static IServiceCollection AddPersistence(
        this IServiceCollection services, IConfiguration configuration)
        => services
            .AddEntityFrameworkCore(configuration)
            .AddRepositories();
}
