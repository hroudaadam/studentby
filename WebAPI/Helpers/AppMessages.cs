using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Helpers
{
    static public class AppMessages
    {
        static public string Required(string field)
        {
            return $"Pole {field} je povinné.";
        }

        static public string MinLenght(string field, int lenght)
        {
            return $"Minimální délka pole {field} je {lenght}.";
        }

        static public string Email(string field)
        {
            return $"Pole {field} není ve formátu emailové adresy.";
        }
    }
}
