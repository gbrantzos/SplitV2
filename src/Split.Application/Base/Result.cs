using System;

namespace Split.Application.Base
{
    // Ideas found on
    // https://gist.github.com/divyang4481/119e6985cc53c731e63ffa5e9e7d867d
    public abstract class Result
    {
        public bool Failed => !String.IsNullOrEmpty(Message);
        public string Message { get; protected init; }
        public Exception Exception { get; protected init; }

        protected Result(string message) => Message = message;

        protected Result(string message, Exception exception)
        {
            Message = message;
            Exception = exception;
        }

        public static Failure Failure(string message) => new Failure(message);
        public static Failure Failure(string message, Exception exception) => new Failure(message, exception);
        public static Success Success() => new Success();
    }

    public class Success : Result
    {
        public Success() : base(String.Empty)
        {
        }
    }

    public class Failure : Result
    {
        public Failure(string message) : base(message)
        {
        }

        public Failure(string message, Exception exception) : base(message, exception)
        {
        }
    }

    public class Result<TData> : Result
    {
        public TData Data { get; }

        private Result(TData data) : base(String.Empty) => Data = data;

        // Implicit casting
        public static implicit operator Result<TData>(TData data) => new(data);
        public static implicit operator Result<TData>(Failure failure) 
            => new(default)
            {
                Message = failure.Message,
                Exception = failure.Exception
            };
    }
}