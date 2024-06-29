using Agenda.Application.Contatos.Commands.AdicionarContatos;
using Agenda.Application.Contatos.Commands.AlterarContato;
using Agenda.Application.Contatos.Commands.ExcluirContato;
using Agenda.Application.Contatos.Queries.ObterContatos;
using Agenda.Application.Contracts.Requests;
using Agenda.Common.Enums;
using Agenda.Domain.Entities;
using Agenda.Infrastructure.Data.Context;
using Agenda.Infrastructure.Data.Repositories;
using Agenda.UnitTests.Utils;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Agenda.UnitTests.Integration
{
    public class AgendaFiapIntegrationTests
    {
        private readonly DbContextOptions<DataContext> _dbContextOptions;

        public AgendaFiapIntegrationTests() {
            // Configuração do banco de dados em memória
            _dbContextOptions = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "AgendaFiapTest")
                .Options;

            // Inicializa o banco de dados em memória
            using (var context = new DataContext(_dbContextOptions)) {
                context.Database.EnsureCreated();
            }
        }

        [Fact]
        public async Task TestAdicionarContatosCommand() {
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

                await handler.Handle(command, CancellationToken.None);

                // Verificação dos dados no banco de dados em memória
                var addedContact = await context.Contato.FirstOrDefaultAsync();
                Assert.NotNull(addedContact);
                Assert.Equal(contatos[0].Nome, addedContact.Nome);
                Assert.Equal(contatos[0].Email, addedContact.Email);
                Assert.Equal(contatos[0].Telefone, addedContact.Telefone);
                Assert.Equal(contatos[0].DDD, addedContact.DDD);
            }
        }

        [Fact]
        public async Task TestAlterarContatoCommandHandler() {
            // Arrange
            using (var context = new DataContext(_dbContextOptions)) {
                var contatoRepository = new ContatoRepository(context);
                var unitOfWork = new UnitOfWork(context);
                var handler = new AlterarContatoCommandHandler(contatoRepository, unitOfWork);

                // Dados de teste
                var contatoExistente = Contato.CriarContato (
                    "Nome Antigo",
                    "email@antigo.com",
                    "12345678",
                    DDDs.SP
                );

                context.Contato.Add(contatoExistente);
                await context.SaveChangesAsync();

                var contatoRequest = new ContatoRequest {
                    Nome = "Nome Novo",
                    Email = "email@novo.com",
                    Telefone = "87654321",
                    DDD = DDDs.RJ
                };

                int id = 1;

                var command = new AlterarContatoCommand (
                     id,
                     contatoRequest
                );

                // Act
                var result = await handler.Handle(command, CancellationToken.None);

                // Assert
                Assert.False(result.IsFailure, result.Error?.Message);

                var contatoAtualizado = await context.Contato.FindAsync(1);
                Assert.NotNull(contatoAtualizado);
                Assert.Equal(contatoRequest.Nome, contatoAtualizado.Nome);
                Assert.Equal(contatoRequest.Email, contatoAtualizado.Email);
                Assert.Equal(contatoRequest.Telefone, contatoAtualizado.Telefone);
                Assert.Equal(contatoRequest.DDD, contatoAtualizado.DDD);
            }
        }

        [Fact]
        public async Task TestObterContatosQueryHandler() {
            // Arrange
            using (var context = new DataContext(_dbContextOptions)) {
                var contatoRepository = new ContatoRepository(context);
                var handler = new ObterContatosQueryHandler(contatoRepository);

                // Dados de teste
                var contatosExistentes = new List<Contato>
                {
                    Contato.CriarContato ("Contato 1", "contato1@example.com", "12345678", DDDs.SP),
                    Contato.CriarContato ("Contato 2", "contato2@example.com", "87654321", DDDs.RJ),
                    Contato.CriarContato ("Contato 3", "contato3@example.com", "11223344", DDDs.SP)
                };

                context.Contato.AddRange(contatosExistentes);
                await context.SaveChangesAsync();

                var query = new ObterContatosQuery (DDDs.SP);

                // Act
                var result = await handler.Handle(query, CancellationToken.None);

                // Assert
                Assert.NotNull(result);
                Assert.Equal(2, result.Count());
                Assert.All(result, r => Assert.Equal(DDDs.SP, r.DDD));
            }
        }

        [Fact]
        public async Task TestExcluirContatoCommandHandler() {
            // Arrange
            using (var context = new DataContext(_dbContextOptions)) {
                var contatoRepository = new ContatoRepository(context);
                var unitOfWork = new UnitOfWork(context);
                var handler = new ExcluirContatoCommandHandler(contatoRepository, unitOfWork);

                // Dados de teste
                var contatoExistente = Contato.CriarContato (
                    "Contato para Excluir",
                    "contato@excluir.com",
                    "12345678",
                    DDDs.SP
                );

                context.Contato.Add(contatoExistente);
                await context.SaveChangesAsync();

                var command = new ExcluirContatoCommand (1);

                // Act
                var result = await handler.Handle(command, CancellationToken.None);

                // Assert
                Assert.False(result.IsFailure, result.Error?.Message);

                var contatoExcluido = await context.Contato.FindAsync(1);
                Assert.Null(contatoExcluido);
            }
        }
    }
}


