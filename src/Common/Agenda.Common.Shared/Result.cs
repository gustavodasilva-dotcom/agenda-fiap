namespace Agenda.Common.Shared;

public class Result
{
    protected readonly bool _isSuccess;

    public readonly Error? Error;

    protected Result()
    {
        _isSuccess = true;
        Error = default;
    }

    protected Result(Error error)
    {
        _isSuccess = false;
        Error = error;
    }

    public bool IsSuccess => _isSuccess;

    public bool IsFailure => !_isSuccess;

    public static implicit operator Result(Error error)
        => new(error);

    public static Result Success() => new();

    public static Result Failure(Error error)
        => new(error);
}

public sealed class Result<TValue, TError> : Result
    where TError : Error
{
    public readonly TValue? Value;

    private Result(TValue value) : base()
        => Value = value;

    private Result(TError error) : base(error)
        => Value = default;

    public static implicit operator Result<TValue, TError>(TValue value)
        => new(value);

    public static implicit operator Result<TValue, TError>(TError error)
        => new(error);

    public Result<TValue, TError> Match(
        Func<TValue, Result<TValue, TError>> success,
        Func<Error, Result<TValue, TError>> failure)
    {
        if (_isSuccess)
        {
            return success(Value!);
        }
        return failure(Error!);
    }
}
