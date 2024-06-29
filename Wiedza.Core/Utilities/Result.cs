namespace Wiedza.Core.Utilities;

public readonly struct Result<TValue>
{
    private readonly TValue? _value;
    private readonly Exception? _exception;

    public bool IsSuccessful { get; }
    public bool IsFailed => !IsSuccessful;
    public TValue Value => _value ?? throw new InvalidOperationException("Value is not set!");
    public Exception Exception => _exception ?? throw new InvalidOperationException("Exception is not set!");

    public Result(TValue value)
    {
        IsSuccessful = true;
        _value = value;
    }

    public Result(Exception exception)
    {
        IsSuccessful = false;
        _exception = exception;
    }

    public static implicit operator Result<TValue>(TValue value) => new(value);
    public static implicit operator Result<TValue>(Exception exception) => new(exception);

    public TResult Match<TResult>(Func<TValue, TResult> ifSuccessful, Func<Exception, TResult> ifFailed)
    {
        return IsSuccessful ? ifSuccessful(Value) : ifFailed(Exception);
    }
}