using Agenda.Modules.Notificacoes.Domain;
using Agenda.Modules.Notificacoes.Infrastructure.Options;
using Agenda.Modules.Notificacoes.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Agenda.Modules.Notificacoes.CrossCutting.DependencyInjection.Infrastructure;

internal static partial class InfrastructureExtensions
{
    private static IServiceCollection AddOptions(
        this IServiceCollection services, IConfiguration configuration)
        => services.Configure<SmtpOptions>(configuration.GetSection(SmtpOptions.Position));

    private static IServiceCollection AddServices(this IServiceCollection services)
        => services.AddScoped<ISmtpService, SmtpService>();

    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services, IConfiguration configuration)
        => services
            .AddOptions(configuration)
            .AddServices();
}
