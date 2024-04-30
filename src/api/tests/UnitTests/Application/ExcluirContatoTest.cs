using Agenda.FIAP.Api.Application.Contatos.Commands.ExcluirContato;
using Agenda.FIAP.Api.Domain.Abstractions;
using Agenda.FIAP.Api.Domain.Entities;
using Agenda.FIAP.Api.Domain.Enums;
using Agenda.FIAP.Api.UnitTests.Utils;
using Moq;
using System.Linq.Expressions;

namespace Agenda.FIAP.Api.UnitTests.Application
{
    public class ExcluirContatoTest
    {
        private readonly Mock<IContatoRepository> _mockContatoRepository;
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;

        public ExcluirContatoTest()
        {
            _mockContatoRepository = new();
            _mockUnitOfWork = new();
        }

        [Fact]
        public async Task Validar_handler_excluir_contato()
        {
            var contatoEntidade =
                Contato.CriarContato(
                    UnitTestUtils.GerarString(20),
                    UnitTestUtils.GerarString(8),
                    UnitTestUtils.GerarEmail(),
                    DDD.SP);

            _mockContatoRepository.Setup(s => s.Obter(It.IsAny<Expression<Func<Contato, bool>>>())).Returns(contatoEntidade);

            {
                var handler = new ExcluirContatoCommandHandler(
                contatoRepository: _mockContatoRepository.Object,
                unitOfWork: _mockUnitOfWork.Object);

                var resultado = await handler.Handle(new ExcluirContatoCommand(0), default);

                Assert.True(resultado.IsSuccess, "Não excluiu um contato.");
            }
        }
        
        [Fact]
        public async Task Validar_handler_excluir_contato_inexistente()
        {
            {
                var handler = new ExcluirContatoCommandHandler(
                contatoRepository: _mockContatoRepository.Object,
                unitOfWork: _mockUnitOfWork.Object);

                var resultado = await handler.Handle(new ExcluirContatoCommand(0), default);

                Assert.True(resultado.IsFailure, "Excluiu um contato existente.");
                Assert.Contains(resultado.Error.Code, "ExcluirContato.ContatoNaoEncontrado");
            }
        }
    }
}
