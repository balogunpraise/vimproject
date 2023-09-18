using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using vimmvc.Services;

namespace vimmvc.Controllers.Dashboards
{
	public class StudentDashboardController : Controller
	{
		private readonly IHttpContextAccessor _contextAccessor;
		private readonly CourseService _courseService;
		private string UserId;
        public StudentDashboardController(IHttpContextAccessor contextAccessor, CourseService courseService)
        {
            _contextAccessor = contextAccessor;
			_courseService = courseService;
			UserId = _contextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }
        [Route("Index")]
		public async Task<IActionResult> Index()
		{
            ViewBag.MyCourses = await _courseService.GetStudentCourse(UserId);
            return View();
		}

		public async Task<IActionResult> MyCourses()
		{
			ViewBag.MyCourses = await _courseService.GetStudentCourse(UserId);
			return View();
		}

		public IActionResult Practice()
		{
			return View();
		}
		public IActionResult MyAssignments() 
		{
			return View();
		}

		public IActionResult MyCurriculum()
		{
			return View();
		}

	}
}
