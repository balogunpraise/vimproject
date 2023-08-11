using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Interfaces
{
    public interface IMusicScoreRepository
    {
        Task<bool> CreateMusicScore(MusicScore musicScore);
        Task<IEnumerable<MusicScore>> GetAll();
    }
}
