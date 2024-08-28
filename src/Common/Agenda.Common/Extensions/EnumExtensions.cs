using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Agenda.Common.Extensions;

public static class EnumExtensions
{
    public static string GetEnumDisplayName<TEnum>(this TEnum @enum) where TEnum : Enum
    {
        return @enum.GetType()
            .GetMember(@enum.ToString())
            .First()
            .GetCustomAttribute<DisplayAttribute>()?
            .GetName() ?? @enum.ToString();
    }
}
