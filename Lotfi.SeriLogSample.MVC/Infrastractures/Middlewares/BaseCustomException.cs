using System;

namespace Lotfi.SeriLogSample.MVC.Infrastractures.Middlewares
{
    public class BaseCustomException : Exception
    {
        private int _code;
        private string _description;

        public int Code
        {
            get => _code;
        }
        public string Description
        {
            get => _description;
        }

        public BaseCustomException(string message, string description, int code) : base(message)
        {
            _code = code;
            _description = description;
        }
    }
    public class NotFoundCustomException : BaseCustomException
    {
        public NotFoundCustomException(string message, string description, int code) : base(message, description, code)
        {
        }
    }
}