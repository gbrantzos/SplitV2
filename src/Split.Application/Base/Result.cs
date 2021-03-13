using System;

namespace Split.Application.Base
{
    public abstract class Result
    {
        public bool Failed => !String.IsNullOrEmpty(Message);
        public string Message { get; protected set; }

        protected Result(string message) => Message = message;
        
        public static Failure Failure(string message) => new Failure(message);
        public static Success Success() => new Success();
    }

    public class Success : Result
    {
        public Success() : base(String.Empty) { }
    }
    
    public class Failure : Result
    {
        public Failure(string message) : base(message) { }
    }

    public class Result<TData> : Result
    {
        public TData Data { get; }

        private Result(TData data) : base(String.Empty) => Data = data;

        // Implicit casting
        public static implicit operator Result<TData>(TData data) => new(data);
        public static implicit operator Result<TData>(Failure failure) => new(default) {Message = failure.Message};
    }
}