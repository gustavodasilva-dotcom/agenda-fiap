using AutoMapper;

namespace Agenda.Modules.Eventos.UnitTests.Configurations;

public class EventoTestFixture : IClassFixture<EventoTestFixture>
{
    public IMapper MockMapper;

    public EventoTestFixture()
    {
        var config = new MapperConfiguration(cfg =>
            cfg.AddMaps(Eventos.Application.AssemblyReference.Assembly));

        MockMapper = config.CreateMapper();
    }
}
