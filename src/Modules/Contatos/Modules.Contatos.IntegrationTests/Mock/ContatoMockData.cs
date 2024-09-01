using Agenda.Common.Shared.Enums;
using Agenda.Modules.Contatos.Domain.Entities;
using Agenda.Modules.Contatos.Persistence;
using Microsoft.Extensions.DependencyInjection;

namespace Agenda.Modules.Contatos.IntegrationTests.Mock;

internal sealed partial class ContatoMockData
{
    public static async Task CreateCategories(
        CustomWebApplicationFactory application,
        bool criar)
    {
        using var scope = application.Services.CreateScope();
        var provider = scope.ServiceProvider;

        using var contatoDbContext = provider
            .GetRequiredService<ContatosDbContext>();
        
        await contatoDbContext.Database.EnsureCreatedAsync();

        if (criar)
        {
            await contatoDbContext.Contatos.AddAsync(
                Contato.CriarContato(
                    "Nome Antigo",
                    "email@antigo.com",
                    "12345678",
                    DDDs.SP));

            await contatoDbContext.Contatos.AddAsync(
                Contato.CriarContato(
                    "Nome Antigo 2",
                    "email2@antigo.com",
                    "12345679",
                    DDDs.MG
                ));

            await contatoDbContext.SaveChangesAsync();
        }
    }
}
