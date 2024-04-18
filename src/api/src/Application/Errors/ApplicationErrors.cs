using Agenda.FIAP.Api.Domain.Shared;

namespace Agenda.FIAP.Api.Application.Errors;

internal static class ApplicationErrors
{
    internal static Error NaoPermitidoCadastrarContatoRepetido = new(
        "AdicionarContato.ContatoRepetido",
        "Não é permitido adicionar contatos repetidos");

    internal static Error ContatoNaoEncontrado = new(
        "Contato.ContatoNaoEncontrado",
        "Não foi encontrado nenhum contato com o id informado");

    internal static Error ContatoExistenteComMesmoTelefone = new(
        "Contato.ContatoExistenteComMesmoTelefone",
        "Já existe um contato cadastro com telefone informado");

    internal static Error ContatoExistenteComMesmoEmail = new(
        "Contato.ContatoExistenteComMesmoEmail",
        "Já existe um contato cadastro com e-mail informado");
}
