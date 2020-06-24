using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestAPI.Models
{
    public class StudentbyTestContext: DbContext
    {
        public StudentbyTestContext(DbContextOptions<StudentbyTestContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Operator> Operators { get; set; }
        public DbSet<Registration> Registrations { get; set; }
        public DbSet<JobOffer> JobOffers { get; set; }
    }
}
