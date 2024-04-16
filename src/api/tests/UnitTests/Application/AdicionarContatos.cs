using Agenda.FIAP.Api.Domain.Abstractions;
using Moq;
using Agenda.FIAP.Api.Application.Contatos.Commands.AdicionarContatos;
using Agenda.FIAP.Api.Application.Contracts.Requests;
using Domain.Enum;
using UnitTests.Utils;
using FluentAssertions;

namespace UnitTests.Application
{
    public class AdicionarContatos
    {
        private readonly Mock<IContatoRepository> _mockContatoRepository;
        public AdicionarContatos()
        {
            _mockContatoRepository = new();
        }

        [Fact]
        public async Task Validar_handler_adicionar_contatos()
        {
            var contatos = new List<ContatoRequest>()
            {
                new()
                {
                    Nome = UnitTestUtils.GerarString(20),
                    Email = UnitTestUtils.GerarEmail(),
                    Telefone = UnitTestUtils.GerarString(8),
                    Ddd = DDD.SP
                }
            };
            var handler = new AdicionarContatosCommandHandler(_mockContatoRepository.Object);

            var resultado = await handler.Handle(new AdicionarContatosCommand(contatos), default);

            resultado.Should().NotBeEmpty();
        }
    }
}
