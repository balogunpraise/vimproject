using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class CourseTransaction : BaseEntity
    {
        public string TransRefrence { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsSuceeded { get; set; }
        public string Status { get; set; }
        public string UserId { get; set; }
        public int Amount { get; set; }
    }
}
