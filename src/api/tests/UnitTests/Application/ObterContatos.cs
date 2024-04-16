using Agenda.FIAP.Api.Application.Contatos.Queries.ObterContatos;
using Agenda.FIAP.Api.Domain.Abstractions;
using Agenda.FIAP.Api.Domain.Entities;
using Application.Contracts.Requests;
using Domain.Enum;
using FluentAssertions;
using Moq;
using UnitTests.Utils;

namespace UnitTests.Application
{
    public class ObterContatos
    {
        private readonly Mock<IContatoRepository> _mockContatoRepository;
        public ObterContatos()
        {
            _mockContatoRepository = new Mock<IContatoRepository>();         
        }

        [Fact]
        public async Task Validar_handler_ObterContatos_contatos()
        {
            var contatos = new List<Contato>()
            {
                new(UnitTestUtils.GerarString(20),UnitTestUtils.GerarString(8),UnitTestUtils.GerarEmail(),DDD.SP),
                new(UnitTestUtils.GerarString(20),UnitTestUtils.GerarString(8),UnitTestUtils.GerarEmail(),DDD.SP),
                new(UnitTestUtils.GerarString(20),UnitTestUtils.GerarString(8),UnitTestUtils.GerarEmail(),DDD.MA)
            };

            _mockContatoRepository.Setup(x => x.ObterPorFiltro(It.IsAny<DDD>())).Returns(contatos);

            var filtro = new ContatoFiltroRequest();
            var handler = new ObterContatosQueryHandler(_mockContatoRepository.Object);

            var resultado = await handler.Handle(new ObterContatosQuery(filtro), default);

            resultado.Should().NotBeEmpty();
        }

        [Fact]
        public async Task Validar_filtro_handler_ObterContatos_contatos()
        {
            var contatos = new List<Contato>()
            {
                new(UnitTestUtils.GerarString(20),UnitTestUtils.GerarString(8),UnitTestUtils.GerarEmail(),DDD.MA)
            };

            var filtro = new ContatoFiltroRequest() { Ddd = DDD.MA};
            _mockContatoRepository.Setup(x => x.ObterPorFiltro(It.Is<DDD>(x => x == filtro.Ddd))).Returns(contatos);

            var handler = new ObterContatosQueryHandler(_mockContatoRepository.Object);

            var resultado = await handler.Handle(new ObterContatosQuery(filtro), default);

            resultado.Should().NotBeEmpty();
            Assert.True(resultado.Any(x => x.Ddd == DDD.MA), "Não realizou o filtro.");
            Assert.True(resultado.Count() == 1, "Não filtrou apenas um registro.");
        }
    }
}
