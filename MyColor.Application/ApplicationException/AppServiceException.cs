using System;

namespace MyColor.Application.ApplicationException
{
    public class AppServiceException : Exception
    {
        public AppServiceException(string error) : base(error)
        { }
    }
}
