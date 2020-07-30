using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestAPI.Models
{
    public class JobApplicationCreateRequest
    {
        [Required]
        public int JobOfferId { get; set; }
    }
}
