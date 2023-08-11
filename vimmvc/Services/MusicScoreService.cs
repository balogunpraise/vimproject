using Core.Application.Interfaces;
using Core.Entities;
using vimmvc.ViewModels;

namespace vimmvc.Services
{
    public class MusicScoreService
    {
        private readonly IMusicScoreRepository _musicScoreRepository;
        public MusicScoreService(IMusicScoreRepository musicScoreRepository)
        {
            _musicScoreRepository = musicScoreRepository;
        }

        public async Task<bool> AddNewScore(CreateMusicScoreViewModel model)
        {
            var music = new MusicScore
            {
                Title = model.Title,
                Price = model.Price,
                Difficulty = model.Difficulty,
                DateCreated = DateTime.Now,
                Author = model.Author,
                ImageUrl = "https://thumbs.dreamstime.com/b/sheet-music-14492279.jpg",
                Instrument = model.Instrument,
                DownloadString = model.DownloadString,
            };
            var response = await _musicScoreRepository.CreateMusicScore(music);
            return response;
        }

        public async Task<IEnumerable<MusicScore>> GetAllScores()
        {
            return await _musicScoreRepository.GetAll();
        }
    }
}
