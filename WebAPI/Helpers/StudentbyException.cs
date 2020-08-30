using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Helpers
{
    public class StudentbyException : Exception
    {
        public StudentbyException(string message) : base(message)
        {
        }
    }
}
