using Agenda.FIAP.Api.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Agenda.FIAP.Api.Extensions;

public static class MigrationExtensions
{
    public static void ApplyMigrations(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        var dbContext = scope.ServiceProvider
            .GetRequiredService<DataContext>();

        dbContext.Database.Migrate();
    }
}
