using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Helpers
{
    /// <summary>
    /// Exception thrown by logic error
    /// </summary>
    public class AppLogicException : Exception
    {
        public string Detail { get; set; }
        public AppLogicException(string message, string detail=null) : base(message)
        {
            Detail = detail;
        }
    }
}
