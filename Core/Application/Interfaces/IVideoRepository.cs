using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Interfaces
{
    public interface IVideoRepository
    {
        Task<bool> AddVideo(TutorialVideos videos);
        Task<ICollection<TutorialVideos>> GetCourseVideos(string courseId);
    }
}
