using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portal.Helper
{
    public class AppException: Exception
    {
        public ExceptionReason ExceptionReason { get; set; }
        public AppException(string message): base(message)
        {
        }

        public AppException(string message, Exception innerException): base(message, innerException)
        {
        }

        public AppException()
        {
        }

    }

    public enum ExceptionReason
    {
        LoginFailed = 1000,
    }
}
