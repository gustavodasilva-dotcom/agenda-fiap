using Agenda.Common.Enums;
using Agenda.Modules.Contatos.Application.Contracts.Requests;
using Agenda.Modules.Contatos.UnitTests.Utils;
using System.ComponentModel.DataAnnotations;

namespace Agenda.Modules.Contatos.UnitTests.Contracts;

public class ContatoRequestTest
{
    private static ContatoRequest CriarContato() => new()
    {
        Nome = UnitTestUtils.GerarString(20),
        Email = UnitTestUtils.GerarEmail(),
        Telefone = UnitTestUtils.GerarString(8),
        DDD = DDDs.SP
    };

    [Fact]
    public void Validar_nome_contato()
    {
        var contato = CriarContato();
        contato.Nome = string.Empty;

        var resultados = new List<ValidationResult>();
        var valido = Validator.TryValidateObject(contato,new ValidationContext(contato), resultados,true);

        Assert.False(valido);
        Assert.True(resultados.Count == 1 && resultados.Any(x => x.ErrorMessage!.Contains("NOME", StringComparison.OrdinalIgnoreCase)));
    }

    [Fact]
    public void Validar_telefone_contato()
    {
        var contato = CriarContato();
        contato.Telefone = string.Empty;

        var resultados = new List<ValidationResult>();
        var valido = Validator.TryValidateObject(contato, new ValidationContext(contato), resultados, true);

        Assert.False(valido);
        Assert.True(resultados.Count == 1 && resultados.Any(x => x.ErrorMessage!.Contains("TELEFONE", StringComparison.OrdinalIgnoreCase)));
    }

    [Fact]
    public void Validar_email_contato()
    {
        var contato = CriarContato();
        contato.Email = string.Empty;

        var resultados = new List<ValidationResult>();
        var valido = Validator.TryValidateObject(contato, new ValidationContext(contato), resultados,true);

        Assert.False(valido);
        Assert.True(resultados.Count == 1 && resultados.Any(x => x.ErrorMessage!.Contains("E-MAIL", StringComparison.OrdinalIgnoreCase)));
    }

    [Fact]
    public void Validar_formato_email_contato()
    {
        var contato = CriarContato();
        contato.Email = UnitTestUtils.GerarString(10);

        var resultados = new List<ValidationResult>();
        var valido = Validator.TryValidateObject(contato, new ValidationContext(contato), resultados,true);

        Assert.False(valido);
        Assert.True(resultados.Count == 1 && resultados.Any(x => x.ErrorMessage!.Contains("E-MAIL", StringComparison.OrdinalIgnoreCase)));
    }

    [Fact]
    public void Validar_ddd_contato()
    {
        var contato = CriarContato();
        contato.DDD = 0;

        var resultados = new List<ValidationResult>();
        var valido = Validator.TryValidateObject(contato, new ValidationContext(contato), resultados, true);

        Assert.False(valido);
        Assert.True(resultados.Count == 1 && resultados.Any(x => x.ErrorMessage!.Contains("DDD", StringComparison.OrdinalIgnoreCase)));
    }
    
    [Fact]
    public void Inclusao_com_sucesso_contatos()
    {
        var contato = CriarContato();

        var resultados = new List<ValidationResult>();
        var valido = Validator.TryValidateObject(contato, new ValidationContext(contato), resultados, true);

        Assert.True(valido);
        Assert.True(resultados.Count == 0);
    }
    
    [Fact]
    public void Validar_telefone_tamanho_contato()
    {
        var contato = CriarContato();
        contato.Telefone = UnitTestUtils.GerarString(10);

        var resultados = new List<ValidationResult>();
        var valido = Validator.TryValidateObject(contato, new ValidationContext(contato), resultados, true);

        Assert.False(valido);
        Assert.True(resultados.Count == 1 && resultados.Any(x => x.ErrorMessage!.Contains("TELEFONE", StringComparison.OrdinalIgnoreCase)));
    }
}
