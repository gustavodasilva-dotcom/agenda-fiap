using Agenda.Common.Shared.Abstractions;

namespace Agenda.Modules.Eventos.Domain.DomainEvents;

public sealed record ContatoAdicionadoAoEventoDomainEvent(
    int ContatoId, int EventoId, string NomeEvento, DateTime DataInicioEvento, DateTime DataFinalEvento) : IDomainEvent;
