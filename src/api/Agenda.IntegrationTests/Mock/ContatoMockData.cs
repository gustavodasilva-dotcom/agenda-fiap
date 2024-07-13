using Agenda.Application.Contracts.Requests;
using Agenda.Common.Enums;
using Agenda.Domain.Entities;
using Agenda.Infrastructure.Data.Context;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agenda.IntegrationTests.Mock
{
    public class ContatoMockData
    {
        public static async Task CreateCategories(CustomWebApplicationFactory application, bool criar)
        {
            using (var scope = application.Services.CreateScope())
            {
                var provider = scope.ServiceProvider;
                using (var contatoDbContext = provider.GetRequiredService<DataContext>())
                {
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
        }
    }
}
