using Agenda.App;
using Agenda.Common.DependencyInjection.Factories;
using Agenda.Modules.Eventos.Persistence;

namespace Agenda.Modules.Eventos.IntegrationTests;

internal sealed partial class EventosWebApplicationFactory : CustomWebApplicationFactory<Program, EventosDbContext>
{
}
