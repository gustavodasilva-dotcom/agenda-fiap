using Agenda.Application.Contatos.Queries.ObterContatos;
using Agenda.Common.Enums;
using Agenda.Domain.Abstractions;
using Agenda.Domain.Entities;
using Agenda.UnitTests.Utils;
using FluentAssertions;
using Moq;

namespace Agenda.UnitTests.Application;

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

        _mockContatoRepository.Setup(x => x.ObterPorFiltro(It.IsAny<DDDs>())).Returns(contatos);

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
                DDDs.MA)
        };

        _mockContatoRepository.Setup(x => x.ObterPorFiltro(It.Is<DDDs>(x => x == DDDs.MA))).Returns(contatos);

        var handler = new ObterContatosQueryHandler(_mockContatoRepository.Object);

        var resultado = await handler.Handle(new ObterContatosQuery(DDDs.MA), default);

        resultado.Should().NotBeEmpty();

        Assert.True(resultado.Any(x => x.DDD == DDDs.MA), "Não realizou o filtro.");
        Assert.True(resultado.Count() == 1, "Não filtrou apenas um registro.");
    }
}
