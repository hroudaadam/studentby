using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Helpers
{
    public class CustomRequired : RequiredAttribute
    {
        public CustomRequired()
        {
            ErrorMessage = "Pole {0} je povinné";
        }
    }
}
