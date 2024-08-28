using System.Reflection;
using Agenda.Common.Helpers.Extensions;
using Microsoft.AspNetCore.Builder;

namespace Agenda.Common.Helpers.MigrationApplier;

public static partial class MigrationApplier
{
    public static void ApplyMigrations(
        this WebApplication app,
        params Assembly[] assemblies)
    {
        var migrationAppliers = assemblies
            .GetTypesFromAssemblies<IMigrationApplier>();

        migrationAppliers.ForEach(
            ma => ma.ApplyMigrations(app));
    }
}
