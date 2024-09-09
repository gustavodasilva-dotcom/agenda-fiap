using Agenda.Common.Helpers.DependencyInstaller;
using Agenda.Modules.Contatos.CrossCutting.DependencyInjection.Application;
using Agenda.Modules.Contatos.CrossCutting.DependencyInjection.Persistence;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Agenda.Modules.Contatos.CrossCutting.DependencyInjection;

public class DependencyInjection : IDependencyInstaller
{
    public void InstallService(
        IServiceCollection services,
        IConfiguration configuration)
        => services
            .AddApplication()
            .AddPersistence(configuration);
}
