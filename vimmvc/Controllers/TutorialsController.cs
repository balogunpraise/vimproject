using Core.Application.Interfaces;
using Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using vimmvc.ViewModels;

namespace vimmvc.Controllers
{
    public class TutorialsController : Controller
    {
        private readonly IVideoRepository _videoRepository;
        private readonly ICourseRepository _courseRepository;
        private static string _courseId;

        public TutorialsController(IVideoRepository videoRepository, ICourseRepository courseRepository)
        {
            _videoRepository = videoRepository;
            _courseRepository = courseRepository;

        }


        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Watch(string courseId)
        {
            var videos = await _videoRepository.GetCourseVideos(courseId);
            string videosJson = JsonSerializer.Serialize(videos);
            ViewBag.Videos = videosJson;
            return View();
        }

        [HttpGet]
        public IActionResult AddVideo(string id, string message)
        {
            ViewBag.message = message;
            _courseId = id;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddVideo(VideoViewModel input) 
        {
            var cExists = await _videoRepository.GetCourseVideos(_courseId);
            bool videoExists = cExists.Any(x => x.name == input.name);
            if (videoExists)
            {
                return RedirectToActionPermanent(nameof(Videos), new { error = "Video already exists on this course. Please upload another video" });
            }
            if(ModelState.IsValid)
            {
                var video = new TutorialVideos
                {
                    courseId = _courseId,
                    name = input.name,
                    title = input.title,
                    duration = input.duration,
                };
                await _videoRepository.AddVideo(video);
                return RedirectToActionPermanent(nameof(Videos), new { message = "Video added successfully" });
            }
            return RedirectToActionPermanent(nameof(Videos), new { error = "Video added successfully" });
        }

        [HttpGet]
        public async Task<IActionResult> Videos(string message = null, string error = null)
        {
            if(message == null && error != null)
            {
                ViewBag.Error = error;
            }
            if(message != null && error == null) 
            {
                ViewBag.Message = message;
            }
            ViewBag.CourseName = await _courseRepository.GetCourseName(_courseId);
            ViewBag.videos = await _videoRepository.GetCourseVideos(_courseId);
            return View();
        }
    }
}
