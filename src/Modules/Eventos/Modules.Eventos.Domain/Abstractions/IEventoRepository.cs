using Agenda.Modules.Contatos.Domain.Abstractions;
using Agenda.Modules.Eventos.Domain.Entities;

namespace Agenda.Modules.Eventos.Domain.Abstractions;

public interface IEventoRepository : IBaseRepository<Evento>
{
    List<Evento> ObterEventosFuturosDoContato(int contatoId);
    Evento? ObterEventoPorPeriodoEContato(int contatoId, DateTime dataInicio, DateTime dataFinal);
}
