using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace UserManagementAPI.Services
{
    public static class StringExtention
    {
        public static bool IsValidEmail(this string email)
        {
            string pattern = @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|" + @"([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)" + @"@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$";
            var regex = new Regex(pattern, RegexOptions.IgnoreCase);
            return regex.IsMatch(email);
        }
    }

    public static class StringListExtention
    {
        public static void addStringIfNotExist(this List<string> list, string newstr)
        {
            if (!list.Contains(newstr))
                list.Add(newstr);
        }
    }
}
