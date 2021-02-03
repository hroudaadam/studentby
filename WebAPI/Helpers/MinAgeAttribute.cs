using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Helpers
{
    public class MinAgeAttribute : ValidationAttribute
    {
        private int _minimumAge;
        public MinAgeAttribute(int minimumAge)
        {
            _minimumAge = minimumAge;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {           
            if (value != null)
            {
                DateTime birthDate = (DateTime)value;
                if (birthDate.AddYears(_minimumAge) < DateTime.UtcNow) 
                {
                    return ValidationResult.Success;
                }
            }
            return new ValidationResult($"Minimum age is {_minimumAge}");
        }
    }
}
