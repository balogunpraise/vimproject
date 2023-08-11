using Core.Entities;
using Microsoft.AspNetCore.Mvc;
using vimmvc.Services;
using vimmvc.ViewModels;

namespace vimmvc.Controllers
{
    public class ShopController : Controller
    {
        private readonly MusicScoreService _musicScoreService;

        public ShopController(MusicScoreService musicScoreService)
        {
            _musicScoreService = musicScoreService;
        }


        public async Task<IActionResult> Index()
        {
            ViewBag.SheetMusic = await _musicScoreService.GetAllScores();
            return View();
        }

        public async Task<IActionResult> CreateNewScore(CreateMusicScoreViewModel model)
        {
            bool response;
            if(ModelState.IsValid)
            {
                response = await _musicScoreService.AddNewScore(model);
            }
            return RedirectToAction("GetMusicScoreList", "AdminDashboard", new {message = "Music score add successfully"});
        }
    }
}
