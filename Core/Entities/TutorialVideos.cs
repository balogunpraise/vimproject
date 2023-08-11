using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
	public class TutorialVideos : BaseEntity
	{
        public string title { get; set; }
        public string name { get; set; }
        public string duration { get; set; }
        public string courseId { get; set; }
    }
}
