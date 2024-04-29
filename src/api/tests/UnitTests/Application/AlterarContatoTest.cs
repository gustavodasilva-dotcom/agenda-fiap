using Agenda.FIAP.Api.Application.Contatos.Commands.AlterarContato;
using Agenda.FIAP.Api.Application.Contracts.Requests;
using Agenda.FIAP.Api.Domain.Abstractions;
using Agenda.FIAP.Api.Domain.Entities;
using Agenda.FIAP.Api.Domain.Enums;
using Agenda.FIAP.Api.UnitTests.Utils;
using Moq;
using System.Linq.Expressions;

namespace UnitTests.Application
{
    public class AlterarContatoTest
    {
        private readonly Mock<IContatoRepository> _mockContatoRepository;
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;

        public AlterarContatoTest()
        {
            _mockContatoRepository = new();
            _mockUnitOfWork = new();
        }

        [Fact]
        public async Task Validar_handler_alterar_contato()
        {
            var contatoEntidade =
                new Contato(
                    UnitTestUtils.GerarString(20),
                    UnitTestUtils.GerarString(8),
                    UnitTestUtils.GerarEmail(),
                    DDD.SP);

            _mockContatoRepository.Setup(s => s.Obter(It.IsAny<Expression<Func<Contato, bool>>>())).Returns(contatoEntidade);

            var contatos = new ContatoRequest()
            {
                Nome = UnitTestUtils.GerarString(20),
                Email = UnitTestUtils.GerarEmail(),
                Telefone = UnitTestUtils.GerarString(8),
                DDD = DDD.SP
            };

            {
                var handler = new AlterarContatoCommandHandler(
                contatoRepository: _mockContatoRepository.Object,
                unitOfWork: _mockUnitOfWork.Object);

                var resultado = await handler.Handle(new AlterarContatoCommand(1, contatos), default);

                Assert.True(resultado.IsSuccess, "Não alterou um contato.");
            }
        }

        [Fact]
        public async Task Validar_handler_alterar_contato_mesmo_telefone()
        {
            var contatoEntidade =
                new Contato(
                    UnitTestUtils.GerarString(20),
                    UnitTestUtils.GerarString(8),
                    UnitTestUtils.GerarEmail(),
                    DDD.SP,
                    1);

            _mockContatoRepository.Setup(s => s.ContatoExistenteComMesmoTelefone(
                    It.Is<string>(x => x == contatoEntidade.Telefone)))
                .Returns(contatoEntidade);

            var contatos = new ContatoRequest()
            {
                Nome = UnitTestUtils.GerarString(20),
                Email = UnitTestUtils.GerarEmail(),
                Telefone = contatoEntidade.Telefone,
                DDD = DDD.SP
            };

            {
                var handler = new AlterarContatoCommandHandler(
                contatoRepository: _mockContatoRepository.Object,
                unitOfWork: _mockUnitOfWork.Object);

                var resultado = await handler.Handle(new AlterarContatoCommand(2, contatos), default);

                Assert.True(resultado.IsFailure, "Alterou um contato com o mesmo telefone.");
                Assert.Contains(resultado.Error.Code, "AlterarContato.ContatoExistenteComMesmoTelefone");
            }
        }

        [Fact]
        public async Task Validar_handler_alterar_contato_mesmo_email()
        {
            var contatoEntidade =
                new Contato(
                    UnitTestUtils.GerarString(20),
                    UnitTestUtils.GerarString(8),
                    UnitTestUtils.GerarEmail(),
                    DDD.SP,
                    1);

            _mockContatoRepository.Setup(s => s.ContatoExistenteComMesmoEmail(
                    It.Is<string>(x => x == contatoEntidade.Email)))
                .Returns(contatoEntidade);

            var contatos = new ContatoRequest()
            {
                Nome = UnitTestUtils.GerarString(20),
                Email = contatoEntidade.Email,
                Telefone = UnitTestUtils.GerarString(8),
                DDD = DDD.SP
            };

            {
                var handler = new AlterarContatoCommandHandler(
                contatoRepository: _mockContatoRepository.Object,
                unitOfWork: _mockUnitOfWork.Object);

                var resultado = await handler.Handle(new AlterarContatoCommand(2, contatos), default);

                Assert.True(resultado.IsFailure, "Alterou um contato com mesmo email.");
                Assert.Contains(resultado.Error.Code, "AlterarContato.ContatoExistenteComMesmoEmail");
            }
        }

        [Fact]
        public async Task Validar_handler_alterar_contato_inexistente()
        {
            var contatos = new ContatoRequest()
            {
                Nome = UnitTestUtils.GerarString(20),
                Email = UnitTestUtils.GerarEmail(),
                Telefone = UnitTestUtils.GerarString(8),
                DDD = DDD.SP
            };

            {
                var handler = new AlterarContatoCommandHandler(
                contatoRepository: _mockContatoRepository.Object,
                unitOfWork: _mockUnitOfWork.Object);

                var resultado = await handler.Handle(new AlterarContatoCommand(1, contatos), default);

                Assert.True(resultado.IsFailure, "encontrou um contato inexistente.");
                Assert.Contains(resultado.Error.Code, "AlterarContato.ContatoNaoEncontrado");
            }
        }
    }
}
