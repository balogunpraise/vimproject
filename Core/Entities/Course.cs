using Core.Entities.LinkingEntities;

namespace Core.Entities
{
    public class Course : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Duration { get; set; }
        public DateTime? DateRegistered { get; set; }
        public decimal Price { get; set; }
        public string Difficulty { get; set; }
        public string ImageUrl { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public virtual ICollection<UserCourse> Students { get; set; }

        public Course()
        {
            Students = new List<UserCourse>();
        }
    }
}
