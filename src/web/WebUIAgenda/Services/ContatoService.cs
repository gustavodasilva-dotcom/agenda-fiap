using System.Net.Http.Json;
using WebUI.Models;

namespace WebUI.Services
{
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

        public async Task<bool> AdicionarContatoAsync(List<ContatoModel> novosContatos)
        {
            var client = _httpClientFactory.CreateClient(HttpClientNames.MyApiContatos);

            var response = await client.PostAsJsonAsync("api/contatos/", novosContatos);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> ExcluirContatoAsync(int id)
        {
            var client = _httpClientFactory.CreateClient("AgendaApi");

            var response = await client.DeleteAsync($"https://localhost:44336/api/contatos/{id}");

            return response.IsSuccessStatusCode;
        }
    }
}
