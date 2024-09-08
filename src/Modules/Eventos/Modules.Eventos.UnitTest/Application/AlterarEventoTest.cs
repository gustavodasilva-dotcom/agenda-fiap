using Agenda.Common.Shared.Abstractions;
using Agenda.Modules.Eventos.Application.Contracts;
using Agenda.Modules.Eventos.Application.Eventos.Commands.AlterarEvento;
using Agenda.Modules.Eventos.Domain.Abstractions;
using Agenda.Modules.Eventos.Domain.Entities;
using Moq;
using System.Linq.Expressions;

namespace Agenda.Modules.Eventos.UnitTests.Application;

public class AlterarEventoTest
{
    private readonly Mock<IEventoRepository> _mockEventoRepository = new();
    private readonly Mock<IUnitOfWork> _mockUnitOfWork = new();

    [Fact]
    public async Task Validar_handler_alterar_evento()
    {
        var eventoEntidade =
            Evento.CriarEvento(
                "nome_evento_unit_test",
                DateTime.Now,
                DateTime.Now.AddDays(1));

        _mockEventoRepository.Setup(s => s.Obter(It.IsAny<Expression<Func<Evento, bool>>>())).Returns(eventoEntidade);

        var evento = new EventoRequest()
        {
            Nome = "nome_evento_unit_test_2",
            DataEventoInicio = DateTime.Now.AddDays(1),
            DataEventoFinal = DateTime.Now.AddDays(2),
            IdContato = 9
        };

        {
            var handler = new AlterarEventoCommandHandler(
            eventoRepository: _mockEventoRepository.Object,
            unitOfWork: _mockUnitOfWork.Object);

            var resultado = await handler.Handle(new AlterarEventoCommand(1, evento), default);

            Assert.True(resultado.IsSuccess, "Não alterou um Evento.");
        }
    }

    [Fact]
    public async Task Validar_handler_alterar_evento_repetido()
    {
        var eventoEntidade =
            Evento.CriarEvento(
                "nome_evento_unit_test",
                DateTime.Now,
                DateTime.Now.AddDays(1));

        eventoEntidade.AdicionarContato(9);

        _mockEventoRepository.Setup(s => s.ObterEventoPorPeriodoEContato(
                It.IsAny<int>(),It.IsAny<DateTime>(), It.IsAny<DateTime>()))
            .Returns(eventoEntidade);

        var eventos = new EventoRequest()
        {
            Nome = "nome_evento_unit_test_2",
            DataEventoInicio = DateTime.Now,
            DataEventoFinal = DateTime.Now.AddDays(1),
            IdContato = 9
        };

        {
            var handler = new AlterarEventoCommandHandler(
            eventoRepository: _mockEventoRepository.Object,
            unitOfWork: _mockUnitOfWork.Object);

            var resultado = await handler.Handle(new AlterarEventoCommand(2, eventos), default);

            Assert.True(resultado.IsFailure, "Alterou um Evento com mesmo email.");
            Assert.Contains(resultado.Error!.Code, "AlterarEvento.ExiteEventoMesmoPeriodoParaContato");
        }
    }

    [Fact]
    public async Task Validar_handler_alterar_evento_inexistente()
    {
        var evento = new EventoRequest()
        {
            Nome = "nome_evento_unit_test_2",
            DataEventoInicio = DateTime.Now.AddDays(1),
            DataEventoFinal = DateTime.Now.AddDays(2),
            IdContato = 9
        };

        {
            var handler = new AlterarEventoCommandHandler(
            eventoRepository: _mockEventoRepository.Object,
            unitOfWork: _mockUnitOfWork.Object);

            var resultado = await handler.Handle(new AlterarEventoCommand(1, evento), default);

            Assert.True(resultado.IsFailure, "encontrou um Evento inexistente.");
            Assert.Contains(resultado.Error!.Code, "AlterarEvento.EventoNaoEncontrado");
        }
    }
}
