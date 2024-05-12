using System.ComponentModel.DataAnnotations;

namespace WebUI.Models
{
    public class ContatoModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public int DDD { get; set; }
    }
}
