using Agenda.Common.Helpers.DependencyInstaller;
using Agenda.Modules.Eventos.CrossCutting.DependencyInjection.Application;
using Agenda.Modules.Eventos.CrossCutting.DependencyInjection.Persistence;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Agenda.Modules.Eventos.CrossCutting.DependencyInjection;

public class DependencyInjection : IDependencyInstaller
{
    public void InstallService(
        IServiceCollection services,
        IConfiguration configuration)
        => services
            .AddApplication()
            .AddPersistence(configuration);
}
