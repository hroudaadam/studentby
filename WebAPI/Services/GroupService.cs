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
        Task<GroupRes> CreateAsync(GroupReq model);
        Task<IEnumerable<GroupRes>> GetListAsync();
        Task<GroupWithCustsRes> GetDetailAsync(int groupId);
        Task<bool> EditAsync(GroupWithIdReq model, int groupId);
    }

    public class GroupService: IGroupService
    {
        private readonly StudentbyContext _context;

        public GroupService(StudentbyContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<GroupRes>> GetListAsync()
        {
            var groups = await _context.Groups.ToListAsync();

            var response = new List<GroupRes>();
            foreach (var group in groups)
            {
                response.Add(new GroupRes(group));
            }

            return response;
        }

        public async Task<GroupWithCustsRes> GetDetailAsync(int groupId)
        {
            var group = await _context.Groups
                .Include(gr => gr.Customers)
                    .ThenInclude(cu => cu.User)
                .FirstOrDefaultAsync(gr => gr.GroupId == groupId);

            if (group == null)
            {
                return null;
            }

            return new GroupWithCustsRes(group);
        }

        public async Task<GroupRes> CreateAsync(GroupReq model)
        {
            var group = new Group
            {
                Name = model.Name
            };
            _context.Groups.Add(group);
            await _context.SaveChangesAsync();

            return new GroupRes(group);
        }

        public async Task<bool> EditAsync(GroupWithIdReq model, int groupId)
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
