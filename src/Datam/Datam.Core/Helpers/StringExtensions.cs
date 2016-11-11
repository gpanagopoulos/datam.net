using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Datam.Core.Helpers
{
    public static class StringExtensions
    {
        public static IEnumerable<string> SplitSqlStatementsOnGo(this string sqlScript)
        {
            // Split by "GO" statements
            var statements = Regex.Split(
                sqlScript,
                @"^\s*GO\s*\d*\s*($|\-\-.*$)",
                RegexOptions.Multiline |
                RegexOptions.IgnorePatternWhitespace |
                RegexOptions.IgnoreCase);

            // Remove empties, trim, and return
            return statements
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .Select(x => x.Trim(' ', '\r', '\n'));
        }
    }
}
