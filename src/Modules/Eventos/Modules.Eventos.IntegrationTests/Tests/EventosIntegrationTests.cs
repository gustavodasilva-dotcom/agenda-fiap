using Agenda.Modules.Eventos.Application.Contracts;
using Agenda.Modules.Eventos.IntegrationTests.Mock;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace Agenda.Modules.Eventos.IntegrationTests.Tests
{
    public sealed class EventosIntegrationTests : IAsyncLifetime
    {
        private readonly EventosWebApplicationFactory _application = new();

        private HttpClient? _client = null;

        public async Task InitializeAsync()
        {
            await EventoMockData.CreateEventos(_application, true);
            _client = _application.CreateClient();
        }

        public async Task DisposeAsync()
        {
            await _application.DisposeAsync();
        }

        [Fact]
        public async Task AdicionarEventos_Returns_Created()
        {
            var novoEvento = new EventoRequest
            {
                IdContato = 1,
                Nome = "Novo Evento",
                DataEventoFinal = DateTime.MinValue,
                DataEventoInicio = DateTime.Now
            };

            var jsonEvento = JsonSerializer.Serialize(novoEvento);
            var content = new StringContent(jsonEvento, Encoding.UTF8, "application/json");

            var response = await _client!.PostAsync("/api/eventos", content);

            var responseContent = await response.Content.ReadAsStringAsync();
            if (response.StatusCode != HttpStatusCode.Created)
            {
                throw new Exception($"Unexpected status code: {response.StatusCode}, Response: {responseContent}");
            }

            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }

        [Fact]
        public async Task AlterarEvento_Returns_Ok()
        {
            var eventoAtualizado = new EventoRequest
            {
                IdContato = 1,
                Nome = "Evento Atualizado",
                DataEventoFinal = DateTime.MinValue,
                DataEventoInicio = DateTime.Now
            };

            var jsonEventoAtualizado = JsonSerializer.Serialize(eventoAtualizado);
            var updateContent = new StringContent(jsonEventoAtualizado, Encoding.UTF8, "application/json");
            var putResponse = await _client!.PutAsync($"/api/eventos/{2}", updateContent);

            var responseContent = await putResponse.Content.ReadAsStringAsync();
            if (putResponse.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception($"Unexpected status code: {putResponse.StatusCode}, Response: {responseContent}");
            }

            Assert.Equal(HttpStatusCode.OK, putResponse.StatusCode);
        }

        [Fact]
        public async Task ExcluirEvento_Returns_NoContent()
        {
            var deleteResponse = await _client!.DeleteAsync($"/api/eventos/{1}");

            var responseContent = await deleteResponse.Content.ReadAsStringAsync();
            if (deleteResponse.StatusCode != HttpStatusCode.NoContent)
            {
                throw new Exception($"Unexpected status code: {deleteResponse.StatusCode}, Response: {responseContent}");
            }

            Assert.Equal(HttpStatusCode.NoContent, deleteResponse.StatusCode);

            var getResponse = await _client.GetAsync($"/api/eventos");
            var responseGet = await getResponse.Content.ReadFromJsonAsync<List<EventoResponse>>();
            Assert.False(responseGet?.Any(e => e.Id == 1));
        }

        [Fact]
        public async Task ObterEventosPorDDD_Returns_Ok()
        {
            var getResponse = await _client!.GetAsync($"/api/eventos");

            getResponse.EnsureSuccessStatusCode();
            var result = await getResponse.Content.ReadFromJsonAsync<List<EventoResponse>>();

            Assert.NotNull(result);
        }
    }
}
