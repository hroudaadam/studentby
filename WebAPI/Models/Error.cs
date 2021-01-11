using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public class ErrorRes
    {
        public string Error { get; set; }
        public string Detail { get; set; }

        public ErrorRes(string error, string detail=null)
        {
            Error = error;
            Detail = detail;
        }
    }
}
