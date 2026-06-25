using PortfolioOfEvidence_Part3;
using System;
using System.IO;
using System.Windows.Controls;

namespace PortfolioOfEvidence_Part3
{// start of namespace
    class auto_interest
    {// start of auto interest class

        // counter variable
        int counting = 0;

        // chat listbox
        ListBox chats;

        // constructor
        public auto_interest(ListBox chats)
        {
            this.chats = chats;
        }

        // method to automatically show user interest after 3 interactions
        public void auto_show_interest(string username)
        {
            // Increase counter
            counting++;



            // Trigger every 3 messages
            if (counting >= 3)
            { // start of if statement to check counter

                string filename = "interested_topic.txt";

                // Check if file exists
                if (File.Exists(filename))
                {// start of if statement to check file existence

                    // Read all lines
                    string[] lines = File.ReadAllLines(filename);


                    // Find current user
                    foreach (string line in lines)
                    {// start of foreach loop to find user

                        if (line.StartsWith(username))
                        {// start of if statement to find user

                            // Get interests only
                            string interests =
                                line.Replace(
                                    username +
                                    " interested in:",
                                    ""
                                ).Trim();

                            // Split interests
                            string[] topicList =
                                interests.Split(',');

                            // Pick random topic
                            Random random = new Random();


                            string selectedTopic =
                                topicList[
                                    random.Next(
                                        topicList.Length
                                    )
                                ].Trim().ToLower();

                            // Variable to store recall response
                            string recallMessage = "";

                            // Personalized responses
                            if (selectedTopic.Contains("phishing"))
                            {
                                recallMessage = "Reminder: As someone interested in phishing, remember to avoid clicking suspicious links and always verify email senders.";

                            }
                            else if (selectedTopic.Contains("password"))
                            {
                                recallMessage = "Reminder: As someone interested in password security, remember to use strong passwords and enable two-factor authentication.";

                            }
                            else if (selectedTopic.Contains("firewall"))
                            {
                                recallMessage = "Reminder: As someone interested in firewalls, make sure your firewall is always enabled to help block unauthorized access.";

                            }
                            else if (selectedTopic.Contains("cybersecurity"))
                            {
                                recallMessage = "Reminder: As someone interested in cybersecurity, staying updated on new cyber threats can help keep your data safe.";

                            }
                            else if (selectedTopic.Contains("fraud"))
                            {
                                recallMessage = "Reminder: As someone interested in fraud prevention, always verify transactions and never share sensitive information online.";

                            }
                            else if (selectedTopic.Contains("vpn"))
                            {
                                recallMessage = "Reminder: As someone interested in VPNs, using a trusted VPN can improve your privacy and protect your online activity.";

                            }
                            else
                            {
                                // Default response
                                recallMessage =
                                    "As someone interested in " +
                                    selectedTopic +
                                    ", you should regularly update your security settings and stay informed about cyber threats.";
                            }


                            // Display memory recall
                            message_display.show(
                                chats,
                                "ChatBot",
                                recallMessage
                            );

                            // Reset counter after showing interest
                            break;

                        }// end of if statement to find user

                    }// end of foreach loop to find user
                }// end of if statement to check file existence

                // Reset counter
                counting = 0;

            }// end of if statement to check counter


        }// end of auto show interest method

    }// end of auto interest class

}// end of namespace