using Agenda.Common.Shared.Abstractions;
using Agenda.Modules.Eventos.Application.Eventos.Commands.ExcluirEvento;
using Agenda.Modules.Eventos.Domain.Abstractions;
using Agenda.Modules.Eventos.Domain.Entities;
using Moq;
using System.Linq.Expressions;

namespace Agenda.Modules.Eventos.UnitTests.Application;

public class ExcluirEventoTest
{
    private readonly Mock<IEventoRepository> _mockEventoRepository = new();
    private readonly Mock<IUnitOfWork> _mockUnitOfWork = new();

    [Fact]
    public async Task Validar_handler_excluir_evento()
    {
        var eventoEntidade =
            Evento.CriarEvento(
                "nome_evento_unit_test",
                DateTime.Now,
                DateTime.Now.AddDays(1));

        _mockEventoRepository.Setup(s => s.Obter(It.IsAny<Expression<Func<Evento, bool>>>())).Returns(eventoEntidade);

        {
            var handler = new ExcluirEventoCommandHandler(
            eventoRepository: _mockEventoRepository.Object,
            unitOfWork: _mockUnitOfWork.Object);

            var resultado = await handler.Handle(new ExcluirEventoCommand(0), default);

            Assert.True(resultado.IsSuccess, "Não excluiu um Evento.");
        }
    }

    [Fact]
    public async Task Validar_handler_excluir_Evento_inexistente()
    {
        {
            var handler = new ExcluirEventoCommandHandler(
            eventoRepository: _mockEventoRepository.Object,
            unitOfWork: _mockUnitOfWork.Object);

            var resultado = await handler.Handle(new ExcluirEventoCommand(0), default);

            Assert.True(resultado.IsFailure, "Excluiu um Evento existente.");
            Assert.Contains(resultado.Error!.Code, "ExcluirEvento.EventoNaoEncontrado");
        }
    }
}
