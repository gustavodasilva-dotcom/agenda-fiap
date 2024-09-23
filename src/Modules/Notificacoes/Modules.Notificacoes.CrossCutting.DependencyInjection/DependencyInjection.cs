using Agenda.Common.Helpers.DependencyInstaller;
using Agenda.Modules.Notificacoes.CrossCutting.DependencyInjection.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Agenda.Modules.Notificacoes.CrossCutting.DependencyInjection;

public class DependencyInjection : IDependencyInstaller
{
    public void InstallService(IServiceCollection services, IConfiguration configuration)
        => services.AddInfrastructure(configuration);
}
