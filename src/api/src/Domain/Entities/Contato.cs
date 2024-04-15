namespace Agenda.FIAP.Api.Domain.Entities;

public class Contato(
    int id,
    string nome,
    string telefone,
    string email)
{
    private Contato()
        : this(
              id: 0,
              nome: string.Empty,
              telefone: string.Empty,
              email: string.Empty)
    {
    }

    public Contato(
        string nome,
        string telefone,
        string email)
        : this(
              id: 0,
              nome,
              telefone,
              email)
    {
    }

    public int Id { get; private set; } = id;

    public string Nome { get; private set; } = nome;

    public string Telefone { get; private set; } = telefone;

    public string Email { get; private set; } = email;
}
