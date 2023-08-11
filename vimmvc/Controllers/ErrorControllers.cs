using Microsoft.AspNetCore.Mvc;

namespace vimmvc.Controllers
{
	public class ErrorControllers : Controller
	{
		public IActionResult ActionError(string message)
		{
			ViewBag.Message = message;
			return View();
		}
	}
}
