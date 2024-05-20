using Agenda.Application.Contatos.Commands.AlterarContato;
using Agenda.Application.Contracts.Requests;
using Agenda.Common.Enums;
using Agenda.Domain.Abstractions;
using Agenda.Domain.Entities;
using Agenda.UnitTests.Utils;
using Moq;
using System.Linq.Expressions;

namespace Agenda.UnitTests.Application;

public class AlterarContatoTest
{
    private readonly Mock<IContatoRepository> _mockContatoRepository;
    private readonly Mock<IUnitOfWork> _mockUnitOfWork;

    public AlterarContatoTest()
    {
        _mockContatoRepository = new();
        _mockUnitOfWork = new();
    }

    [Fact]
    public async Task Validar_handler_alterar_contato()
    {
        var contatoEntidade =
            Contato.CriarContato(
                UnitTestUtils.GerarString(20),
                UnitTestUtils.GerarString(8),
                UnitTestUtils.GerarEmail(),
                DDDs.SP);

        _mockContatoRepository.Setup(s => s.Obter(It.IsAny<Expression<Func<Contato, bool>>>())).Returns(contatoEntidade);

        var contatos = new ContatoRequest()
        {
            Nome = UnitTestUtils.GerarString(20),
            Email = UnitTestUtils.GerarEmail(),
            Telefone = UnitTestUtils.GerarString(8),
            DDD = DDDs.SP
        };

        {
            var handler = new AlterarContatoCommandHandler(
            contatoRepository: _mockContatoRepository.Object,
            unitOfWork: _mockUnitOfWork.Object);

            var resultado = await handler.Handle(new AlterarContatoCommand(1, contatos), default);

            Assert.True(resultado.IsSuccess, "Não alterou um contato.");
        }
    }

    [Fact]
    public async Task Validar_handler_alterar_contato_mesmo_telefone()
    {
        var contatoEntidade =
            Contato.CriarContato(
                UnitTestUtils.GerarString(20),
                UnitTestUtils.GerarString(8),
                UnitTestUtils.GerarEmail(),
                DDDs.SP);

        _mockContatoRepository.Setup(s => s.ContatoExistenteComMesmoTelefone(
                It.Is<string>(x => x == contatoEntidade.Telefone)))
            .Returns(contatoEntidade);

        var contatos = new ContatoRequest()
        {
            Nome = UnitTestUtils.GerarString(20),
            Email = UnitTestUtils.GerarEmail(),
            Telefone = contatoEntidade.Telefone,
            DDD = DDDs.SP
        };

        {
            var handler = new AlterarContatoCommandHandler(
            contatoRepository: _mockContatoRepository.Object,
            unitOfWork: _mockUnitOfWork.Object);

            var resultado = await handler.Handle(new AlterarContatoCommand(2, contatos), default);

            Assert.True(resultado.IsFailure, "Alterou um contato com o mesmo telefone.");
            Assert.Contains(resultado.Error!.Code, "AlterarContato.ContatoExistenteComMesmoTelefone");
        }
    }

    [Fact]
    public async Task Validar_handler_alterar_contato_mesmo_email()
    {
        var contatoEntidade =
            Contato.CriarContato(
                UnitTestUtils.GerarString(20),
                UnitTestUtils.GerarString(8),
                UnitTestUtils.GerarEmail(),
                DDDs.SP);

        _mockContatoRepository.Setup(s => s.ContatoExistenteComMesmoEmail(
                It.Is<string>(x => x == contatoEntidade.Email)))
            .Returns(contatoEntidade);

        var contatos = new ContatoRequest()
        {
            Nome = UnitTestUtils.GerarString(20),
            Email = contatoEntidade.Email,
            Telefone = UnitTestUtils.GerarString(8),
            DDD = DDDs.SP
        };

        {
            var handler = new AlterarContatoCommandHandler(
            contatoRepository: _mockContatoRepository.Object,
            unitOfWork: _mockUnitOfWork.Object);

            var resultado = await handler.Handle(new AlterarContatoCommand(2, contatos), default);

            Assert.True(resultado.IsFailure, "Alterou um contato com mesmo email.");
            Assert.Contains(resultado.Error!.Code, "AlterarContato.ContatoExistenteComMesmoEmail");
        }
    }

    [Fact]
    public async Task Validar_handler_alterar_contato_inexistente()
    {
        var contatos = new ContatoRequest()
        {
            Nome = UnitTestUtils.GerarString(20),
            Email = UnitTestUtils.GerarEmail(),
            Telefone = UnitTestUtils.GerarString(8),
            DDD = DDDs.SP
        };

        {
            var handler = new AlterarContatoCommandHandler(
            contatoRepository: _mockContatoRepository.Object,
            unitOfWork: _mockUnitOfWork.Object);

            var resultado = await handler.Handle(new AlterarContatoCommand(1, contatos), default);

            Assert.True(resultado.IsFailure, "encontrou um contato inexistente.");
            Assert.Contains(resultado.Error!.Code, "AlterarContato.ContatoNaoEncontrado");
        }
    }
}
