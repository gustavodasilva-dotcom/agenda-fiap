using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Agenda.Common.Helpers.DependencyInstaller;

public interface IDependencyInstaller
{
    void InstallService(
        IServiceCollection services,
        IConfiguration configuration);
}
