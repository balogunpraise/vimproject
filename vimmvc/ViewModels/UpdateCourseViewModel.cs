namespace vimmvc.ViewModels
{
    public class UpdateCourseViewModel
    {
		public string CourseTitle { get; set; }
		public string Description { get; set; }
		public string Duration { get; set; }
		public decimal Price { get; set; }
		public string Difficulty { get; set; }
		public IFormFile Picture { get; set; }
	}
}
