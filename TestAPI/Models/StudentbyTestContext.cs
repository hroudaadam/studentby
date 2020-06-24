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
    }
}
