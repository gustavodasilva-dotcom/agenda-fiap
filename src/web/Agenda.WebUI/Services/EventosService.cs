using Agenda.WebUI.Models;
using System.Net.Http.Json;

namespace Agenda.WebUI.Services
{
    public class EventosService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public EventosService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<List<ContatoModel>> GetEventosAsync(int? Ddd)
        {
            var client = _httpClientFactory.CreateClient(HttpClientNames.MyApiContatos);

            var response = await client.GetAsync($"api/eventos/{Ddd}");
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
    }


}
