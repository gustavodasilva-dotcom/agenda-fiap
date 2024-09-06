using Agenda.App;
using Agenda.Common.DependencyInjection;
using Agenda.Modules.Eventos.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modules.Eventos.IntegrationTests
{
    public sealed class EventosWebApplicationFactory : CustomWebApplicationFactory<Program, EventosDbContext>
    {
    }
}
