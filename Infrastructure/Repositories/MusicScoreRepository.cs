using Core.Application.Interfaces;
using Core.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class MusicScoreRepository : IMusicScoreRepository
    {
        private readonly ApplicationDbContext _context;
        public MusicScoreRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<bool> CreateMusicScore(MusicScore musicScore)
        {
            bool isCompleted = false;
            try
            {
                await _context.AddAsync(musicScore);
                await _context.SaveChangesAsync();
                isCompleted = true;
                return isCompleted;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }

        public async Task<IEnumerable<MusicScore>> GetAll()
        {
            return await _context.MusicScores.ToListAsync();
        }
    }
}
