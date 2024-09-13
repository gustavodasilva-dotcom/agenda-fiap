using System.ComponentModel.DataAnnotations;

namespace Agenda.Common.Shared.Attributes;

public class ArrayComValoresUnicosAttribute<TType> : ValidationAttribute
{
    public override bool IsValid(object? value)
    {
        var array = (TType[]?)value;
        if (value is null || array?.Length == 0)
        {
            return false;
        }
        return !array!.GroupBy(x => x).Where(x => x.Count() > 1).Any();
    }
}
