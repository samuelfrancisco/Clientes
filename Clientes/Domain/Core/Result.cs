using System;
using System.Collections.Generic;
using System.Linq;

namespace Clientes.Domain.Core
{
    [Serializable]
    public class Result
    {
        public Result()
        {

        }

        public Result(Result result)
        {
            if (result == null)
                return;

            ErroMessage.Message = result.ErroMessage.Message;            

            foreach (var error in result.ErroMessage.Errors)
            {
                ErroMessage.Errors.Add(error);
            }
        }

        public ErrorMessage ErroMessage { get; set; } = new ErrorMessage();
        public bool IsSuccess { get { return !IsFailure; } }
        public bool IsFailure { get { return ErroMessage.Errors.Any(); } }

        public void AddError(string message, string reason, string domain)
        {
            ErroMessage.Errors.Add(new ErrorDescription { Message = message, Reason = reason, Domain = domain });
        }

        public void Merge(Result result)
        {
            if (result == null)
                return;

            foreach (var error in result.ErroMessage.Errors)
            {
                ErroMessage.Errors.Add(error);
            }
        }

        public Result<T> ToResult<T>()
        {
            return new Result<T>(default(T), this);
        }

        public Result<T> ToResult<T>(T value)
        {
            return new Result<T>(value, this);
        }
    }

    [Serializable]
    public class Result<T> : Result
    {
        public Result(T value)
        {
            Value = value;
        }

        public Result(T value, Result result)
            : base(result)
        {
            Value = value;
        }
        public T Value { get; set; }
    }

    [Serializable]
    public class ErrorMessage
    {        
        public string Message { get; set; }
        public ICollection<ErrorDescription> Errors { get; set; }


        public ErrorMessage()
        {
            Errors = new HashSet<ErrorDescription>();
        }

        public ErrorMessage( string message)
            : this()
        {            
            Message = message;
        }
    }

    [Serializable]
    public class ErrorDescription
    {
        public string Domain { get; set; }
        public string Reason { get; set; }
        public string Message { get; set; }
    }
}
