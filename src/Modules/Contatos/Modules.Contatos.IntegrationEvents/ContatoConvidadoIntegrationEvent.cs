namespace Agenda.Modules.Contatos.IntegrationEvents;

public sealed record ContatoConvidadoIntegrationEvent(
    string NomeContato, string EmailContato, string NomeEvento, DateTime DataInicioEvento, DateTime DataFinalEvento);
