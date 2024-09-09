using Agenda.Modules.Eventos.Domain.Entities;
using Agenda.Modules.Eventos.Persistence;
using Microsoft.Extensions.DependencyInjection;

namespace Agenda.Modules.Eventos.IntegrationTests.Mock;

internal sealed class EventoMockData
{
    public static async Task CreateEventos(
        EventosWebApplicationFactory application,
        bool criar)
    {
        using var scope = application.Services.CreateScope();
        var provider = scope.ServiceProvider;

        using var eventoDbContext = provider
            .GetRequiredService<EventosDbContext>();

        await eventoDbContext.Database.EnsureCreatedAsync();

        if (criar)
        {
            await eventoDbContext.Eventos.AddAsync(
                Evento.CriarEvento(
                    "Nome Evento Antigo",
                    DateTime.MinValue,
                    DateTime.Now));

            await eventoDbContext.Eventos.AddAsync(
                Evento.CriarEvento(
                    "Nome Evento Antigo 2",
                    DateTime.MinValue,
                    DateTime.Now));

            await eventoDbContext.SaveChangesAsync();
        }
    }
}

