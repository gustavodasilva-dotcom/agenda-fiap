namespace Agenda.Common.Shared.Abstractions;

public abstract class BaseEntity : DomainEvent, IEquatable<BaseEntity>
{
    protected BaseEntity()
    {
    }

    protected BaseEntity(int id)
    {
        Id = id;
    }

    public int Id { get; private set; }

    public abstract IEnumerable<object> GetAtomicValues();

    private bool ValuesAreEqual(BaseEntity other)
        => GetAtomicValues().SequenceEqual(other.GetAtomicValues());

    public override bool Equals(object? obj)
        => obj is BaseEntity other && ValuesAreEqual(other);

    public override int GetHashCode()
        => GetAtomicValues()
            .Aggregate(
                default(int),
                HashCode.Combine);

    public bool Equals(BaseEntity? other)
        => other is not null && ValuesAreEqual(other);
}
