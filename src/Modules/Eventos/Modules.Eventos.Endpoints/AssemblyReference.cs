using System.Reflection;

namespace Modules.Eventos.Endpoints;

public static partial class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}
