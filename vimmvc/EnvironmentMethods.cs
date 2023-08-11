namespace vimmvc
{
    public class EnvironmentMethods
    {
        private readonly IWebHostEnvironment _env;
        public EnvironmentMethods(IWebHostEnvironment env)
        {
            _env = env;
        }
        public string GetRunningEnvironment()
        {
            return _env.EnvironmentName;
        }
    }
}
