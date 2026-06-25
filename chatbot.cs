using PortfolioOfEvidence_Part3;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;

namespace PortfolioOfEvidence_Part3
{// start of namespace

    public class chatbot
    {// start of chatbot class

        // ArrayList for chatbot replies
        ArrayList reply;

        // ArrayList for ignored words
        ArrayList ignore;

        // Chat display ListBox
        ListBox chats;

        auto_interest reminder;



        // Constructor
        public chatbot(ArrayList reply, ArrayList ignore, ListBox chats)
        {// start constructor

            this.reply = reply;
            this.ignore = ignore;
            this.chats = chats;

            reminder = new auto_interest(chats);

        }// end constructor


        // Chatbot response method
        public void ai_check(string question, string username)
        {// start of ai_check method

            // Remove special characters
            question = special_character.RemoveSpecialCharacters(question);

            // Check if question is empty
            if (string.IsNullOrWhiteSpace(question))
            {
                message_display.show(
                    chats,
                    "ChatBot",
                    "Please enter a valid question."
                );

                return;
            }

            // Split question into words
            string[] words =
                question.ToLower().Split(
                    new char[]
                    {
                        ' ',
                        ',',
                        '.',
                        '?',
                        '!'
                    },
                    StringSplitOptions.RemoveEmptyEntries
                );

            // Store user's interests
            string interestResponse =
                interest_manager.store_interest(
                    words,
                    username,
                    ignore
                );

            // Display interest confirmation
            if (!string.IsNullOrEmpty(interestResponse))
            {
                message_display.show(
                    chats,
                    "ChatBot",
                    interestResponse
                );
            }

            // Check if question is follow-up
            bool followUp =
                conversation_memory.is_followup(words);

            // Continue previous topic
            if (followUp)
            {
                question =
                    conversation_memory.continue_topic(
                        question
                    );

                // Rebuild words
                words =
                    question.ToLower().Split(
                        new char[]
                        {
                            ' ',
                            ',',
                            '.',
                            '?',
                            '!'
                        },
                        StringSplitOptions.RemoveEmptyEntries
                    );
            }

            // Random object
            Random random = new Random();

            // Store final responses
            List<string> finalResponses =
                new List<string>();

            // Check every word
            foreach (string word in words)
            {// start of foreach loop

                // Ignore useless words
                if (
                    word.Length < 3 ||
                    ignore.Contains(word)
                )
                {
                    continue;
                }

                // List for topic responses
                List<string> topicResponses =
                    new List<string>();

                // Search every response
                foreach (string item in reply)
                {
                    string text = item.ToString();

                    string[] parts =
                        text.Split(':');

                    if (parts.Length < 2)
                    {
                        continue;
                    }

                    string keyword = parts[0];
                    string response = parts[1];

                    if (keyword == word)
                    {
                        topicResponses.Add(response);
                    }
                }

                // If responses exist
                if (topicResponses.Count > 0)
                {
                    string selected =
                        topicResponses[
                            random.Next(
                                topicResponses.Count
                            )
                        ];

                    finalResponses.Add(selected);

                    // Save topic memory
                    conversation_memory.save_topic(
                        word
                    );
                }
            }// end of foreach loop

            // Remove duplicate responses
            finalResponses =
                finalResponses
                .Distinct()
                .ToList();

            // Display response
            if (finalResponses.Count > 0)
            {
                string finalMessage =
                    string.Join(
                        " ",
                        finalResponses
                    );

                message_display.show(
                    chats,
                    "ChatBot",
                    finalMessage
                );
            }
            else
            {
                string[] fallback =
                {
                    "I don't understand that.",
                    "Please ask another question.",
                    "Can you rephrase that?",
                    "I could not find an answer for that topic."
                };

                string randomFallback =
                    fallback[
                        random.Next(
                            fallback.Length
                        )
                    ];

                message_display.show(
                    chats,
                    "ChatBot",
                    randomFallback
                );
            }

            // Show automatic interest reminder
            reminder.auto_show_interest(username);

        }// end of ai_check method


    }// end chatbot class

}// end namespace