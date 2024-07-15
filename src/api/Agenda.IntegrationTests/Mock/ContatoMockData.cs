using Agenda.Common.Enums;
using Agenda.Domain.Entities;
using Agenda.Infrastructure.Data.Context;
using Microsoft.Extensions.DependencyInjection;

namespace Agenda.IntegrationTests.Mock;

internal sealed partial class ContatoMockData
{
    public static async Task CreateCategories(CustomWebApplicationFactory application, bool criar)
    {
        using var scope = application.Services.CreateScope();
        var provider = scope.ServiceProvider;

        using var contatoDbContext = provider.GetRequiredService<DataContext>();
        await contatoDbContext.Database.EnsureCreatedAsync();

        if (criar)
        {
            await contatoDbContext.Contato.AddAsync(Contato.CriarContato(
                "Nome Antigo",
                "email@antigo.com",
                "12345678",
                DDDs.SP
            ));

            await contatoDbContext.Contato.AddAsync(Contato.CriarContato(
                "Nome Antigo 2",
                "email2@antigo.com",
                "12345679",
                DDDs.MG
            ));

            await contatoDbContext.SaveChangesAsync();
        }
    }
}
