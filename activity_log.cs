using System;
using System.Collections.Generic;
using System.Linq;

namespace PortfolioOfEvidence_Part3
{// start of namespace

    // Represents a single logged action
    public class LogEntry
    {// start of LogEntry class

        public string Description { get; set; }
        public DateTime Timestamp { get; set; }

        // Display string for the log list
        public string Display =>
            $"[{Timestamp:HH:mm:ss}]  {Description}";

    }// end of LogEntry class


    // Manages the activity log for the chatbot
    public class activity_log
    {// start of activity_log class

        // Maximum entries to show by default
        private const int MAX_DISPLAY = 10;

        // Full log history (static so it persists across the session)
        private static List<LogEntry> log = new List<LogEntry>();


        // Add an entry to the log
        public static void add(string description)
        {// start of add method

            log.Add(new LogEntry
            {
                Description = description,
                Timestamp = DateTime.Now
            });

        }// end of add method


        // Get the last MAX_DISPLAY entries (newest first)
        public static List<LogEntry> get_recent()
        {// start of get_recent method

            return log
                .AsEnumerable()
                .Reverse()
                .Take(MAX_DISPLAY)
                .ToList();

        }// end of get_recent method


        // Get ALL entries (newest first)
        public static List<LogEntry> get_all()
        {// start of get_all method

            return log
                .AsEnumerable()
                .Reverse()
                .ToList();

        }// end of get_all method


        // Get total number of logged entries
        public static int Count => log.Count;


        // Format last entries as a readable summary string
        public static string get_summary()
        {// start of get_summary method

            List<LogEntry> recent = get_recent();

            if (recent.Count == 0)
                return "No actions have been recorded yet.";

            var lines = new System.Text.StringBuilder();
            lines.AppendLine($"Here's a summary of recent actions ({recent.Count} shown):");

            int number = 1;
            foreach (LogEntry entry in recent)
            {
                lines.AppendLine($"  {number}. {entry.Display}");
                number++;
            }

            return lines.ToString().Trim();

        }// end of get_summary method


        // Clear all log entries
        public static void clear()
        {
            log.Clear();
        }

    }// end of activity_log class

}// end of namespace