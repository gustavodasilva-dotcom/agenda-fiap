using Agenda.FIAP.Api.Domain.Enums;

namespace Agenda.FIAP.Api.Domain.Entities;

public class Contato
{
    private Contato()
    {
    }

    private Contato(
        string nome,
        string telefone,
        string email,
        DDD ddd)
    {
        Nome = nome;
        Telefone = telefone;
        Email = email;
        DDD = ddd;
    }

    private Contato(
        int id,
        string nome,
        string telefone,
        string email,
        DDD ddd)
        : this(
            nome,
            telefone,
            email,
            ddd)
    {
        Id = id;
    }

    public int Id { get; private set; }

    public string Nome { get; private set; }

    public string Telefone { get; private set; }

    public string Email { get; private set; }

    public DDD DDD { get; private set; }

    public static Contato CriarContato(
        string nome,
        string telefone,
        string email,
        DDD ddd)
    {
        var contato = new Contato(
            nome.Trim(),
            telefone.Trim(),
            email.Trim(),
            ddd);

        return contato;
    }

    public static Contato AtualizarContato(
        int id,
        string nome,
        string telefone,
        string email,
        DDD ddd)
    {
        var contato = new Contato(
            id,
            nome.Trim(),
            telefone.Trim(),
            email.Trim(),
            ddd);

        return contato;
    }
}
