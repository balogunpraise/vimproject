using Core.Application.Interfaces;
using Core.Entities;
using Core.Entities.LinkingEntities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Logging;
using vimmvc.Core.Application.ExtensionMethods;

namespace Infrastructure.Repositories
{
    public class CourseRepository : ICourseRepository
	{
		private readonly ApplicationDbContext _context;
		private readonly ILogger<CourseRepository> _logger;

        public CourseRepository(ApplicationDbContext context, ILogger<CourseRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<ICollection<Course>> GetAllCourses()
        {
            return await _context.Coureses.ToListAsync();
        }

        public async Task<Course> GetCourseById(string id)
        {
            return await _context.Coureses.FirstAsync(c => c.Id == id);
        }

        public async Task<string> GetCourseName(string id)
        {
            return await _context.Coureses.Where(x =>x.Id == id).Select(x => x.Name).FirstOrDefaultAsync();
        }

        public async Task<EntityEntry<Course>> DeleteCourse(string id)
        {
            EntityEntry<Course> isDeleted = null;
            var course = await _context.Coureses.FirstOrDefaultAsync(c => c.Id == id);
            if (course != null)
            {
                isDeleted = _context.Coureses.Remove(course);
                await _context.SaveChangesAsync();
            }
            return isDeleted;
        }

        public async Task<Course?> UpdateCourse(string id, Course inputcourse)
        {
            var course = await _context.Coureses.FirstOrDefaultAsync(c => c.Id == id);
            if(course != null)
            {
                course.Name = inputcourse.Name.HasValue() ? inputcourse.Name : course.Name;
                course.DateUpdated = DateTime.UtcNow;
                course.Description = inputcourse.Description.HasValue() ? inputcourse.Description : course.Description;
                course.Duration = inputcourse.Duration != course.Duration ? inputcourse.Duration : course.Duration;
                await _context.SaveChangesAsync();
            }
            return course ?? null;
        }

        public async Task<bool> AddNewCourse(Course course)
        {
            var response = false;
            try
            {
                response = _context.Coureses.AddAsync(course).IsCompleted;
                await _context.SaveChangesAsync();
                return response;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task RemoveCourse(string id)
        {
           var course = await _context.Coureses.FirstOrDefaultAsync(x => x.Id == id);
            if(course != null)
                _context.Coureses.Remove(course);
           await _context.SaveChangesAsync();
        }

        public async Task<ICollection<UserCourse>> GetStudentCourses(string studentId)
        {
            var courses = await _context.UserCourses.Where(x => x.UserId == studentId).Include(x=> x.Course)
                    .ToListAsync();
            return courses;
        }
    }
}
