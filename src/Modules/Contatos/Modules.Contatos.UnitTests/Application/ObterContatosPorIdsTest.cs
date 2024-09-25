using Agenda.Common.Shared.Enums;
using Agenda.Modules.Contatos.Domain.Abstractions;
using Agenda.Modules.Contatos.Domain.Entities;
using Agenda.Modules.Contatos.UnitTests.Utils;
using FluentAssertions;
using Modules.Contatos.Application.Contatos.Queries.ObterContatosPorIdsQuery;
using Moq;

namespace Modules.Contatos.UnitTests.Application
{
    public class ObterContatosPorIdsTest
    {
        private readonly Mock<IContatoRepository> _mockContatoRepository = new();

        [Fact]
        public async Task Validar_handler_ObterContatosPorIds() {
            var contatos = new List<Contato>()
            {
            Contato.CriarContato(
                UnitTestUtils.GerarString(20),
                UnitTestUtils.GerarString(8),
                UnitTestUtils.GerarEmail(),
                DDDs.SP),
            Contato.CriarContato(
                UnitTestUtils.GerarString(20),
                UnitTestUtils.GerarString(8),
                UnitTestUtils.GerarEmail(),
                DDDs.SP),
            Contato.CriarContato(
                UnitTestUtils.GerarString(20),
                UnitTestUtils.GerarString(8),
                UnitTestUtils.GerarEmail(),
                DDDs.MA)
        };

            _mockContatoRepository.Setup(repo => repo.ObterPorFiltro(It.IsAny<int[]>())).Returns(contatos);

            var handler = new ObterContatosPorIdsQueryHandler(_mockContatoRepository.Object);

            var resultado = await handler.Handle(new ObterContatosPorIdsQuery(new List<int> { 1 }), default);

            resultado.Should().NotBeEmpty();
        }
    }
}
