using Agenda.App;
using Agenda.Modules.Contatos.Persistence;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using MediatR;
using System.Linq;
using Agenda.Common.Shared.Abstractions;
using Agenda.Common.DependencyInjection;

internal sealed partial class ContatosWebApplicationFactory : CustomWebApplicationFactory<Program, ContatosDbContext>
{
}
