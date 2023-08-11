using Core.Entities;
using Core.Entities.LinkingEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Interfaces
{
	public interface ICourseRepository
	{
        Task<ICollection<Course>> GetAllCourses();
        Task<bool> AddNewCourse(Course course);
        Task RemoveCourse(string id);
        Task<Course> GetCourseById(string id);
		Task<Course?> UpdateCourse(string id, Course inputcourse);
		Task<ICollection<UserCourse>> GetStudentCourses(string studentId);
        Task<string> GetCourseName(string id);

    }
}
