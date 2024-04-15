namespace UnitTests.Utils
{
    public static class UnitTestUtils
    {
        public static string GerarString(int tamanho)
        {
            var caracteres = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            var resultado = new string(
                Enumerable.Repeat(caracteres, tamanho)
                          .Select(s => s[random.Next(s.Length)])
                          .ToArray());
            return resultado;
        }

        public static string GerarEmail() => $"{GerarString(5)}@{GerarString(5)}";
    }
}
