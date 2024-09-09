using Agenda.Common.Shared.Abstractions;
using Agenda.Common.Shared.Enums;
using Agenda.Modules.Contatos.Domain.DomainEvents;

namespace Agenda.Modules.Contatos.Domain.Entities;

public class Contato : BaseEntity
{
    private Contato(
        string nome,
        string telefone,
        string email,
        DDDs ddd)
    {
        Nome = nome;
        Telefone = telefone;
        Email = email;
        DDD = ddd;
    }

    private Contato()
        : this(
            nome: string.Empty,
            telefone: string.Empty,
            email: string.Empty,
            DDDs.SP)
    {
    }

    public string Nome { get; private set; }

    public string Telefone { get; private set; }

    public string Email { get; private set; }

    public DDDs DDD { get; private set; }

    public override IEnumerable<object> GetAtomicValues()
        => [Nome, Telefone, Email, DDD];

    public static Contato CriarContato(
        string nome,
        string telefone,
        string email,
        DDDs ddd)
        => new(
            nome.Trim(),
            telefone.Trim(),
            email.Trim(),
            ddd);

    public Contato AtualizarContato(
        string nome,
        string telefone,
        string email,
        DDDs ddd)
    {
        Nome = nome.Trim();
        Telefone = telefone.Trim();
        Email = email.Trim();
        DDD = ddd;

        return this;
    }

    public void RaiseContatoExcluidoDomainEvent()
        => RaiseDomainEvent(new ContatoExcluidoDomainEvent(Id));
}
