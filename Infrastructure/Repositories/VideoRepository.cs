using Core.Application.Interfaces;
using Core.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
	public class VideoRepository : IVideoRepository
    {
        private readonly ApplicationDbContext _context;

        public VideoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AddVideo(TutorialVideos videos)
        {
            try
            {
                await _context.TutorialVideos.AddAsync(videos);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
           
        }

        public async Task<ICollection<TutorialVideos>> GetCourseVideos(string courseId)
        {
            return await _context.TutorialVideos.Where(x => x.courseId == courseId).ToListAsync();
        }

        public async Task<bool> DeleteVideo(string id)
        {
            try
            {
                var video = await _context.TutorialVideos.Where(x => x.Id == id).SingleOrDefaultAsync();
                if(video != null)
                {
                    _context.TutorialVideos.Remove(video);
                    await _context.SaveChangesAsync();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
