using System.Net.Http;
using System.Net.Http.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebUI.Models;
using Microsoft.Extensions.Configuration;

namespace WebUI.Services
{
    public class ContatosService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl; 

        public ContatosService(HttpClient httpClient, IConfiguration configuration) 
        {
            _httpClient = httpClient;
            _baseUrl = configuration.GetValue<string>("BaseUrl");
        }

        public async Task<List<ContatoModel>> GetContatosAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<ContatoModel>>($"{_baseUrl}/api/contatos/11");
        }

        public async Task<bool> AdicionarContatoAsync(List<ContatoModel> novosContatos)
        {
            var response = await _httpClient.PostAsJsonAsync($"{_baseUrl}/api/contatos", novosContatos); 

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> ExcluirContatoAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{_baseUrl}/api/contatos/{id}"); 

            return response.IsSuccessStatusCode;
        }
    }
}
