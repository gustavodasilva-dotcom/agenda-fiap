using Agenda.Application.Contracts.Requests;
using Agenda.Application.Contracts.Responses;
using Agenda.Common.Enums;
using System.Net.Http.Json;
using System.Net;
using System.Text;
using System.Text.Json;
using Agenda.IntegrationTests.Mock;
using Agenda.IntegrationTests;

namespace Agenda.Tests.IntegrationTests;

public sealed class AgendaFiapIntegrationTests
{
    [Fact]
    public async Task AdicionarContatos_Returns_Created()
    {
        await using var application = new CustomWebApplicationFactory();

        var client = application.CreateClient();

        var novoContato = new ContatoRequest
        {
            Nome = "Novo Contato",
            Email = "contato@exemplo.com",
            Telefone = "123456789",
            DDD = DDDs.SP
        };

        var jsonContato = JsonSerializer.Serialize(new List<ContatoRequest> { novoContato });
        var content = new StringContent(jsonContato, Encoding.UTF8, "application/json");

        var response = await client.PostAsync("/api/contatos", content);

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
        await using var application = new CustomWebApplicationFactory();

        await ContatoMockData.CreateContatos(application, true);
        var client = application.CreateClient();

        var contatoAtualizado = new ContatoRequest
        {
            Nome = "Contato Atualizado",
            Email = "contatoatualizado@exemplo.com",
            Telefone = "987654321",
            DDD = DDDs.SP
        };

        var jsonContatoAtualizado = JsonSerializer.Serialize(contatoAtualizado);
        var updateContent = new StringContent(jsonContatoAtualizado, Encoding.UTF8, "application/json");

        var putResponse = await client.PutAsync($"/api/contatos/{1}", updateContent);

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
        await using var application = new CustomWebApplicationFactory();

        await ContatoMockData.CreateContatos(application, true);
        var client = application.CreateClient();

        var deleteResponse = await client.DeleteAsync($"/api/contatos/{1}");

        var responseContent = await deleteResponse.Content.ReadAsStringAsync();
        if (deleteResponse.StatusCode != HttpStatusCode.NoContent)
        {
            throw new Exception($"Unexpected status code: {deleteResponse.StatusCode}, Response: {responseContent}");
        }

        Assert.Equal(HttpStatusCode.NoContent, deleteResponse.StatusCode);

        var getResponse = await client.GetAsync($"/api/contatos/{1}");
        Assert.Equal(HttpStatusCode.NoContent, getResponse.StatusCode);
    }

    [Fact]
    public async Task ObterContatosPorDDD_Returns_Ok()
    {
        await using var application = new CustomWebApplicationFactory();

        await ContatoMockData.CreateContatos(application, true);
        var client = application.CreateClient();

        var ddd = DDDs.SP;
        var getResponse = await client.GetAsync($"/api/contatos/{(int)ddd}");

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
