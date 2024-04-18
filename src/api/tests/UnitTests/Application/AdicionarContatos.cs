using Agenda.FIAP.Api.Application.Contatos.Commands.AdicionarContatos;
using Agenda.FIAP.Api.Application.Contracts.Requests;
using Agenda.FIAP.Api.Domain.Abstractions;
using Agenda.FIAP.Api.Domain.Enums;
using Agenda.FIAP.Api.UnitTests.Utils;
using FluentAssertions;
using Moq;

namespace Agenda.FIAP.Api.UnitTests.Application
{
    public class AdicionarContatos
    {
        private readonly Mock<IContatoRepository> _mockContatoRepository;
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;

        public AdicionarContatos()
        {
            _mockContatoRepository = new();
            _mockUnitOfWork = new();
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
                    DDD = DDD.SP
                }
            };
            
            var handler = new AdicionarContatosCommandHandler(
                contatoRepository: _mockContatoRepository.Object,
                unitOfWork: _mockUnitOfWork.Object);

            var resultado = await handler.Handle(new AdicionarContatosCommand(contatos), default);

            resultado.Value.Should().NotBeEmpty();
        }
    }
}
