using Core.Entities;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace vimmvc.Common
{
    public class Current
    {
        private static IHttpContextAccessor _contextAccessor;
        private static UserManager<ApplicationUser> _userManager;
        public Current(IHttpContextAccessor contextAccessor, UserManager<ApplicationUser> userManager)
        {
            _contextAccessor = contextAccessor;
            _userManager = userManager;
        }
        public static ApplicationUser User
        {
            get
            {
                string email = _contextAccessor.HttpContext.User?.FindFirst(ClaimTypes.Email).Value;
                return _userManager.FindByEmailAsync(email).Result;
            }
        }
    }
}
