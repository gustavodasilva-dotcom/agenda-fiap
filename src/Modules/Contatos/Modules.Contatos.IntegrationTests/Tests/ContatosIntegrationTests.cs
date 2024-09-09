using Agenda.Common.Shared.Enums;
using Agenda.Modules.Contatos.Application.Contracts.Requests;
using Agenda.Modules.Contatos.Application.Contracts.Responses;
using Agenda.Modules.Contatos.IntegrationTests.Mock;
using System.Net.Http.Json;
using System.Net;
using System.Text;
using System.Text.Json;

namespace Agenda.Modules.Contatos.IntegrationTests.Tests;

public sealed class ContatosIntegrationTests : IAsyncLifetime
{
    private readonly ContatosWebApplicationFactory _application = new();
    private HttpClient? _client = null;

    public async Task InitializeAsync()
    {
        await ContatoMockData.CreateContatos(_application, true);
        _client = _application.CreateClient();
    }

    public async Task DisposeAsync()
    {
        await _application.DisposeAsync();
    }

    [Fact]
    public async Task AdicionarContatos_Returns_Created()
    {
        var novoContato = new ContatoRequest
        {
            Nome = "Novo Contato",
            Email = "contato@exemplo.com",
            Telefone = "123456789",
            DDD = DDDs.SP
        };

        var jsonContato = JsonSerializer.Serialize(new List<ContatoRequest> { novoContato });
        var content = new StringContent(jsonContato, Encoding.UTF8, "application/json");

        var response = await _client!.PostAsync("/api/contatos", content);

        var responseContent = await response.Content.ReadAsStringAsync();
        if (response.StatusCode != HttpStatusCode.Created)
        {
            throw new Exception($"Unexpected status code: {response.StatusCode}, Response: {responseContent}");
        }

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
    }

    [Fact]
    public async Task AlterarContato_Returns_Ok()
    {
        var contatoAtualizado = new ContatoRequest
        {
            Nome = "Contato Atualizado",
            Email = "contatoatualizado@exemplo.com",
            Telefone = "987654321",
            DDD = DDDs.SP
        };

        var jsonContatoAtualizado = JsonSerializer.Serialize(contatoAtualizado);
        var updateContent = new StringContent(jsonContatoAtualizado, Encoding.UTF8, "application/json");
        var putResponse = await _client!.PutAsync($"/api/contatos/{2}", updateContent);

        var responseContent = await putResponse.Content.ReadAsStringAsync();
        if (putResponse.StatusCode != HttpStatusCode.OK)
        {
            throw new Exception($"Unexpected status code: {putResponse.StatusCode}, Response: {responseContent}");
        }

        Assert.Equal(HttpStatusCode.OK, putResponse.StatusCode);
    }

    [Fact]
    public async Task ExcluirContato_Returns_NoContent()
    {
        var deleteResponse = await _client!.DeleteAsync($"/api/contatos/{1}");

        var responseContent = await deleteResponse.Content.ReadAsStringAsync();
        if (deleteResponse.StatusCode != HttpStatusCode.NoContent)
        {
            throw new Exception($"Unexpected status code: {deleteResponse.StatusCode}, Response: {responseContent}");
        }

        Assert.Equal(HttpStatusCode.NoContent, deleteResponse.StatusCode);

        var getResponse = await _client.GetAsync($"/api/contatos/{1}");
        Assert.Equal(HttpStatusCode.NoContent, getResponse.StatusCode);
    }

    [Fact]
    public async Task ObterContatosPorDDD_Returns_Ok()
    {
        var ddd = DDDs.SP;
        var getResponse = await _client!.GetAsync($"/api/contatos/{(int)ddd}");

        getResponse.EnsureSuccessStatusCode();
        var result = await getResponse.Content.ReadFromJsonAsync<List<ContatoResponse>>();

        Assert.NotNull(result);
        Assert.Contains(result, c => c.DDD == DDDs.SP);

        foreach (var contato in result)
        {
            Assert.Equal(DDDs.SP, contato.DDD);
        }
    }
}
