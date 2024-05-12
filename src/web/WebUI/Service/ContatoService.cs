using System.Net.Http;
using System.Net.Http.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebUI.Models;

namespace WebUI.Services
{
    public class ContatosService
    {
        private readonly HttpClient _httpClient;

        public ContatosService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<ContatoModel>> GetContatosAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<ContatoModel>>("https://localhost:44336/api/contatos/11");
        }

        public async Task<bool> AdicionarContatoAsync(List<ContatoModel> novosContatos)
        {
            var response = await _httpClient.PostAsJsonAsync("https://localhost:44336/api/contatos", novosContatos);

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
            var response = await _httpClient.DeleteAsync($"https://localhost:44336/api/contatos/{id}");

            return response.IsSuccessStatusCode;
        }
    }
}
