
namespace vimmvc.Core.Application.ExtensionMethods
{
    public static class StringExtensions
    {
        public static bool HasValue(this string value)
        {
            return value != "";
        }
    }
}
