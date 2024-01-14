using Microsoft.AspNetCore.Mvc;

namespace vimmvc.Controllers.Dashboards
{
    public class EventController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
