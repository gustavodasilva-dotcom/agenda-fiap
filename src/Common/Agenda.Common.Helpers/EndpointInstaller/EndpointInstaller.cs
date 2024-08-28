using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Agenda.Common.Helpers.EndpointInstaller;

public static partial class EndpointInstaller
{
    public static IServiceCollection InstallEndpoints(
        this IServiceCollection services,
        Assembly assembly)
    {
        ServiceDescriptor[] serviceDescriptors = assembly
            .DefinedTypes
            .Where(type => type is { IsAbstract: false, IsInterface: false } &&
                           type.IsAssignableTo(typeof(IEndpointInstaller)))
            .Select(type => ServiceDescriptor.Transient(typeof(IEndpointInstaller), type))
            .ToArray();

        services.TryAddEnumerable(serviceDescriptors);

        return services;
    }

    public static IApplicationBuilder MapEndpoints(
        this WebApplication app,
        RouteGroupBuilder? routeGroupBuilder = null)
    {
        IEnumerable<IEndpointInstaller> endpoints = app.Services
            .GetRequiredService<IEnumerable<IEndpointInstaller>>();

        IEndpointRouteBuilder builder =
            routeGroupBuilder is null ? app : routeGroupBuilder;

        foreach (IEndpointInstaller endpoint in endpoints)
        {
            endpoint.InstallEndpoint(builder);
        }

        return app;
    }
}
