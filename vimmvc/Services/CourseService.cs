using Core.Application.Interfaces;
using Core.Entities;
using Core.Entities.LinkingEntities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using vimmvc.Cloudinary;
using vimmvc.ViewModels;

namespace vimmvc.Services
{
	public class CourseService
	{
		private ICourseRepository _courseRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;

        public CourseService(ICourseRepository courseRepository, UserManager<ApplicationUser> userManager,
            ApplicationDbContext context)
        {
            _courseRepository = courseRepository;
            _userManager = userManager;
            _context = context;
        }

        public async Task<bool> CreateCourse(CreateCourseViewModel input)
        {
            var newCourse = new Course()
            {
                Name = input.CourseTitle,
                Description = input.Description,
                Duration = input.Duration,
                Price = input.Price,
                Difficulty = input.Difficulty,
                DateCreated = DateTime.Now,
            };
            //if (input.Picture != null)
            //{
                //var stream = input.Picture.GetStream();
                //newCourse.ImageUrl = CloudinaryService.UploadImage(input.CourseTitle + Guid.NewGuid().ToString(), stream).Result.ImageUrl;
                newCourse.ImageUrl = "https://img.freepik.com/free-vector/elegant-musical-notes-music-chord-background_1017-20759.jpg";
            //}
            var response = await _courseRepository.AddNewCourse(newCourse);
            return response;
        }
        public async Task<int> GetCourseCount()
        {
            var courses = await _courseRepository.GetAllCourses();
            return courses.Count();
        }
        public async Task<ICollection<Course>> GetAllCourses() => await _courseRepository.GetAllCourses();

        public async Task RemoveCourse(string id)
        {
            await _courseRepository.RemoveCourse(id);
        }

        public async Task<Course> UpdateCourse(string id, CreateCourseViewModel input)
        {
            var course = await GetCourse(id);
            if(course != null)
            {
                course.Name = input.CourseTitle ?? course.Name;
                course.Description = input.Description ?? course.Description;
                course.Duration = input.Duration ?? course.Duration;
                course.Price = input.Price == 0 ? course.Price : input.Price;
                course.Difficulty = input.Difficulty ?? course.Difficulty;
                course.DateUpdated = DateTime.Now;
            }
            var response = await _courseRepository.UpdateCourse(id, course);
            return response;
        }
        public async Task<Course> GetCourse(string id)
        {
            return await _courseRepository.GetCourseById(id);
        }


        public async Task<bool> Enrol(string studentId, string courseId)
        {
            bool result;
            var selectedCourse = await GetCourse(courseId);
            var student = await _userManager.FindByIdAsync(studentId);
            try
            {
                if (student != null && selectedCourse != null)
                {
                    student.Courses.Add(new UserCourse
                    {
                        User = student,
                        Course = selectedCourse
                    });
                    await _context.SaveChangesAsync();
                }
                result = true;
            }
            catch (Exception ex)
            {
                result = false;
            }
           return result;
        }

        public async Task<ICollection<UserCourse>> GetStudentCourse(string studentId)
        {
            return await _courseRepository.GetStudentCourses(studentId);
        }
    }
}
