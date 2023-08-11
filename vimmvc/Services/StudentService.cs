using Core.Application.Constants;
using Core.Entities;
using Microsoft.AspNetCore.Identity;

namespace vimmvc.Services
{
	public class StudentService
	{
		private readonly UserManager<ApplicationUser> _userManager;

        public StudentService(UserManager<ApplicationUser> userManager)
        {
			_userManager = userManager;
        }
        public async Task<ICollection<ApplicationUser>> GetAllStudents()
		{
			var students = await _userManager.GetUsersInRoleAsync(RoleConstants.Student);
			return students;
		}
	}
}
