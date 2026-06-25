using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace PortfolioOfEvidence_Part3
{// start of namespace

    // Enum for detected user intent
    public enum UserIntent
    {
        AddTask,
        SetReminder,
        ViewTasks,
        StartQuiz,
        ShowActivityLog,
        CybersecurityQuery,
        Unknown
    }


    // Result of NLP processing
    public class NlpResult
    {// start of NlpResult class

        public UserIntent Intent { get; set; }
        public string ExtractedText { get; set; }
        public string ReminderTime { get; set; }

    }// end of NlpResult class


    // NLP Processor — detects intent from natural language input
    public class nlp_processor
    {// start of nlp_processor class

        // Keywords that indicate "add task" intent
        private static readonly string[] addTaskKeywords =
        {
            "add task", "create task", "new task", "add a task",
            "set a task", "make a task", "remind me to", "add reminder",
            "i need to", "schedule task", "add to do", "set up task",
            "enable 2fa", "enable two-factor", "review privacy",
            "update password", "check firewall", "set up vpn",
            "add todo", "create a reminder"
        };

        // Keywords that indicate "set reminder" intent
        private static readonly string[] reminderKeywords =
        {
            "remind me", "set a reminder", "reminder for",
            "remind me in", "remind me on", "set reminder",
            "don't let me forget", "alert me"
        };

        // Keywords that indicate "view tasks" intent
        private static readonly string[] viewTaskKeywords =
        {
            "show tasks", "view tasks", "list tasks", "my tasks",
            "show my tasks", "what are my tasks", "pending tasks",
            "show to do", "task list", "all tasks", "view my tasks"
        };

        // Keywords that indicate "start quiz" intent
        private static readonly string[] quizKeywords =
        {
            "start quiz", "begin quiz", "play quiz", "take quiz",
            "quiz me", "test me", "cyber quiz", "start the quiz",
            "cybersecurity quiz", "quiz", "test my knowledge"
        };

        // Keywords that indicate "show activity log" intent
        private static readonly string[] logKeywords =
        {
            "show activity log", "activity log", "what have you done",
            "show log", "recent actions", "what did you do",
            "history", "show history", "log", "show recent"
        };

        // Cybersecurity topic keywords (route to existing chatbot)
        private static readonly string[] cyberKeywords =
        {
            "phishing", "password", "firewall", "vpn", "malware",
            "hacking", "fraud", "cybersecurity", "security",
            "virus", "ransomware", "encryption", "breach",
            "two-factor", "2fa", "scam", "threat", "attack"
        };


        // Main method — analyse input and return NlpResult
        public static NlpResult analyse(string input)
        {// start of analyse method

            if (string.IsNullOrWhiteSpace(input))
                return new NlpResult { Intent = UserIntent.Unknown };

            string lower = input.ToLower().Trim();

            // 1. Check for activity log intent
            if (matches_any(lower, logKeywords))
            {
                return new NlpResult { Intent = UserIntent.ShowActivityLog };
            }

            // 2. Check for quiz intent
            if (matches_any(lower, quizKeywords))
            {
                return new NlpResult { Intent = UserIntent.StartQuiz };
            }

            // 3. Check for view tasks intent
            if (matches_any(lower, viewTaskKeywords))
            {
                return new NlpResult { Intent = UserIntent.ViewTasks };
            }

            // 4. Check for reminder intent (before add task — more specific)
            if (matches_any(lower, reminderKeywords))
            {
                string extracted = extract_after_keyword(lower, reminderKeywords);
                string time = extract_time(lower);

                return new NlpResult
                {
                    Intent = UserIntent.SetReminder,
                    ExtractedText = extracted,
                    ReminderTime = time
                };
            }

            // 5. Check for add task intent
            if (matches_any(lower, addTaskKeywords))
            {
                string extracted = extract_task_title(lower);

                return new NlpResult
                {
                    Intent = UserIntent.AddTask,
                    ExtractedText = extracted
                };
            }

            // 6. Cybersecurity query — pass to existing chatbot
            if (matches_any(lower, cyberKeywords))
            {
                return new NlpResult { Intent = UserIntent.CybersecurityQuery };
            }

            // 7. Unknown intent
            return new NlpResult { Intent = UserIntent.Unknown };

        }// end of analyse method


        // Check if input contains any of the given keywords
        private static bool matches_any(string input, string[] keywords)
        {
            foreach (string kw in keywords)
            {
                if (input.Contains(kw))
                    return true;
            }
            return false;
        }


        // Extract task title from common patterns
        private static string extract_task_title(string input)
        {// start extract_task_title

            // Patterns to strip from the beginning
            string[] stripPhrases =
            {
                "add a task to", "add task to", "create a task to",
                "create task to", "new task to", "add task",
                "create task", "new task", "set a task to",
                "i need to", "remind me to", "add reminder to",
                "schedule task to", "add to do"
            };

            foreach (string phrase in stripPhrases)
            {
                if (input.Contains(phrase))
                {
                    int idx = input.IndexOf(phrase) + phrase.Length;
                    string result = input.Substring(idx).Trim();

                    // Capitalise first letter
                    if (result.Length > 0)
                        return char.ToUpper(result[0]) + result.Substring(1);
                }
            }

            // Fallback — use input as title (clean it up)
            return capitalise(input);

        }// end extract_task_title


        // Extract text after matched keyword
        private static string extract_after_keyword(string input, string[] keywords)
        {
            foreach (string kw in keywords)
            {
                if (input.Contains(kw))
                {
                    int idx = input.IndexOf(kw) + kw.Length;
                    string after = input.Substring(idx).Trim();

                    // Remove leading "me" or "to"
                    after = Regex.Replace(after, @"^(me\s+|to\s+)", "").Trim();

                    return capitalise(after);
                }
            }
            return input;
        }


        // Extract time reference from input
        private static string extract_time(string input)
        {// start extract_time

            // Common time patterns
            string[] patterns =
            {
                @"in (\d+) days?",
                @"in (\d+) hours?",
                @"in (\d+) weeks?",
                @"tomorrow",
                @"next week",
                @"on (\w+ \d+)",
                @"(\d{4}-\d{2}-\d{2})",
                @"(\d+/\d+/\d+)"
            };

            foreach (string pattern in patterns)
            {
                Match m = Regex.Match(input, pattern);
                if (m.Success)
                    return m.Value;
            }

            return "No specific time set";

        }// end extract_time


        // Capitalise first letter of a string
        private static string capitalise(string s)
        {
            if (string.IsNullOrWhiteSpace(s)) return s;
            return char.ToUpper(s[0]) + s.Substring(1);
        }

    }// end of nlp_processor class

}// end of namespace