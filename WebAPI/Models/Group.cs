using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Entities;

namespace WebAPI.Models
{
    public class GroupRequest
    {
        public string Title { get; set; }
    }

    public class GroupResponse
    {
        public string Title { get; set; }

        public GroupResponse(Group group)
        {
            Title = group.Title;
        }
    }
}
