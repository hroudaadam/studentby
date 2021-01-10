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
        public AppLogicException(string message) : base(message)
        {
        }
    }
}
