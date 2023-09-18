using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Cloudinary
{
    public class CourseTutorialVideo : BaseEntity
    {
        public string CourseId { get; set; }
        public string Url { get; set; }
        public string Title { get; set; }
    }
}
