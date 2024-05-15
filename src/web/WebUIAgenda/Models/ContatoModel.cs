using Agenda.FIAP.Api.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace WebUI.Models
{
    public class ContatoModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Nome é obrigatório")]
        [StringLength(100, ErrorMessage = "Máximo de 100 caracteres")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Telefone é obrigatório")]
        [StringLength(100, ErrorMessage = "Máximo de 100 caracteres")]
        public string Telefone { get; set; }

        [Required(ErrorMessage = "DDD é obrigatório")]
        public DDD? DDD { get; set; }

        [Required(ErrorMessage = "E-mail é obrigatório")]
        [EmailAddress(ErrorMessage = "Endereço de e-mail inválido")]
        public string Email { get; set; }

    }
}
