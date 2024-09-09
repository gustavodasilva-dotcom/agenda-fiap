using Agenda.Common.Shared.Abstractions;

namespace Agenda.Modules.Contatos.Domain.DomainEvents;

public sealed record ContatoExcluidoDomainEvent(int ContatoId)
    : IDomainEvent;
