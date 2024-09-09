using System.Reflection;

namespace Agenda.Modules.Eventos.CrossCutting.DependencyInjection;

public static partial class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}
