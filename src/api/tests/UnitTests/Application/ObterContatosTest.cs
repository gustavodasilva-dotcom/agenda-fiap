using Agenda.FIAP.Api.Application.Contatos.Queries.ObterContatos;
using Agenda.FIAP.Api.Domain.Abstractions;
using Agenda.FIAP.Api.Domain.Entities;
using Agenda.FIAP.Api.Domain.Enums;
using Agenda.FIAP.Api.UnitTests.Utils;
using FluentAssertions;
using Moq;

namespace Agenda.FIAP.Api.UnitTests.Application
{
    public class ObterContatosTest
    {
        private readonly Mock<IContatoRepository> _mockContatoRepository;
        public ObterContatosTest()
        {
            _mockContatoRepository = new Mock<IContatoRepository>();         
        }

        [Fact]
        public async Task Validar_handler_ObterContatos_contatos()
        {
            var contatos = new List<Contato>()
            {
                Contato.CriarContato(
                    UnitTestUtils.GerarString(20),
                    UnitTestUtils.GerarString(8),
                    UnitTestUtils.GerarEmail(),
                    DDD.SP),
                Contato.CriarContato(
                    UnitTestUtils.GerarString(20),
                    UnitTestUtils.GerarString(8),
                    UnitTestUtils.GerarEmail(),
                    DDD.SP),
                Contato.CriarContato(
                    UnitTestUtils.GerarString(20),
                    UnitTestUtils.GerarString(8),
                    UnitTestUtils.GerarEmail(),
                    DDD.MA)
            };

            _mockContatoRepository.Setup(x => x.ObterPorFiltro(It.IsAny<DDD>())).Returns(contatos);

            var handler = new ObterContatosQueryHandler(_mockContatoRepository.Object);

            var resultado = await handler.Handle(new ObterContatosQuery(0), default);

            resultado.Should().NotBeEmpty();
        }

        [Fact]
        public async Task Validar_filtro_handler_ObterContatos_contatos()
        {
            var contatos = new List<Contato>()
            {
                Contato.CriarContato(
                    UnitTestUtils.GerarString(20),
                    UnitTestUtils.GerarString(8),
                    UnitTestUtils.GerarEmail(),
                    DDD.MA)
            };

            _mockContatoRepository.Setup(x => x.ObterPorFiltro(It.Is<DDD>(x => x == DDD.MA))).Returns(contatos);

            var handler = new ObterContatosQueryHandler(_mockContatoRepository.Object);

            var resultado = await handler.Handle(new ObterContatosQuery(DDD.MA), default);

            resultado.Should().NotBeEmpty();

            Assert.True(resultado.Any(x => x.DDD == DDD.MA), "Não realizou o filtro.");
            Assert.True(resultado.Count() == 1, "Não filtrou apenas um registro.");
        }
    }
}
