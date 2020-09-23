using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Entities;
using WebAPI.Models;
using WebAPI.Helpers;

namespace WebAPI.Services
{
    public interface IGroupService
    {
        Task<GroupResponse> CreateAsync(GroupRequest model);
        Task<IEnumerable<GroupResponse>> GetListAsync();
        Task<GroupDetailWithCustomersResponse> GetDetailAsync(int groupId);
        Task<bool> EditAsync(GroupWithIdRequest model, int groupId);
    }

    public class GroupService: IGroupService
    {
        private readonly StudentbyContext _context;

        public GroupService(StudentbyContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<GroupResponse>> GetListAsync()
        {
            var groups = await _context.Groups.ToListAsync();

            var response = new List<GroupResponse>();
            foreach (var group in groups)
            {
                response.Add(new GroupResponse(group));
            }

            return response;
        }

        public async Task<GroupDetailWithCustomersResponse> GetDetailAsync(int groupId)
        {
            var group = await _context.Groups
                .Include(gr => gr.Customers)
                    .ThenInclude(cu => cu.User)
                .FirstOrDefaultAsync(gr => gr.GroupId == groupId);

            if (group == null)
            {
                return null;
            }

            return new GroupDetailWithCustomersResponse(group);
        }

        public async Task<GroupResponse> CreateAsync(GroupRequest model)
        {
            var group = new Group
            {
                Name = model.Name
            };
            _context.Groups.Add(group);
            await _context.SaveChangesAsync();

            return new GroupResponse(group);
        }

        public async Task<bool> EditAsync(GroupWithIdRequest model, int groupId)
        {
            if (groupId != model.GroupId) 
            {
                throw new StudentbyException("Neplatný požadavek");
            }

            var group = await _context.Groups.FirstOrDefaultAsync(gr => gr.GroupId == groupId);
            if (group == null)
            {
                return false;
            }

            group.Name = model.Name;

            await _context.SaveChangesAsync();

            return true;
        }
    }

}
