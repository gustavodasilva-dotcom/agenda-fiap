using System.ComponentModel.DataAnnotations;

namespace WebUI.Models
{
    public class ContatoModel
    {
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        public string Telefone { get; set; }

        [Required]
        public int DDD { get; set; }

        public string Email { get; set; }

    }
}
