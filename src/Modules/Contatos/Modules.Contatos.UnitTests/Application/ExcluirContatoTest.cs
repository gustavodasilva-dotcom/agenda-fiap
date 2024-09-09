using Agenda.Common.Shared.Abstractions;
using Agenda.Common.Shared.Enums;
using Agenda.Modules.Contatos.Application.Contatos.Commands.ExcluirContato;
using Agenda.Modules.Contatos.Domain.Abstractions;
using Agenda.Modules.Contatos.Domain.Entities;
using Agenda.Modules.Contatos.UnitTests.Utils;
using Moq;
using System.Linq.Expressions;

namespace Agenda.Modules.Contatos.UnitTests.Application;

public class ExcluirContatoTest
{
    private readonly Mock<IContatoRepository> _mockContatoRepository = new();
    private readonly Mock<IUnitOfWork> _mockUnitOfWork = new();

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
