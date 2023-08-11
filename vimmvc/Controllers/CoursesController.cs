using Core.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PayStack.Net;
using System.Security.Claims;
using vimmvc.Services;
using vimmvc.ViewModels;

namespace vimmvc.Controllers
{
    public class CoursesController : Controller
    {
        private readonly CourseService _courseService;
        private readonly IHttpContextAccessor _contextAccessor;
        private string UserId;
        private readonly UserManager<ApplicationUser> _userManager;
        private static string _courseId;

        //new
        private readonly IConfiguration _configuration;
        private PayStackApi Paystack { get; set; }
        private readonly string token;
        private readonly ApplicationDbContext _context;

        public CoursesController(CourseService courseService, IHttpContextAccessor contextAccessor,
            IConfiguration configuration, ApplicationDbContext context,
            UserManager<ApplicationUser> userManager)
        {
            _configuration = configuration;
            token = _configuration["Payment:PaystackSk"];
            Paystack = new PayStackApi(token);
            _context = context;
            _userManager = userManager;
            _courseService = courseService;
            _contextAccessor = contextAccessor;
            UserId = _contextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            //ViewBag.Message = message;
            ViewBag.Courses = await _courseService.GetAllCourses();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateCourse(CreateCourseViewModel input)
        {
            if (ModelState.IsValid)
            {
                var response = await _courseService.CreateCourse(input);
            }
            return RedirectToAction("CourseList", "AdminDashboard", new {message = "Course addes successfully"});
            }

        public async Task<IActionResult> RemoveCourse(string id)
        {
            await _courseService.RemoveCourse(id);
            return RedirectToAction("CourseList", "AdminDashboard");
        }

        [HttpGet]
        public async Task<IActionResult> CourseDetails(string id)
        {
            ViewBag.SingleCourse = await _courseService.GetCourse(id);
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> EditCourse(CreateCourseViewModel input)
        {
            Course response;
            if (ModelState.IsValid)
            {
                response = await _courseService.UpdateCourse(input.Id, input);
                if (response != null)
                    return RedirectToAction("CourseList", "AdminDashboard", new { message = "Course updated successfully." });
                else
                    return RedirectToAction("CourseList", "AdminDashboard", new { message = "Course was not updated" });
            }
            return RedirectToAction("CourseList", "AdminDashboard", new { message = "Course was not updated" });
        }


        [HttpGet("enrol:{courseId}")]
        public async Task<IActionResult> Enrol(string courseId)
        {
            var user = await _userManager.FindByIdAsync(UserId);
            var course = await _courseService.GetCourse(courseId);
            if (user != null && courseId != null) 
            {
                _courseId = courseId;
                TransactionInitializeRequest request = new()
                {
                    AmountInKobo = (int)course.Price * 100,
                    Email = user.Email,
                    Reference = GenerateReference().ToString(),
                    Currency = "NGN",
                    CallbackUrl = "https://localhost:7011/courses/verify"

                };
                TransactionInitializeResponse response = Paystack.Transactions.Initialize(request);
                if (response.Status)
                {
                    var transcation = new CourseTransaction
                    {
                        Amount = (int)course.Price,
                        Email = user.Email,
                        TransRefrence = request.Reference,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        UserId = user.Id,
                    };
                    await _context.Transactions.AddAsync(transcation);
                    await _context.SaveChangesAsync();
                    return Redirect(response.Data.AuthorizationUrl);
                }
            }
            return RedirectToAction("ActionError", "Error", new { message = "Action was not completed successfully" });
        }

        [HttpGet]
        public async Task<IActionResult> Verify(string reference)
        {
            TransactionVerifyResponse response = Paystack.Transactions.Verify(reference);
            if (response.Data.Status == "success")
            {
                var transaction = await _context.Transactions.Where(x => x.TransRefrence == reference)
                    .FirstOrDefaultAsync();
                if (transaction != null)
                {
                    transaction.IsSuceeded = true;
                    _context.Transactions.Update(transaction);
                    await _context.SaveChangesAsync();
                    var enrolresponse = await _courseService.Enrol(UserId, _courseId);
                    if (enrolresponse)
                        return RedirectToAction("MyCourses", "StudentDashboard", new { id = UserId });
                }
            }
            return RedirectToAction("ActionError", "Error", new { message = "Payment was not verified successfully" });
        }

        private static int GenerateReference()
        {
            Random random = new Random((int)DateTime.Now.Ticks);
            return random.Next(100000000, 999999999);
        }

    }
}
