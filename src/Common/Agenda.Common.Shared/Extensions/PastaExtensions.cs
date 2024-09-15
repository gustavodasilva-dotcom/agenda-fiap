namespace Agenda.Common.Shared.Extensions;

public static class PastaExtensions
{
    public static string ConcatenarCaminho(this string caminhoBase, string[] caminhos)
    {
        string concatenados = Path.Combine(caminhos);
        string caminhoCompleto = Path.Combine(caminhoBase, concatenados);
        if (!Directory.Exists(caminhoCompleto))
        {
            Directory.CreateDirectory(caminhoCompleto);
        }
        return caminhoCompleto;
    }

    public static string ConcatenarCaminho(this string caminhoBase, string pasta)
        => ConcatenarCaminho(caminhoBase, [pasta]);
}
