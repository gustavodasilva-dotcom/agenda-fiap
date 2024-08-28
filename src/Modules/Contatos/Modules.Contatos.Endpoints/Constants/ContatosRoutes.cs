namespace Agenda.Modules.Contatos.Endpoints.Constants;

internal static partial class ContatosRoutes
{
    public const string ObterContatos = "api/contatos/{ddd:int}";

    public const string AdicionarContatos = "api/contatos";

    public const string AlterarContatos = "api/contatos/{id:int}";

    public const string ExcluirContato = "api/contatos/{id:int}";
}
