using Microsoft.AspNetCore.Mvc;

namespace vimmvc.Controllers
{
    public class CurriculumController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
