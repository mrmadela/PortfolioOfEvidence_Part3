using System.Text;
using System.Text.RegularExpressions;

namespace PortfolioOfEvidence_Part3
{// start of namspace
    public class special_character
    {// start of special character class
        public static string RemoveSpecialCharacters(string input)
        {// start of method

            // check if the input is empty
            if (string.IsNullOrWhiteSpace(input))
                return string.Empty;

            // stringBuilder object
            StringBuilder sanitized = new StringBuilder();

            // loop through every character
            foreach (char c in input)
            {// start of foreach loop

                // keep letters, numbers and spaces
                if (char.IsLetterOrDigit(c) ||
                    char.IsWhiteSpace(c) ||
                    c == '\'' ||
                    c == '-')
                {
                    sanitized.Append(c);
                }
                else
                {
                    sanitized.Append(' ');
                }
            }// end of foreach loop


            // return clean string and remove extra spaces
            return Regex.Replace(
                sanitized.ToString(),
                @"\s+",
                " "
            ).Trim();

        }// end of method

    }// end of special_character
}// end of namespace