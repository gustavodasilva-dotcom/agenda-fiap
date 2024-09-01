using Agenda.Common.Shared;
using Agenda.Common.Shared.Abstractions;

namespace Agenda.Modules.Eventos.Domain.Entities;

public class Evento : BaseEntity
{
    private readonly HashSet<EventoContato> _contatos = [];

    private Evento(string nome, DateTime dataEvento)
    {
        Nome = nome;
        DataEvento = dataEvento;
    }

    private Evento()
        : this(nome: string.Empty, dataEvento: DateTime.MaxValue)
    {
    }

    public string Nome { get; private set; }

    public DateTime DataEvento { get; private set; }

    public IReadOnlySet<EventoContato> Contatos
        => _contatos;

    public override IEnumerable<object> GetAtomicValues()
        => [Nome, DataEvento];

    public static Evento CriarEvento(string nome)
        => new(nome.Trim(), DateTime.Now);

    public Result AdicionarContato(int contatoId)
    {
        var contato = _contatos.SingleOrDefault(c => c.ContatoId == contatoId);
        if (contato is not null)
        {
            return new Error(
                "AdicionarContato.ContatoRepetido",
                "Contato já existente na lista do evento");
        }

        _contatos.Add(EventoContato.CriarContato(Id, contatoId));

        return Result.Success();
    }

    public void RemoverContato(int contatoId)
    {
        var contato = _contatos.SingleOrDefault(c => c.ContatoId == contatoId);
        if (contato is not null)
        {
            _contatos.Remove(contato);
        }
    }
}
