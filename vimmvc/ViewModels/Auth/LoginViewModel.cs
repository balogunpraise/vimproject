namespace vimmvc.ViewModels.Auth
{
	public class LoginViewModel
	{
        public string EmailOrUserName { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
        public string ReturnUrl { get; set; }
    }
}
