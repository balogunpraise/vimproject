using Microsoft.IdentityModel.Tokens;

namespace vimmvc.Extensions.ExtensionMethods
{
    public static class StringExtensions
    {
      public static bool HasValue(this string value)
        {
            return value.IsNullOrEmpty() || value.Length == 0;
        }
    }
}
