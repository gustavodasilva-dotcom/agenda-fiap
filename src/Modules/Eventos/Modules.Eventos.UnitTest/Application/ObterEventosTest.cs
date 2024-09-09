using Agenda.Modules.Eventos.Application.Eventos.Queries.ObterEventos;
using Agenda.Modules.Eventos.Domain.Abstractions;
using Agenda.Modules.Eventos.Domain.Entities;
using Moq;

namespace Agenda.Modules.Eventos.UnitTests.Application;

public class ObterEventosTest
{
    private readonly Mock<IEventoRepository> _mockEventoRepository = new();

    [Fact]
    public async Task Validar_handler_obterEventos()
    {
        var evento = new List<Evento>()
        {
            Evento.CriarEvento(
                "nome_evento_unit_test",
                DateTime.Now,
                DateTime.Now.AddDays(1)),

            Evento.CriarEvento(
                "nome_evento_unit_test_2",
                DateTime.Now,
                DateTime.Now.AddDays(1))
        };

        _mockEventoRepository.Setup(x => x.ObterTodos()).Returns(evento);

        var handler = new ObterEventosQueryHandler(_mockEventoRepository.Object);

        var resultado = await handler.Handle(new ObterEventosQuery(), default);

        Assert.True(resultado.Any(), "Não listou eventos.");
    }
}
