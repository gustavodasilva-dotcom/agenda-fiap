using Agenda.Common.Helpers.MigrationApplier;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Agenda.Modules.Eventos.Persistence;

public class EventosMigrationApplier : IMigrationApplier
{
    public void ApplyMigrations(WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        var dbContext = scope.ServiceProvider.
            GetRequiredService<EventosDbContext>();

        if (dbContext.Database.IsRelational())
        {
            dbContext.Database.Migrate();
        }
    }
}
