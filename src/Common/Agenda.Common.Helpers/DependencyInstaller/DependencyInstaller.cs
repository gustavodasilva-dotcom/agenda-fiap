using System.Reflection;
using Agenda.Common.Helpers.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Agenda.Common.Helpers.DependencyInstaller;

public static partial class DependencyInstaller
{
    public static IServiceCollection InstallDependencies(
        this IServiceCollection services,
        IConfiguration configuration,
        params Assembly[] assemblies)
    {
        var dependencyInstallers = assemblies
            .GetTypesFromAssemblies<IDependencyInstaller>();

        dependencyInstallers.ForEach(
            si => si.InstallService(
                services,
                configuration));

        return services;
    }
}
