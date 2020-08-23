using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestAPI.Entities;

namespace TestAPI.Models
{
    public class JobApplicationEmployeeResponse
    {
        public string StudentFirstName { get; set; }
        public string StudentLastName { get; set; }

        public JobApplicationEmployeeResponse(JobApplication jobApplication)
        {
            StudentFirstName = jobApplication.Student.FirstName;
            StudentLastName = jobApplication.Student.LastName;
        }
    }
}
