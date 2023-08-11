using Core.Entities.LinkingEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
	public class Assignment : BaseEntity
	{
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DateAssigned { get; set; }
        public DateTime ExpiryDate { get; set; }
        public bool IsExpired { get; set; }
        public bool IsCompleted { get; set; }
        public virtual ICollection<UserAssignment> Students { get; set; }

        public Assignment()
        {
            Students = new List<UserAssignment>();
        }
    }
}
