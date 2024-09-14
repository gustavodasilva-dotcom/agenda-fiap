using Agenda.WebUI.Models;
using Agenda.WebUI.Services.Responses;
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

        public async Task<List<EventoModel>> GetEventosAsync()
        {
            var client = _httpClientFactory.CreateClient(HttpClientNames.MyApiContatos);

            var response = await client.GetAsync($"api/eventos");
            var contentType = response.Content.Headers.ContentType;

            if (response.IsSuccessStatusCode
                && contentType != null
                && contentType.MediaType == "application/json")
            {
                var jsonString = await response.Content.ReadAsStringAsync();

                return await response.Content.ReadFromJsonAsync<List<EventoModel>>() ?? [];
            }
            else
                return [];
        }

        public async Task<BaseResponse> AdicionarEventoAsync(List<EventoModel> novoEvento)
        {
            var result = new BaseResponse();

            var client = _httpClientFactory.CreateClient(HttpClientNames.MyApiContatos);
            var response = await client.PostAsJsonAsync("api/eventos/", novoEvento);

            if (!response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadFromJsonAsync<BaseResponse>();

                result.FailWithMessage(responseContent!.Message);
            }

            return result;
        }


    }


}
