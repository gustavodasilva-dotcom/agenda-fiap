using System.Net.Http.Json;
using Agenda.Common.Shared.Enums;
using Agenda.Common.Shared.Extensions;
using Agenda.WebUI.Models;
using Agenda.WebUI.Services.Responses;

namespace Agenda.WebUI.Services;

public class ContatosService
{
    private readonly IHttpClientFactory _httpClientFactory;

    public ContatosService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<List<ContatoModel>> GetContatosAsync(int? Ddd)
    {
        var client = _httpClientFactory.CreateClient(HttpClientNames.MyApiContatos);

        var response = await client.GetAsync($"api/contatos/{Ddd}");
        var contentType = response.Content.Headers.ContentType;

        if (response.IsSuccessStatusCode
            && contentType != null
            && contentType.MediaType == "application/json")
        {
            var jsonString = await response.Content.ReadAsStringAsync();

            return await response.Content.ReadFromJsonAsync<List<ContatoModel>>() ?? [];
        }
        else
            return [];
    }

    public async Task<BaseResponse> AdicionarContatoAsync(List<ContatoModel> novosContatos)
    {
        var result = new BaseResponse();

        var client = _httpClientFactory.CreateClient(HttpClientNames.MyApiContatos);
        var response = await client.PostAsJsonAsync("api/contatos/", novosContatos);

        if (!response.IsSuccessStatusCode)
        {
            var responseContent = await response.Content.ReadFromJsonAsync<BaseResponse>();

            result.FailWithMessage(responseContent!.Message);
        }

        return result;
    }

    public async Task<BaseResponse> AlterarContatoAsync(ContatoModel contato)
    {
        var result = new BaseResponse();

        var client = _httpClientFactory.CreateClient(HttpClientNames.MyApiContatos);
        var response = await client.PutAsJsonAsync($"api/contatos/{contato.Id}", contato);

        if (!response.IsSuccessStatusCode)
        {
            var responseContent = await response.Content.ReadFromJsonAsync<BaseResponse>();

            result.FailWithMessage(responseContent!.Message);
        }

        return result;
    }

    public async Task<BaseResponse> ExcluirContatoAsync(int id)
    {
        var result = new BaseResponse();

        var client = _httpClientFactory.CreateClient(HttpClientNames.MyApiContatos);
        var response = await client.DeleteAsync($"api/contatos/{id}");

        if (!response.IsSuccessStatusCode)
        {
            var responseContent = await response.Content.ReadFromJsonAsync<BaseResponse>();

            result.FailWithMessage(responseContent!.Message);
        }

        return result;
    }

    public List<DDDModel> GetDdds()
    {
        var result = new List<DDDModel>();

        foreach (DDDs enumValue in Enum.GetValues(typeof(DDDs)))
        {
            result.Add(new DDDModel((int)enumValue, enumValue.GetEnumDisplayName()));
        }

        return result;
    }
}
