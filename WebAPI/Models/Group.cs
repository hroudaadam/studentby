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
        public string Name { get; set; }
    }

    public class GroupResponse
    {
        public int GroupId { get; set; }
        public string Name { get; set; }

        public GroupResponse(Group group)
        {
            GroupId = group.GroupId;
            Name = group.Name;
        }
    }

    public class GroupWithCustomersResponse
    {
        public int GroupId { get; set; }
        public string Name { get; set; }
        public ICollection<CustomerSimpleResponse> Customers { get; set; }

        public GroupWithCustomersResponse(Group group)
        {
            GroupId = group.GroupId;
            Name = group.Name;
            Customers = MapCustomers(group.Customers);
        }

        private List<CustomerSimpleResponse> MapCustomers(ICollection<Customer> customers)
        {
            var output = new List<CustomerSimpleResponse>();

            foreach (var customer in customers)
            {
                output.Add(new CustomerSimpleResponse(customer));
            }
            return output;
        }
    }
}
