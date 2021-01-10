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

    /// <summary>
    /// Service for Group operations
    /// </summary>
    public class GroupService: IGroupService
    {
        private readonly StudentbyContext _context;

        public GroupService(StudentbyContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get list of Groups
        /// </summary>
        /// <returns>List of Group DTOs</returns>
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

        /// <summary>
        /// Get detail of Group
        /// </summary>
        /// <param name="groupId">Group ID</param>
        /// <returns>Group DTO</returns>
        public async Task<GroupWithCustsRes> GetDetailAsync(int groupId)
        {
            var group = await _context.Groups
                .Include(gr => gr.Customers)
                    .ThenInclude(cu => cu.User)
                .FirstOrDefaultAsync(gr => gr.GroupId == groupId);

            // group not found - 404
            if (group == null)
            {
                return null;
            }

            return new GroupWithCustsRes(group);
        }

        /// <summary>
        /// Create Group
        /// </summary>
        /// <param name="model">Group DTO</param>
        /// <returns>Group DTO</returns>
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

        /// <summary>
        /// Edit Group
        /// </summary>
        /// <param name="model">Group DTO</param>
        /// <param name="groupId">Group ID</param>
        /// <returns>Bool if Group was found</returns>
        public async Task<bool> EditAsync(GroupWithIdReq model, int groupId)
        {
            // route id and model id differs
            if (groupId != model.GroupId) 
            {
                throw new AppLogicException("Neplatný požadavek");
            }

            var group = await _context.Groups.FirstOrDefaultAsync(gr => gr.GroupId == groupId);
            // group not found - 404
            if (group == null)
            {
                return false;
            }

            // edit group
            group.Name = model.Name;
            await _context.SaveChangesAsync();
            return true;
        }
    }

}
