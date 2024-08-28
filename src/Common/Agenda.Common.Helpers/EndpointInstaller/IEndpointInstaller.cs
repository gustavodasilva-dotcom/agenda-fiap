using Microsoft.AspNetCore.Routing;

namespace Agenda.Common.Helpers.EndpointInstaller;

public interface IEndpointInstaller
{
    void InstallEndpoint(IEndpointRouteBuilder app);
}
