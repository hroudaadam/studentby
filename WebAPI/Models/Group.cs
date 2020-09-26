using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Entities;

namespace WebAPI.Models
{
    /// <summary>
    /// Request: Group
    /// </summary>
    public class GroupReq
    {
        [Required]
        public string Name { get; set; }
    }

    /// <summary>
    /// Request: Group with Id
    /// </summary>
    public class GroupWithIdReq
    {
        [Required]
        public int GroupId { get; set; }

        [Required]
        public string Name { get; set; }
    }

    /// <summary>
    /// Response: Group
    /// </summary>
    public class GroupRes
    {
        public int GroupId { get; set; }
        public string Name { get; set; }

        public GroupRes(Group group)
        {
            GroupId = group.GroupId;
            Name = group.Name;
        }
    }

    /// <summary>
    /// Response: Group with Customers
    /// </summary>
    public class GroupWithCustsRes
    {
        public int GroupId { get; set; }
        public string Name { get; set; }
        public ICollection<CustomerSimpleResponse> Customers { get; set; }

        public GroupWithCustsRes(Group group)
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
