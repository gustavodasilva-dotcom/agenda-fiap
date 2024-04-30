using Agenda.FIAP.Api.Application.Contatos.Commands.AdicionarContatos;
using Agenda.FIAP.Api.Application.Contracts.Requests;
using Agenda.FIAP.Api.Domain.Abstractions;
using Agenda.FIAP.Api.Domain.Entities;
using Agenda.FIAP.Api.Domain.Enums;
using Agenda.FIAP.Api.UnitTests.Utils;
using FluentAssertions;
using Moq;

namespace Agenda.FIAP.Api.UnitTests.Application
{
    public class AdicionarContatoTest
    {
        private readonly Mock<IContatoRepository> _mockContatoRepository;
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;

        public AdicionarContatoTest()
        {
            _mockContatoRepository = new();
            _mockUnitOfWork = new();
        }

        [Fact]
        public async Task Validar_handler_adicionar_contatos()
        {
            var contatos = new List<ContatoRequest>()
            {
                new()
                {
                    Nome = UnitTestUtils.GerarString(20),
                    Email = UnitTestUtils.GerarEmail(),
                    Telefone = UnitTestUtils.GerarString(8),
                    DDD = DDD.SP
                }
            };

            var handler = new AdicionarContatosCommandHandler(
                contatoRepository: _mockContatoRepository.Object,
                unitOfWork: _mockUnitOfWork.Object);

            var resultado = await handler.Handle(new AdicionarContatosCommand(contatos), default);

            resultado.Value.Should().NotBeEmpty();
        }

        [Fact]
        public async Task Validar_handler_adicionar_contato_com_email_existente()
        {
            var contatoEntidade =
                Contato.CriarContato(
                    UnitTestUtils.GerarString(20),
                    UnitTestUtils.GerarString(8),
                    UnitTestUtils.GerarEmail(),
                    DDD.SP);

            _mockContatoRepository.Setup(s => s.ContatoExistenteComMesmoEmail(
                    It.Is<string>(x => x == contatoEntidade.Email)))
                .Returns(contatoEntidade);

            var contatos = new List<ContatoRequest>()
            {
                new()
                {
                    Nome = UnitTestUtils.GerarString(20),
                    Email = contatoEntidade.Email,
                    Telefone = UnitTestUtils.GerarString(8),
                    DDD = DDD.SP
                }
            };

            var handler = new AdicionarContatosCommandHandler(
                contatoRepository: _mockContatoRepository.Object,
                unitOfWork: _mockUnitOfWork.Object);

            var resultado = await handler.Handle(new AdicionarContatosCommand(contatos), default);

            Assert.True(resultado.IsFailure, "Adicinou um contato com email existente.");
            Assert.Contains(resultado.Error.Code, "AdicionarContatos.ContatoExistenteComMesmoEmail");
        }

        [Fact]
        public async Task Validar_handler_adicionar_contato_com_telefone_existente()
        {
            var contatoEntidade =
                Contato.CriarContato(
                    UnitTestUtils.GerarString(20),
                    UnitTestUtils.GerarString(8),
                    UnitTestUtils.GerarEmail(),
                    DDD.SP);

            _mockContatoRepository.Setup(s => s.ContatoExistenteComMesmoTelefone(
                    It.Is<string>(x => x == contatoEntidade.Telefone)))
                .Returns(contatoEntidade);

            var contatos = new List<ContatoRequest>()
            {
                new()
                {
                    Nome = UnitTestUtils.GerarString(20),
                    Email = UnitTestUtils.GerarEmail(),
                    Telefone = contatoEntidade.Telefone,
                    DDD = DDD.SP
                }
            };

            var handler = new AdicionarContatosCommandHandler(
                contatoRepository: _mockContatoRepository.Object,
                unitOfWork: _mockUnitOfWork.Object);

            var resultado = await handler.Handle(new AdicionarContatosCommand(contatos), default);

            Assert.True(resultado.IsFailure, "Adicinou um contato com email existente.");
            Assert.Contains(resultado.Error.Code, "AdicionarContatos.ContatoExistenteComMesmoTelefone");
        }
        
        [Fact]
        public async Task Validar_handler_adicionar_contato_repetido()
        {
            var contatoEntidade =
                Contato.CriarContato(
                    UnitTestUtils.GerarString(20),
                    UnitTestUtils.GerarString(8),
                    UnitTestUtils.GerarEmail(),
                    DDD.SP);

            var contatos = new List<ContatoRequest>()
            {
                new()
                {
                    Nome = contatoEntidade.Nome,
                    Email = contatoEntidade.Email,
                    Telefone = contatoEntidade.Telefone,
                    DDD = contatoEntidade.DDD
                },
                new()
                {
                    Nome = contatoEntidade.Nome,
                    Email = contatoEntidade.Email,
                    Telefone = contatoEntidade.Telefone,
                    DDD = contatoEntidade.DDD
                }
            };

            var handler = new AdicionarContatosCommandHandler(
                contatoRepository: _mockContatoRepository.Object,
                unitOfWork: _mockUnitOfWork.Object);

            var resultado = await handler.Handle(new AdicionarContatosCommand(contatos), default);

            Assert.True(resultado.IsFailure, "Adicinou um contato existente.");
            Assert.Contains(resultado.Error.Code, "AdicionarContatos.ContatoRepetido");
        }
    }
}
