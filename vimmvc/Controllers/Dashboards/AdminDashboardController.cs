using Core.Application.Constants;
using Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using vimmvc.Services;

namespace vimmvc.Controllers.Dashboards
{

    [Authorize(Roles = RoleConstants.Admin)]
    public class AdminDashboardController : Controller
    {
		private readonly StaffService _staffService;
		private readonly CourseService _courseService;
		private readonly MusicScoreService _musicScoreService;
		private readonly StudentService _studentService;
        public AdminDashboardController(StaffService staffService, CourseService courseService,
			MusicScoreService musicScoreService, StudentService studentService)
        {
			_staffService = staffService;
			_courseService = courseService;
			_musicScoreService = musicScoreService;
			_studentService = studentService;
        }

		[HttpGet]
        public IActionResult Index()
		{

			return View();
		}

		[HttpGet]
		public async Task<IActionResult> Staff()
		{
			var staff = await _staffService.GetStaffList();
			ViewBag.Product = staff;
            return View();
		}

		public async Task<IActionResult> CourseList(string message)
		{
			ViewBag.Message = message;
			ViewBag.Courses = await _courseService.GetAllCourses();
			return View();
		}

		[HttpGet]
		public async Task<IActionResult> GetSingleStaff(string id)
		{
			var singleStaff = await _staffService.GetStaffAsync(id);
			ViewBag.SingleStaff = singleStaff;
			return View();
		}

		[HttpGet]
		public async Task<IActionResult> CreateStaff(string id)
		{
			var singleStaff = await _staffService.CreateStaffAsync(id);
			ViewBag.SingleStaff = singleStaff;
            return RedirectToAction("UserProfile", "Profile", new { id = id });
		}

		[HttpGet]
		public async Task<IActionResult> GetMusicScoreList(string message)
		{
			if (!string.IsNullOrEmpty(message))
				ViewBag.CreationStatus = message;
			ViewBag.MusicScore = await _musicScoreService.GetAllScores();
			return View();
		}

		[HttpGet]
		public async Task<IActionResult> Students()
		{
			var students = await _studentService.GetAllStudents();
			ViewBag.Students = students;
			return View();
		}
	}
}
