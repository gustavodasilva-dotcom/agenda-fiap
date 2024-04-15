using Agenda.FIAP.Api.Domain.Abstractions;
using Moq;

namespace UnitTests.Application
{
    public class AdicionarContatos
    {
        private readonly Mock<IContatoRepository> _mockContatoRepository;
        public AdicionarContatos()
        {
            _mockContatoRepository = new();
        }

        //todo: adicionar teste apos validar entrada de contatos
        [Fact]
        public async Task Validar_handler_adicionar_contatos()
        {

        }
    }
}
