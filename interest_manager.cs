using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PortfolioOfEvidence_Part3
{// start of namespace

    class interest_manager
    {// start of interest manager class

        // Method to save interests
        public static string store_interest(string[] words, string username, ArrayList ignore)
        {// start of staore interest method

            // Check if user mentioned interests
            if (!words.Contains("interested"))
                return "";

            // Store interests without duplicates
            HashSet<string> interests =
                new HashSet<string>();

            // Loop through words
            foreach (string word in words)
            {// start of foreach loop

                // Ignore unnecessary words
                if (
                    !ignore.Contains(word) &&
                    word != "interested" &&
                    word != "and" &&
                    word != "in" &&
                    word.Length >= 3
                )
                {
                    interests.Add(word);
                }
            }// end of for each loop

            // Convert interests to string
            string interestText = string.Join(", ", interests);

            // Save interests to file
            File.AppendAllText(
                "interested_topic.txt",

                username +
                " interested in: " +
                string.Join(", ", interests) +
                "\n"
            );

            // Return chatbot response
            return
                "Great! I will remember that you are interested in " +
                interestText + ".";

        }// end of store interest method

    }// end of interest manager class

}// end of namespace