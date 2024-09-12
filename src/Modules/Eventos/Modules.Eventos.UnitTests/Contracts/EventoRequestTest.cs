using Agenda.Modules.Eventos.Application.Contracts;
using System.ComponentModel.DataAnnotations;

namespace Agenda.Modules.Eventos.UnitTests.Contracts;

public class EventoRequestTest
{
    private static EventoRequest CriarEvento() => new()
    {
        Nome = "nome_evento_unit_test",
        DataEventoFinal = DateTime.Now.AddDays(1),
        DataEventoInicio = DateTime.Now,
        ContatosIds = [3]
    };

    [Fact]
    public void Validar_nome_evento()
    {
        var evento = CriarEvento();
        evento.Nome = string.Empty;

        var resultados = new List<ValidationResult>();
        var valido = Validator.TryValidateObject(evento, new ValidationContext(evento), resultados, true);

        Assert.False(valido);
        Assert.True(resultados.Count == 1 && resultados.Any(x => x.ErrorMessage!.Contains("NOME", StringComparison.OrdinalIgnoreCase)));
    }

    [Fact]
    public void Inclusao_com_sucesso_contatos()
    {
        var evento = CriarEvento();

        var resultados = new List<ValidationResult>();
        var valido = Validator.TryValidateObject(evento, new ValidationContext(evento), resultados, true);

        Assert.True(valido);
        Assert.True(resultados.Count == 0);
    }
}
