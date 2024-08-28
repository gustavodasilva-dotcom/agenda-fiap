using Agenda.Common.Helpers.DependencyInstaller;
using Agenda.Modules.Contatos.CrossCutting.DependencyInjection.AddApplication;
using Agenda.Modules.Contatos.CrossCutting.DependencyInjection.AddPersistence;
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
