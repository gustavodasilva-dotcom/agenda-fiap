using System.Linq.Expressions;
using Agenda.Common.Shared.Repositories;
using Agenda.Modules.Eventos.Domain.Abstractions;
using Agenda.Modules.Eventos.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Agenda.Modules.Eventos.Persistence.Repositories;

public sealed class EventoRepository(EventosDbContext dbContext)
    : BaseRepository<EventosDbContext, Evento>(dbContext), IEventoRepository
{
    private readonly EventosDbContext _dbContext = dbContext;

    public override Evento? Obter(Expression<Func<Evento, bool>> filtro)
        => _dbContext.Eventos.Include(e => e.Contatos)
            .SingleOrDefault(filtro);

    public Evento? ObterEventoPorPeriodoEContato(int contatoId, DateTime dataInicio, DateTime dataFinal)
        => _dbContext.Eventos.Include(e => e.Contatos)
            .FirstOrDefault(e =>
                e.DataEventoInicio >= dataInicio &&
                e.DataEventoFinal <= dataFinal &&
                e.Contatos.Any(c => c.ContatoId == contatoId));

    public List<Evento> ObterEventosFuturosDoContato(int contatoId)
        => [.. _dbContext.Eventos.Include(e => e.Contatos)
            .Where(e =>
                e.DataEventoInicio > DateTime.Now &&
                e.Contatos.Any(c =>
                    c.ContatoId == contatoId))];
}
