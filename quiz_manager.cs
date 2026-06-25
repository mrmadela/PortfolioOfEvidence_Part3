using System;
using System.Collections.Generic;

namespace PortfolioOfEvidence_Part3
{// start of namespace

    // Model for a single quiz question
    public class QuizQuestion
    {// start of QuizQuestion class

        public string Question { get; set; }
        public string[] Options { get; set; }
        public int CorrectIndex { get; set; }
        public string Explanation { get; set; }
        public bool IsTrueFalse { get; set; }

    }// end of QuizQuestion class


    // Class to manage the cybersecurity quiz
    public class quiz_manager
    {// start of quiz_manager class

        // All quiz questions
        private List<QuizQuestion> allQuestions;

        // Questions used in current game (shuffled subset)
        public List<QuizQuestion> ActiveQuestions { get; private set; }

        // Current question index
        public int CurrentIndex { get; private set; }

        // Score tracker
        public int Score { get; private set; }

        // Total questions per game
        private const int QUESTIONS_PER_GAME = 10;


        // Constructor — load all questions
        public quiz_manager()
        {// start constructor

            CurrentIndex = 0;
            Score = 0;
            load_questions();
            prepare_game();

        }// end constructor


        // Load all available questions into the pool
        private void load_questions()
        {// start load_questions

            allQuestions = new List<QuizQuestion>
            {
                // multiple choice
                new QuizQuestion
                {
                    Question     = "What should you do if you receive an email asking for your password?",
                    Options      = new[] { "A) Reply with your password", "B) Delete the email", "C) Report the email as phishing", "D) Ignore it" },
                    CorrectIndex = 2,
                    Explanation  = "Reporting phishing emails helps prevent scams and protects others.",
                    IsTrueFalse  = false
                },

                new QuizQuestion
                {
                    Question     = "Which of the following is the strongest password?",
                    Options      = new[] { "A) password123", "B) John1990", "C) Tr0ub4dor&3!", "D) qwerty" },
                    CorrectIndex = 2,
                    Explanation  = "Strong passwords use a mix of upper/lowercase, numbers, and symbols.",
                    IsTrueFalse  = false
                },

                new QuizQuestion
                {
                    Question     = "What does a VPN primarily do?",
                    Options      = new[] { "A) Speeds up your internet", "B) Encrypts your internet connection", "C) Removes viruses", "D) Blocks ads" },
                    CorrectIndex = 1,
                    Explanation  = "A VPN encrypts your traffic, protecting your data from interception.",
                    IsTrueFalse  = false
                },

                new QuizQuestion
                {
                    Question     = "What is two-factor authentication (2FA)?",
                    Options      = new[] { "A) Using two passwords", "B) A second security layer beyond just a password", "C) Logging in from two devices", "D) Encrypting files twice" },
                    CorrectIndex = 1,
                    Explanation  = "2FA adds a second verification step, making accounts much harder to hack.",
                    IsTrueFalse  = false
                },

                new QuizQuestion
                {
                    Question     = "Which of these is a sign of a phishing website?",
                    Options      = new[] { "A) HTTPS in the URL", "B) A padlock icon in the browser", "C) Slightly misspelled domain (e.g. paypa1.com)", "D) A professional-looking design" },
                    CorrectIndex = 2,
                    Explanation  = "Phishing sites often use misspelled domains to trick users.",
                    IsTrueFalse  = false
                },

                new QuizQuestion
                {
                    Question     = "What is social engineering in cybersecurity?",
                    Options      = new[] { "A) Building social media apps", "B) Manipulating people into revealing confidential info", "C) Engineering better social networks", "D) Fixing software bugs" },
                    CorrectIndex = 1,
                    Explanation  = "Social engineering exploits human psychology rather than technical vulnerabilities.",
                    IsTrueFalse  = false
                },

                new QuizQuestion
                {
                    Question     = "What should you do before connecting to public Wi-Fi?",
                    Options      = new[] { "A) Share your location", "B) Disable your firewall", "C) Use a VPN", "D) Log into your bank account" },
                    CorrectIndex = 2,
                    Explanation  = "A VPN protects your data on unsecured public networks.",
                    IsTrueFalse  = false
                },

                new QuizQuestion
                {
                    Question     = "Which action best protects your accounts after a data breach?",
                    Options      = new[] { "A) Wait and see", "B) Change your password immediately", "C) Delete the account app", "D) Log out once" },
                    CorrectIndex = 1,
                    Explanation  = "Immediately changing your password limits the damage of a data breach.",
                    IsTrueFalse  = false
                },


                // true / false 
                new QuizQuestion
                {
                    Question     = "True or False: You should use the same password for all your accounts to make it easier to remember.",
                    Options      = new[] { "True", "False" },
                    CorrectIndex = 1,
                    Explanation  = "Reusing passwords means one breach can compromise all your accounts. Use unique passwords.",
                    IsTrueFalse  = true
                },

                new QuizQuestion
                {
                    Question     = "True or False: A firewall can help block unauthorized access to your network.",
                    Options      = new[] { "True", "False" },
                    CorrectIndex = 0,
                    Explanation  = "Firewalls monitor and control network traffic, blocking suspicious connections.",
                    IsTrueFalse  = true
                },

                new QuizQuestion
                {
                    Question     = "True or False: Clicking a link in an email is always safe if the email looks professional.",
                    Options      = new[] { "True", "False" },
                    CorrectIndex = 1,
                    Explanation  = "Phishing emails are often designed to look professional. Always verify the sender.",
                    IsTrueFalse  = true
                },

                new QuizQuestion
                {
                    Question     = "True or False: Updating your software regularly helps protect against known security vulnerabilities.",
                    Options      = new[] { "True", "False" },
                    CorrectIndex = 0,
                    Explanation  = "Software updates often include security patches that fix known vulnerabilities.",
                    IsTrueFalse  = true
                },

                new QuizQuestion
                {
                    Question     = "True or False: Malware can only enter your device through email attachments.",
                    Options      = new[] { "True", "False" },
                    CorrectIndex = 1,
                    Explanation  = "Malware can enter via downloads, websites, USB drives, and more — not just email.",
                    IsTrueFalse  = true
                }

            };

        }// end load_questions


        // Prepare a shuffled game with QUESTIONS_PER_GAME questions
        public void prepare_game()
        {// start prepare_game

            CurrentIndex = 0;
            Score = 0;

            // Shuffle the full pool
            Random rng = new Random();
            List<QuizQuestion> shuffled = new List<QuizQuestion>(allQuestions);

            for (int i = shuffled.Count - 1; i > 0; i--)
            {
                int j = rng.Next(i + 1);
                var temp = shuffled[i];
                shuffled[i] = shuffled[j];
                shuffled[j] = temp;
            }

            // Take the first QUESTIONS_PER_GAME
            ActiveQuestions = shuffled.GetRange(
                0,
                Math.Min(QUESTIONS_PER_GAME, shuffled.Count)
            );

        }// end prepare_game


        // Get current question
        public QuizQuestion get_current_question()
        {// start get_current_question

            if (CurrentIndex < ActiveQuestions.Count)
                return ActiveQuestions[CurrentIndex];

            return null;

        }// end get_current_question


        // Submit an answer — returns feedback string
        public string submit_answer(int selectedIndex)
        {// start submit_answer

            QuizQuestion q = get_current_question();

            if (q == null)
                return "No active question.";

            bool correct = selectedIndex == q.CorrectIndex;

            if (correct)
            {
                Score++;
                CurrentIndex++;
                return $"✅ Correct! {q.Explanation}";
            }
            else
            {
                string correctAnswer = q.Options[q.CorrectIndex];
                CurrentIndex++;
                return $"❌ Wrong! The correct answer was: {correctAnswer}. {q.Explanation}";
            }

        }// end submit_answer


        // Check if quiz is finished
        public bool is_finished()
        {
            return CurrentIndex >= ActiveQuestions.Count;
        }


        // Get the final score message
        public string get_final_message()
        {// start get_final_message

            int total = ActiveQuestions.Count;
            double percent = (double)Score / total * 100;

            string feedback;

            if (percent >= 90)
                feedback = "🏆 Outstanding! You're a cybersecurity pro!";
            else if (percent >= 70)
                feedback = "👍 Great job! You have solid cybersecurity knowledge!";
            else if (percent >= 50)
                feedback = "📚 Not bad! Keep learning to stay safe online.";
            else
                feedback = "⚠️ Keep learning to stay safe online! Review the topics and try again.";

            return $"Quiz Complete!\nYou scored {Score} out of {total} ({percent:0}%).\n\n{feedback}";

        }// end get_final_message


        // Number of questions in the active game
        public int TotalQuestions => ActiveQuestions.Count;

    }// end of quiz_manager class

}// end of namespace