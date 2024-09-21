namespace Agenda.Modules.Eventos.IntegrationEvents;

public sealed record ContatoAdicionadoAoEventoIntegrationEvent(
    int ContatoId, int EventoId, string NomeEvento, DateTime DataInicioEvento, DateTime DataFinalEvento);
