using System.ComponentModel.DataAnnotations;

namespace Agenda.Modules.Eventos.Application.Contracts
{
    public class EventoRequest
    {
        [Range(1, int.MaxValue, ErrorMessage = "O contato é obrigatório")]
        public int IdContato { get; set; }
        [Required(ErrorMessage = "O nome é obrigatório")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "Data Evento Inicial é obrigatória")]
        public DateTime DataEventoInicio { get; set; }
        [Required(ErrorMessage = "Data Evento Final é obrigatória")]
        public DateTime DataEventoFinal { get; set; }
    }
}
