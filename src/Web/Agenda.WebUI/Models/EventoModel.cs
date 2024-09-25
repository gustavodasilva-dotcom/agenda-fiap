namespace Agenda.WebUI.Models
{
    public class EventoModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public DateTime DataEventoInicio { get; set; }
        public DateTime DataEventoFinal { get; set; }

        public List<int> ContatosIds { get; set; }
    }
}
