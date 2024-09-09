using Agenda.App;
using Agenda.Modules.Contatos.Persistence;
using Agenda.Common.DependencyInjection.Factories;

namespace Agenda.Modules.Contatos.IntegrationTests;

internal sealed partial class ContatosWebApplicationFactory : CustomWebApplicationFactory<Program, ContatosDbContext>
{
}
