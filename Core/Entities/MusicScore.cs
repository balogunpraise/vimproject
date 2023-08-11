using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
	public class MusicScore : BaseEntity
	{
        public string Title { get; set; }
        public string Instrument { get; set; }
        public string DownloadString { get; set; }
        public decimal Price { get; set; }
        public string Difficulty { get; set; }
        public string ImageUrl { get; set; }
        public string Author { get; set; }
    }
}
