using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Entities;

namespace WebAPI.Models
{
    public class GroupRequest
    {
        [Required]
        public string Title { get; set; }
    }

    public class GroupResponse
    {
        public int GroupId { get; set; }
        public string Title { get; set; }

        public GroupResponse(Group group)
        {
            GroupId = group.GroupId;
            Title = group.Title;
        }
    }

    public class GroupWithCustomersResponse
    {
        public int GroupId { get; set; }
        public string Title { get; set; }
        ICollection<CustomerResponse> Customers { get; set; }

        public GroupWithCustomersResponse(Group group)
        {
            GroupId = group.GroupId;
            Title = group.Title;
            Customers = MapCustomers(group.Customers);
        }

        private List<CustomerResponse> MapCustomers(ICollection<Customer> customers)
        {
            var output = new List<CustomerResponse>();

            foreach (var customer in customers)
            {
                output.Add(new CustomerResponse(customer));
            }
            return output;
        }
    }
}
