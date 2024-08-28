using Microsoft.AspNetCore.Builder;

namespace Agenda.Common.Helpers.MigrationApplier;

public interface IMigrationApplier
{
    void ApplyMigrations(WebApplication app);
}
