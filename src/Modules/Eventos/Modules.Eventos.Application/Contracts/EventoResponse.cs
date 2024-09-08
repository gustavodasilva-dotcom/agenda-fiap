namespace Agenda.Modules.Eventos.Application.Contracts
{
    public class EventoResponse
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public DateTime DataHora { get; set; }
    }
}
