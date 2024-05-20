using Agenda.Application.Contatos.Commands.ExcluirContato;
using Agenda.Common.Enums;
using Agenda.Domain.Abstractions;
using Agenda.Domain.Entities;
using Agenda.UnitTests.Utils;
using Moq;
using System.Linq.Expressions;

namespace Agenda.UnitTests.Application;

public class ExcluirContatoTest
{
    private readonly Mock<IContatoRepository> _mockContatoRepository;
    private readonly Mock<IUnitOfWork> _mockUnitOfWork;

    public ExcluirContatoTest()
    {
        _mockContatoRepository = new();
        _mockUnitOfWork = new();
    }

    [Fact]
    public async Task Validar_handler_excluir_contato()
    {
        var contatoEntidade =
            Contato.CriarContato(
                UnitTestUtils.GerarString(20),
                UnitTestUtils.GerarString(8),
                UnitTestUtils.GerarEmail(),
                DDDs.SP);

        _mockContatoRepository.Setup(s => s.Obter(It.IsAny<Expression<Func<Contato, bool>>>())).Returns(contatoEntidade);

        {
            var handler = new ExcluirContatoCommandHandler(
            contatoRepository: _mockContatoRepository.Object,
            unitOfWork: _mockUnitOfWork.Object);

            var resultado = await handler.Handle(new ExcluirContatoCommand(0), default);

            Assert.True(resultado.IsSuccess, "Não excluiu um contato.");
        }
    }

    [Fact]
    public async Task Validar_handler_excluir_contato_inexistente()
    {
        {
            var handler = new ExcluirContatoCommandHandler(
            contatoRepository: _mockContatoRepository.Object,
            unitOfWork: _mockUnitOfWork.Object);

            var resultado = await handler.Handle(new ExcluirContatoCommand(0), default);

            Assert.True(resultado.IsFailure, "Excluiu um contato existente.");
            Assert.Contains(resultado.Error!.Code, "ExcluirContato.ContatoNaoEncontrado");
        }
    }
}
