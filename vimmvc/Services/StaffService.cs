using Core.Application.Constants;
using Core.Entities;
using Microsoft.AspNetCore.Identity;

namespace vimmvc.Services
{
    public class StaffService
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly RoleManager<IdentityRole> _roleManager;

        public StaffService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<ApplicationUser> CreateStaffAsync(string id)
        {
            var staff = await _userManager.FindByIdAsync(id);
            if (staff == null)
                return null;
            await _userManager.RemoveFromRoleAsync(staff, RoleConstants.Student);
            await _userManager.AddToRoleAsync(staff, RoleConstants.Staff);
            return staff;
        }

        public async Task<ICollection<ApplicationUser>> GetStaffList()
        {
            var list = await _userManager.GetUsersInRoleAsync(RoleConstants.Staff);
            return list;
        }

        public async Task<ApplicationUser> GetStaffAsync(string Id)
        {
            return await _userManager.FindByIdAsync(Id);
        }
    }
}
