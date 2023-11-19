using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AppUI.Utilities
{
    /// <summary>
    /// Represents a wildcard running on the
    /// <see cref="System.Text.RegularExpressions"/> engine.
    /// Taken from https://www.codeproject.com/Articles/11556/Converting-Wildcards-to-Regexes
    /// </summary>
    public class Wildcard : Regex
    {
        [JsonInclude]
        public string WildcardString;

        /// <summary>
        /// Initializes a wildcard with the given search pattern.
        /// </summary>
        /// <param name="pattern">The wildcard pattern to match.</param>
        public Wildcard(string pattern)
         : base(WildcardToRegex(pattern))
        {
            WildcardString = pattern;
        }

        /// <summary>
        /// Initializes a wildcard with the given search pattern and options.
        /// </summary>
        /// <param name="pattern">The wildcard pattern to match.</param>
        /// <param name="options">A combination of one or more
        /// <see cref="System.Text.RegexOptions"/>.</param>
        [JsonConstructor]
        public Wildcard(string pattern, RegexOptions options)
         : base(WildcardToRegex(pattern), options)
        {
            WildcardString = pattern;
        }

        /// <summary>
        /// Converts a wildcard to a regex.
        /// </summary>
        /// <param name="pattern">The wildcard pattern to convert.</param>
        /// <returns>A regex equivalent of the given wildcard.</returns>
        public static string WildcardToRegex(string pattern)
        {
            return "^" + Escape(pattern).
             Replace("\\*", ".*").
             Replace("\\?", ".") + "$";
        }

        public override string ToString()
        {
            return WildcardString;
        }


        public static void TestMethod()
        {
            // usage example

            // Get a list of files in the My Documents folder
            string[] files = Directory.GetFiles(
             Environment.GetFolderPath(
             Environment.SpecialFolder.Personal));

            // Create a new wildcard to search for all
            // .txt files, regardless of case
            Wildcard wildcard = new Wildcard("*.txt", RegexOptions.IgnoreCase);

            // Print all matching files
            foreach (string file in files)
                if (wildcard.IsMatch(file))
                    Console.WriteLine(file);
        }
    }
}
