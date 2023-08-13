using Core.Application.Constants;
using Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Net;
using vimmvc.ViewModels.Auth;

namespace vimmvc.Services
{
	public class AuthServices
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;
		private readonly RoleManager<IdentityRole> _roleManager;
		private readonly ILogger<AuthServices> _logger;

        public AuthServices(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,
			RoleManager<IdentityRole> roleManager, ILogger<AuthServices> logger)
        {
            _logger = logger;
			_userManager = userManager;
			_signInManager = signInManager;
			_roleManager = roleManager;
        }


		public async Task<(bool, string)> AuhenticateUserAsync(LoginViewModel login)
		{
			ApplicationUser user;
			(bool succeeded, string role) response = (false, string.Empty); 
			if (login.EmailOrUserName.Contains('@') && login.EmailOrUserName.Contains(".com"))
			{
				user = await _userManager.FindByEmailAsync(login.EmailOrUserName);
			}
			else
			{
				user = await _userManager.FindByNameAsync(login.EmailOrUserName);
			}
			if (user != null)
			{
				var result = await _signInManager.PasswordSignInAsync(user, login.Password, login.RememberMe, false);
				if (result.Succeeded)
				{
					if (await _userManager.IsInRoleAsync(user, RoleConstants.Admin))
					{
						response.succeeded = true;
						response.role = RoleConstants.Admin;
					}
					if (await _userManager.IsInRoleAsync(user, RoleConstants.Staff))
					{
						response.succeeded = true;
						response.role = RoleConstants.Staff;
					}
					if (await _userManager.IsInRoleAsync(user, RoleConstants.Student))
					{
						response.succeeded = true;
						response.role = RoleConstants.Student;
					}
				}
			}
			return response;
		}

		public async Task<(bool, string)> RegisterUserAsync(SignupViewModel model)
		{
			(bool succeeded, string role) response = (false, string.Empty);
			var userExists = await _userManager.FindByEmailAsync(model.Email);
			if (userExists == null)
			{
				var user = new ApplicationUser
				{
					FirstName = model.FirstName,
					LastName = model.LastName,
					Email = model.Email,
					UserName = !(model.UserName.IsNullOrEmpty()) ? model.UserName : model.Email,
				};
				response.succeeded = (await _userManager.CreateAsync(user, model.Password)).Succeeded;
				await _userManager.AddToRoleAsync(user, RoleConstants.Student);
				response.role = RoleConstants.Student;
				return response;
			}
			return response;
		}

		public async Task LogoutAsync()
		{
			await _signInManager.SignOutAsync();
		}

    }
}
