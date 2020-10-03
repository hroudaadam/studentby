using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Helpers
{
    /// <summary>
    /// Exception thrown by logic error
    /// </summary>
    public class StudentbyException : Exception
    {
        public StudentbyException(string message) : base(message)
        {
        }
    }
}
