using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Entities;
using WebAPI.Models;

namespace WebAPI.Services
{
    public interface IGroupService
    {
        Task<GroupResponse> CreateAsync(GroupRequest model);
        Task<IEnumerable<GroupResponse>> GetAllAsync();
        Task<GroupWithCustomersResponse> GetAsync(int groupId);
    }

    public class GroupService: IGroupService
    {
        private readonly StudentbyContext _context;

        public GroupService(StudentbyContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<GroupResponse>> GetAllAsync()
        {
            var groups = await _context.Groups.ToListAsync();

            var response = new List<GroupResponse>();
            foreach (var group in groups)
            {
                response.Add(new GroupResponse(group));
            }

            return response;
        }

        public async Task<GroupWithCustomersResponse> GetAsync(int groupId)
        {
            var group = await _context.Groups
                .Include(gr => gr.Customers)
                    .ThenInclude(cu => cu.User)
                .FirstOrDefaultAsync(gr => gr.GroupId == groupId);

            if (group == null)
            {
                return null;
            }

            return new GroupWithCustomersResponse(group);
        }

        public async Task<GroupResponse> CreateAsync(GroupRequest model)
        {
            var group = new Group
            {
                Title = model.Title
            };
            _context.Groups.Add(group);
            await _context.SaveChangesAsync();

            return new GroupResponse(group);
        }
    }
}
