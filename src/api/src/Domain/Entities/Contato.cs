namespace Agenda.FIAP.Api.Domain.Entities;

public class Contato(
    int id,
    string nome,
    string telefone,
    string email)
{
    public Contato()
        : this(
              id: 0,
              nome: string.Empty,
              telefone: string.Empty,
              email: string.Empty)
    {
    }

    public int Id { get; set; } = id;

    public string Nome { get; set; } = nome;

    public string Telefone { get; set; } = telefone;

    public string Email { get; set; } = email;
}
