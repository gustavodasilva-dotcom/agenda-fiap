using System.Reflection;

namespace Agenda.Common.Helpers.Extensions;

public static partial class AssemblyExtensions
{
    public static List<TInterface> GetTypesFromAssemblies<TInterface>(
        this Assembly[] assemblies) where TInterface : class
    {
        static bool IsAssignableToType<T>(TypeInfo typeInfo) =>
            typeof(T).IsAssignableFrom(typeInfo) &&
            !typeInfo.IsInterface &&
            !typeInfo.IsAbstract;

        var assemblyTypes = assemblies
            .SelectMany(a => a.DefinedTypes)
            .Where(IsAssignableToType<TInterface>)
            .Select(Activator.CreateInstance)
            .Cast<TInterface>()
            .ToList();

        return assemblyTypes;
    }
}
