using AutoMapper;
using System;

namespace CommonLibrary
{
    public class AppException : Exception
    {
        public string ExceptionCode { get; set; }

        public string ExceptionInfo { get; set; }

        public AppException(Exception exception)
        {
            if (exception != null)
            {
                Mapper.Map(exception, this);
            }
        }
        public AppException(string code)
        {
            ExceptionCode = code;
            ExceptionInfo = AppExceptionCodes.GetCodeMessage(code);
    }
        public AppException()
        {

        }
    }
}
