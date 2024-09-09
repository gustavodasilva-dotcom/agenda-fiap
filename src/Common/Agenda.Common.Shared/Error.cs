namespace Agenda.Common.Shared;

public record Error(string Code, string? Message = null)
{
    public static readonly Error None = new(string.Empty);
}
