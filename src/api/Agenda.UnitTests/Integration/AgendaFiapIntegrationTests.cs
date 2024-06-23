using Agenda.Application.Contatos.Commands.AdicionarContatos;
using Agenda.Application.Contracts.Requests;
using Agenda.Common.Enums;
using Agenda.Infrastructure.Data.Context;
using Agenda.Infrastructure.Data.Repositories;
using Agenda.UnitTests.Utils;
using Microsoft.EntityFrameworkCore;

namespace Agenda.UnitTests.Integration
{
    public class AgendaFiapIntegrationTests : IClassFixture<DockerFixture>
    {
        private readonly DockerFixture _dockerFixture;
        private readonly DbContextOptions<DataContext> _dbContextOptions;

        public AgendaFiapIntegrationTests(DockerFixture dockerFixture) {
            _dockerFixture = dockerFixture;

            _dbContextOptions = new DbContextOptionsBuilder<DataContext>()
                .UseSqlServer(_dockerFixture.GetConnectionString("AgendaFiapTest"), options =>
                    options.EnableRetryOnFailure(
                        maxRetryCount: 5,
                        maxRetryDelay: TimeSpan.FromSeconds(10), 
                        errorNumbersToAdd: null)) 
                .Options;

            using (var context = new DataContext(_dbContextOptions)) {
                context.Database.EnsureCreated();
            }
        }

        [Fact]
        public async Task TestDatabaseFunctionality() {
            using (var context = new DataContext(_dbContextOptions)) {

                var contatoRepository = new ContatoRepository(context);
                var unitOfWork = new UnitOfWork(context);

                var handler = new AdicionarContatosCommandHandler(contatoRepository, unitOfWork);

                List<ContatoRequest> contatos = new List<ContatoRequest>()
                {
                    new()
                    {
                        Nome = UnitTestUtils.GerarString(20),
                        Email = UnitTestUtils.GerarEmail(),
                        Telefone = UnitTestUtils.GerarString(8),
                        DDD = DDDs.SP
                    }
                };

                var command = new AdicionarContatosCommand(contatos);

                var result = await handler.Handle(command, CancellationToken.None);

            }
        }
    }
}
