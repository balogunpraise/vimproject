using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities.LinkingEntities
{
	public class UserCourse
	{
        public string UserId { get; set; }
        public string CourseId { get; set; }
        public ApplicationUser User { get; set; }
        public Course Course { get; set; }
    }
}
