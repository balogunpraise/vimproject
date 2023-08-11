using Core.Entities.LinkingEntities;
using Microsoft.AspNetCore.Identity;

namespace Core.Entities
{
	public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsSubscribed { get; set; }
        public bool IsStudent { get; set; }
        public virtual ICollection<UserCourse> Courses { get; set; }
        public virtual ICollection<UserAssignment> Assignments { get; set; }
        public ICollection<InternalMessages> Messages { get; set; }
        public ICollection<CourseTransaction> Transactions { get; set; }

        public ApplicationUser()
        {
            Assignments = new List<UserAssignment>();
            Messages = new List<InternalMessages>();
            Transactions = new List<CourseTransaction>();
            Courses = new List<UserCourse>();   
        }
    }
}
