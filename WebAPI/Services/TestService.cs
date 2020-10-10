using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Entities;
using WebAPI.Helpers;
using WebAPI.Models;

namespace WebAPI.Services
{
    public interface ITestService
    {
        Task ClearDbAsync();
        Task SeedDbAsync();
    }

    public class TestService : ITestService
    {
        private readonly IAddressService _addressService;
        private readonly ICustomerService _customerService;
        private readonly IGroupService _groupService;
        private readonly IJobApplicationService _jobApplicationService;
        private readonly IJobOfferService _jobOfferService;
        private readonly IStudentService _studentService;
        private readonly IUserService _userService;
        private readonly StudentbyContext _context;
        private readonly ILogger<TestService> _logger;

        public TestService(
            IAddressService addressService,
            ICustomerService customerService,
            IGroupService groupService,
            IJobApplicationService jobApplicationService,
            IJobOfferService jobOfferService,
            IStudentService studentService,
            IUserService userService,
            StudentbyContext context,
            ILogger<TestService> logger
            )
        {
            _addressService = addressService;
            _customerService = customerService;
            _groupService = groupService;
            _jobApplicationService = jobApplicationService;
            _jobOfferService = jobOfferService;
            _studentService = studentService;
            _userService = userService;
            _context = context;
            _logger = logger;
        }

        public async Task ClearDbAsync()
        {
            foreach (var ja in _context.JobApplications)
            {
                _context.JobApplications.Remove(ja);
            }

            foreach (var us in _context.Users)
            {
                _context.Users.Remove(us);
            }

            foreach (var st in _context.Students)
            {
                _context.Students.Remove(st);
            }

            foreach (var cu in _context.Customers)
            {
                _context.Customers.Remove(cu);
            }

            foreach (var jo in _context.JobOffers)
            {
                _context.JobOffers.Remove(jo);
            }

            foreach (var gr in _context.Groups)
            {
                _context.Groups.Remove(gr);
            }

            foreach (var ad in _context.Addresses)
            {
                _context.Addresses.Remove(ad);
            }

            await _context.SaveChangesAsync();
        }

        public async Task SeedDbAsync()
        {
            // operator
            await _userService.EnsureOperatorAsync();

            // groups
            await ImportGroupsAsync();

            // customers
            await ImportCustomersAsync();

            // students
            await ImportStudentsAsync();

            // jobOffers
            await ImportJobOffersAsync();
        }

        private async Task ImportGroupsAsync()
        {
            using (var reader = new StreamReader(@".\seedData\groups.csv"))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var vals = line.Split(';');

                    GroupReq newGroup = new GroupReq
                    {
                        Name = vals[0]
                    };
                    await _groupService.CreateAsync(newGroup);
                }
            }
        }

        private async Task ImportCustomersAsync()
        {
            Random rn = new Random();
            var groups = await _context.Groups
                    .ToListAsync();

            using (var reader = new StreamReader(@".\seedData\customers.csv"))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var vals = line.Split(';');

                    int groupId = groups[rn.Next(0, groups.Count)].GroupId;
                    CustomerReq newCustomer = new CustomerReq
                    {
                        Email = vals[0],
                        FirstName = vals[1],
                        LastName = vals[2],
                        GroupId = groupId
                    };
                    await _customerService.CreateAsync(newCustomer);
                }
            }
        }

        private async Task ImportStudentsAsync()
        {
            using (var reader = new StreamReader(@".\seedData\students.csv"))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var vals = line.Split(';');

                    StudentReq newStudent = new StudentReq
                    {
                        Email = vals[0],
                        Password = vals[1],
                        FirstName = vals[2],
                        LastName = vals[3],
                        Address = new AddressReq { Country = "Czechia", City = "Praha", Street = "Nová", Number = "20" },
                        DateOfBirth = new DateTime(1997, 8, 5)
                    };
                    await _studentService.CreateAsync(newStudent);
                }
            }
        }

        private async Task ImportJobOffersAsync()
        {
            Random rn = new Random();           
            var users = await _context.Users
                    .Where(us => us.Role == UserRoles.Customer)
                    .ToListAsync();


            // title, desc, wage, space, year, month, day
            using (var reader = new StreamReader(@".\seedData\jobOffers.csv"))
            {

                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var vals = line.Split(';');

                    JobOfferReq newJobOffer = new JobOfferReq
                    {
                        Title = vals[0],
                        Description = vals[1],
                        Wage = double.Parse(vals[2]),
                        Spaces = int.Parse(vals[3]),
                        Start = new DateTime(int.Parse(vals[4]), int.Parse(vals[5]), int.Parse(vals[6]), 10, 0, 0),
                        End = new DateTime(int.Parse(vals[4]), int.Parse(vals[5]), int.Parse(vals[6]), 18, 0, 0),
                        Address = new AddressReq { Country = "Czechia", City = "Praha", Street = "Nová", Number = "20" }
                    };

                    int userId = users[rn.Next(0, users.Count)].UserId;
                    await _jobOfferService.CreateAsync(newJobOffer, userId);
                }
            }
        }
    }
}
