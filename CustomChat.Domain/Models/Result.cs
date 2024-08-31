using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomChat.Domain.Models
{
    public class Result
    {
        public bool Success { get; }
        public string Message { get; }

        protected Result(bool success, string message)
        {
            Success = success;
            Message = message;
        }

        public static Result Ok(string message = "") => new Result(true, message);
        public static Result<T> Ok<T>(T value, string message = "") => new Result<T>(value, true, message);
        public static Result Error(string message = "") => new Result(false, message);
        public static Result<T> Error<T>(string message = "") => new Result<T>(default!, false, message);
    }

    public class Result<T> : Result
    {
        private readonly T _value;
        public T Value => Success ? _value : throw new AccessViolationException("No se puede acceder a un resultado fallido.");
        protected internal Result(T value, bool success, string message) : base(success, message) => _value = value;

        public static implicit operator Result<T>(T value) => new Result<T>(value, true, "");
    }
}
