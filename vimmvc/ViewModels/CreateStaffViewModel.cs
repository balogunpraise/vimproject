namespace vimmvc.ViewModels
{
    public class CreateStaffViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string CourseName { get; set; }
        public decimal Salary { get; set; }
        public IFormFile ProfilePic { get; set; }
    }
}
