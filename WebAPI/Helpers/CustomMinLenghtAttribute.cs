using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Helpers
{
    public class CustomMinLength : MinLengthAttribute
    {
        public CustomMinLength(int length) : base(length)
        {
            ErrorMessage = "Minimální délka pole {0} je " + length;
        }
    }
}
