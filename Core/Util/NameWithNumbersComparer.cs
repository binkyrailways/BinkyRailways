using System.Collections.Generic;
using System.Text;

namespace BinkyRailways.Core.Util
{
    /// <summary>
    /// Comparer for names with numbers in it.
    /// </summary>
    public class NameWithNumbersComparer : IComparer<string>
    {
        private static readonly char[] digits = new[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };

        /// <summary>
        /// Default instance
        /// </summary>
        public static readonly NameWithNumbersComparer Instance = new NameWithNumbersComparer();

        /// <summary>
        /// Compares two objects and returns a value indicating whether one is less than, equal to, or greater than the other.
        /// </summary>
        /// <returns>
        /// A signed integer that indicates the relative values of <paramref name="x"/> and <paramref name="y"/>, as shown in the following table.Value Meaning Less than zero<paramref name="x"/> is less than <paramref name="y"/>.Zero<paramref name="x"/> equals <paramref name="y"/>.Greater than zero<paramref name="x"/> is greater than <paramref name="y"/>.
        /// </returns>
        /// <param name="x">The first object to compare.</param><param name="y">The second object to compare.</param>
        public int Compare(string x, string y)
        {
            var ex = ExpandNumbers(x);
            var ey = ExpandNumbers(y);
            return string.Compare(ex, ey, System.StringComparison.OrdinalIgnoreCase);
        }
        

        /// <summary>
        /// Expand all numbers found in the given string.
        /// </summary>
        public static string ExpandNumbers(string s)
        {
            if (string.IsNullOrEmpty(s))
                return string.Empty;
            if (s.IndexOfAny(digits) < 0)
                return s;
            var sb = new StringBuilder();
            var i = 0;
            while (i < s.Length)
            {
                if (char.IsDigit(s[i]))
                {
                    // Start of a number
                    var start = i;
                    // Find end of number
                    while ((i < s.Length) && (char.IsDigit(s[i])))
                    {
                        i++;
                    }
                    // Number at start .. i (exclusive)
                    var numberString = s.Substring(start, i - start);
                    int number;
                    if (int.TryParse(numberString, out number))
                    {
                        // Expand number
                        sb.AppendFormat("{0:D9}", number);
                    }
                    else
                    {
                        // Cannot parse, add as is
                        sb.Append(numberString);
                    }
                }
                else
                {
                    // Just add the character
                    sb.Append(s[i++]);
                }
            }
            return sb.ToString();
        }
    }
}
