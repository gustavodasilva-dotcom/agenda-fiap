using System.ComponentModel.DataAnnotations;
using Agenda.Common.Shared.Attributes;

namespace Agenda.Modules.Eventos.Application.Contracts;

public class EventoRequest
{
    [Required(ErrorMessage = "O nome é obrigatório")]
    public string Nome { get; set; } = string.Empty;

    [Required(ErrorMessage = "Data Evento Inicial é obrigatória")]
    public DateTime DataEventoInicio { get; set; }

    [Required(ErrorMessage = "Data Evento Final é obrigatória")]
    public DateTime DataEventoFinal { get; set; }

    [Required(ErrorMessage = "O contato é obrigatório"), MinLength(1, ErrorMessage = "O contato é obrigatório")]
    [ArrayComValoresUnicos<int>(ErrorMessage = "O evento não pode ter contatos repetidos")]
    public int[] ContatosIds { get; set; } = [];
}
