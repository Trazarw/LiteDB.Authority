using System.Collections.Generic;
using System.Linq;

namespace LiteDB.Authority.Extensions
{
    public static class WhiteListStringExtension
    {
        private static HashSet<char> _allowedChars = new HashSet<char>(@"1234567890ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz.,'()?!#$");

        public static bool IsWhitelisted(this string value)
        {
            var invalidChars = value.Where(c => !_allowedChars.Contains(c));
            if(invalidChars.Any())
            {
                return false;
            }
            return true;
        }

        public static string GetWhiteList(this string value)
        {
            return string.Join(string.Empty, _allowedChars);
        }
    }
}
