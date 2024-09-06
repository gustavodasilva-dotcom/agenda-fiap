using Agenda.Common.Helpers.MigrationApplier;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Agenda.Modules.Contatos.Persistence;

internal sealed class ContatosMigrationApplier : IMigrationApplier
{
    public void ApplyMigrations(WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        
        var dbContext = scope.ServiceProvider.
            GetRequiredService<ContatosDbContext>();

        if (dbContext.Database.IsRelational())
        {
            dbContext.Database.Migrate();
        }
    }
}