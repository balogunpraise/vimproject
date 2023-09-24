using Core.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace vimmvc.Controllers
{
    public class VideosController : Controller
    {
        private readonly IVideoRepository _repo;


        public VideosController(IVideoRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string courseId, string message = null)
        {
            var videos = await _repo.GetCourseVideos(courseId);
            ViewBag.videos = videos;
            return View();
        }


        public async Task<IActionResult> DeleteVideo(string vidId)
        {
            var result = await _repo.DeleteVideo(vidId);
            if (result)
                return RedirectToAction(nameof(Index), new { message = "Video Deleted Successfully" });
            else
                return RedirectToAction(nameof(Index), new { message = "Operation Failed" });
        }
    }
}
