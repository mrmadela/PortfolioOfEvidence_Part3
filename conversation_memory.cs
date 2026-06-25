using System;
using System.Linq;

namespace PortfolioOfEvidence_Part3
{// start of namespace

    // Class used to manage
    // chatbot conversation memory
    public class conversation_memory
    {// start of class

        // Store previous topic
        public static string previousTopic = "";

        // Store previous response
        public static string previousResponse = "";

        // Follow-up keywords
        static string[] followUpWords =
        {
            "more",
            "another",
            "continue",
            "explain",
            "again",
            "details",
            "tell"
        };


        // Detect follow-up message
        public static bool is_followup(string[] words)
        {// start of method

            // Check if any follow-up keywords are present
            foreach (string word in words)
            {// start of foreach loop

                if (
                    followUpWords.Contains(word)
                )
                {
                    return true;
                }
            }// end of foreach loop

            return false;

        }// end of method



        // Add previous topic
        public static string continue_topic(string question)
        {// start of method

            // Check previous topic
            if (
                !string.IsNullOrWhiteSpace(
                    previousTopic
                )
            )
            {
                // Add previous topic to question
                question += " " + previousTopic;

            }

            return question;

        }// end of method



        // Save current topic
        public static void save_topic(string topic)
        {// start of method

            // Save topic for future reference
            previousTopic = topic;

        }// end of method



        // Save chatbot response
        public static void save_response(
            string response
        )
        {// start of method

            // Save response for future reference
            previousResponse = response;

        }// end of method

    }// end of class

}// end of namespace