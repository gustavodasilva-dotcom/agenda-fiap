using Agenda.Common.Shared.Abstractions;
using Agenda.Modules.Eventos.Application.Contracts;
using Agenda.Modules.Eventos.Application.Eventos.Commands.AdicionarEvento;
using Agenda.Modules.Eventos.Domain.Abstractions;
using Agenda.Modules.Eventos.Domain.Entities;
using Moq;

namespace Agenda.Modules.Eventos.UnitTests.Application;

public class AdicionarEventoTest
{
    private readonly Mock<IEventoRepository> _mockEventoRepository = new();
    private readonly Mock<IUnitOfWork> _mockUnitOfWork = new();

    [Fact]
    public async Task Validar_handler_adicionar_evento()
    {
        EventoRequest evento = new()
        {
            Nome = "nome_evento_unit_test",
            DataEventoInicio = DateTime.Now.AddDays(1),
            DataEventoFinal = DateTime.Now.AddDays(2),
            ContatosIds = [9]
        };

        var handler = new AdicionarEventoCommandHandler(
            eventoRepository: _mockEventoRepository.Object,
            unitOfWork: _mockUnitOfWork.Object);

        var resultado = await handler.Handle(new AdicionarEventoCommand(evento), default);

        Assert.NotNull(resultado.Value);
        Assert.True(resultado.IsSuccess);
    }
    [Fact]
    public async Task Validar_handler_adicionar_evento_repetido()
    {
        var eventoEntidade =
            Evento.CriarEvento(
                "nome_evento_unit_test",
                DateTime.Now,
                DateTime.Now.AddDays(1));

        eventoEntidade.AdicionarContato(9);

        _mockEventoRepository.Setup(s => s.ObterEventoPorPeriodoEContato(
                It.IsAny<int>(), It.IsAny<DateTime>(), It.IsAny<DateTime>()))
            .Returns(eventoEntidade);

        EventoRequest Eventos = new()
        {
            Nome = "nome_evento_unit_test_2",
            DataEventoInicio = DateTime.Now.AddDays(1),
            DataEventoFinal = DateTime.Now.AddDays(2),
            ContatosIds = [9]
        };

        var handler = new AdicionarEventoCommandHandler(
            eventoRepository: _mockEventoRepository.Object,
            unitOfWork: _mockUnitOfWork.Object);

        var resultado = await handler.Handle(new AdicionarEventoCommand(Eventos), default);

        Assert.True(resultado.IsFailure, "Adicinou um Evento com email existente.");
        Assert.Contains(resultado.Error!.Code, "AdicionarEvento.ExiteEventoMesmoPeriodoParaContato");
    }
}
