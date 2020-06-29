using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestAPI.Helpers
{
    public class AppException : Exception
    {
        public AppException(string message) : base(message)
        {
        }
    }
}
