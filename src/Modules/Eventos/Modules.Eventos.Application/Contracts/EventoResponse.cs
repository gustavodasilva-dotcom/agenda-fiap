namespace Agenda.Modules.Eventos.Application.Contracts
{
    public class EventoResponse
    {
        public int Id { get; set; }

        public string Nome { get; set; } = string.Empty;

        public DateTime DataEventoInicio { get; set; }

        public DateTime DataEventoFinal { get; set; }

        public int[] ContatosIds { get; set; } = [];
    }
}
