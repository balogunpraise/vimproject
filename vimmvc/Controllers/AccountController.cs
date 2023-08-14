using Core.Application.Constants;
using Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using vimmvc.Services;
using vimmvc.ViewModels.Auth;

namespace vimmvc.Controllers
{
    public class AccountController : Controller
    {
        private readonly AuthServices _authServices;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(AuthServices authServices, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _authServices = authServices;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Register(string errorMessage)
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromForm]SignupViewModel model)
        {
			if (ModelState.IsValid)
			{
				var response = await _authServices.RegisterUserAsync(model);
				string attachment = response.Item2 as string;
				if (response.Item1)
					//return RedirectToAction("Index", attachment + "Dashboard");
					return RedirectToAction("Login");
			}
			return RedirectToAction("Register", new { error = "Somethine went wrong" });
		}

        [HttpGet]
        public IActionResult Login(string returnUrl = "")
        {
            var model = new LoginViewModel { ReturnUrl = returnUrl };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var response = await _authServices.AuhenticateUserAsync(model);
                string attachment = response.Item2 as string;
                if (response.Item1) 
                {
					if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
					{
						return Redirect(model.ReturnUrl);
					}
					else
					{
						return RedirectToAction("Index", attachment + "Dashboard");
					}
				}  
            }
            return RedirectToAction("Index", new { error = "Somethine went wrong" });
        }

        public async Task<IActionResult> Logout()
        {
            await _authServices.LogoutAsync();
            return RedirectToAction("Login");
        }
    }
}
