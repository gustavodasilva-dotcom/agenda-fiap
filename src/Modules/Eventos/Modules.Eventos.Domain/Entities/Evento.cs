using Agenda.Common.Shared;
using Agenda.Common.Shared.Abstractions;

namespace Agenda.Modules.Eventos.Domain.Entities;

public class Evento : BaseEntity
{
    private readonly HashSet<EventoContato> _contatos = [];

    private Evento(string nome, DateTime dataEventoInicio, DateTime dataEventoFinal)
    {
        Nome = nome;
        DataEventoInicio = dataEventoInicio;
        DataEventoFinal = dataEventoFinal;
    }

    private Evento()
        : this(nome: string.Empty, dataEventoInicio: DateTime.MinValue, dataEventoFinal: DateTime.MaxValue)
    {
    }

    public string Nome { get; private set; }

    public DateTime DataEventoInicio { get; private set; }

    public DateTime DataEventoFinal { get; private set; }

    public IReadOnlySet<EventoContato> Contatos
        => _contatos;

    public override IEnumerable<object> GetAtomicValues()
        => [Nome, DataEventoInicio, DataEventoFinal];

    public static Evento CriarEvento(string nome, DateTime dataEventoInicio, DateTime dataEventoFinal)
        => new(nome.Trim(), dataEventoInicio, dataEventoFinal);

    public Evento AtualizarEvento(string nome, DateTime dataEventoInicio, DateTime dataEventoFinal)
    {
        Nome = nome;
        DataEventoInicio = dataEventoInicio;
        DataEventoFinal = dataEventoFinal;

        return this;
    }

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
