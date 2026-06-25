using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace PortfolioOfEvidence_Part3
{// start of namespace
    public class user_name
    {// start username class

        public string submit_name(TextBox user_name, ListView chats)
        {//start of submit name method

            //temp variables 
            string filename = "user_names.txt";

            //check if the filename exists or not , then auto create
            if (!File.Exists(filename))
            {// start of if statement
                //auto create the file using AppendAllText() function
                File.AppendAllText(filename, "auto_create\n");

            }//end if statement

            //temp variables
            string name = user_name.Text.ToString();
            bool found = check_name(name);

            //check if the user is found or not and write the name in a text file
            if (!found)
            {//start of if
                //write the name in a text file
                File.AppendAllText(filename, name + "\n");
                //then welcome the user
                error_method("ChatBot", "Hey " + name + "  welcome to AI cybersecurity", chats);

            }//end of if
            else
            {//start of else

                //welcome the user back
                error_method("ChatBot ", "Hey " + name + " welcome back, how can i help you today", chats);
            }//end of else


            //return name
            return name;


        }//end of username method



        //method to check name of the user
        private Boolean check_name(string name)
        {//start of check name method

            //temp variable
            string filename = "user_names.txt";

            //boolean variable to check if the name is found or not
            bool found_name = false;


            //store or get all the names in the text file and store in an 1D array
            string[] names = File.ReadAllLines(filename);

            //foreach to search the name of the user
            foreach (string name_found in names)
            { //start of loop

                //if statement to check for the username
                if (name_found.ToLower() == name.ToLower())
                {//start if

                    //found_name set to true
                    found_name = true;

                }//end of if

            }//end of the loop


            //return the status of found or not [ true or false ]
            return found_name;

        }//end check name method



        // Error method
        // This method displays styled messages in the chat ListView
        private void error_method(
            string name,
            string message,
            ListView chats
        )
        {// start of error method

            // Create message border
            Border messageBorder = new Border
            {
                Margin = new Thickness(8),
                Padding = new Thickness(12),
                CornerRadius = new CornerRadius(20),
                BorderThickness = new Thickness(2),
                MaxWidth = 500,

                // Align chatbot left and user right
                HorizontalAlignment =
                    name.ToLower().Contains("chat")
                    ? HorizontalAlignment.Left : HorizontalAlignment.Right
            };

            // ChatBot styling
            if (
                name.ToLower().Contains("chatbot") ||
                name.ToLower().Contains("chat")
            )
            {// Start of if statement

                // Dark chatbot background
                messageBorder.Background =
                    new SolidColorBrush(
                        Color.FromRgb(26, 26, 46)
                    );

                // DeepSkyBlue border
                messageBorder.BorderBrush =
                    Brushes.DeepSkyBlue;
            }// end of if statement
            else
            {// User start of else statement

                // User message background
                messageBorder.Background =
                    new SolidColorBrush(
                        Color.FromRgb(30, 58, 138)
                    );

                // Light blue border
                messageBorder.BorderBrush =
                    new SolidColorBrush(Color.FromRgb(56, 189, 248)
                    );
            }// end of else statement


            // Create textblock
            TextBlock messageText = new TextBlock
            {
                TextWrapping = TextWrapping.Wrap,
                Margin = new Thickness(3),
                FontSize = 14,
                FontFamily = new FontFamily("Segoe UI")
            };


            // Username color
            Brush nameColor =
                (
                    name.ToLower().Contains("chatbot") ||
                    name.ToLower().Contains("chat")
                )
                ? Brushes.DeepSkyBlue : Brushes.White;


            // Message text color
            Brush messageColor =
                new SolidColorBrush(Color.FromRgb(184, 184, 197));

            // Add username
            messageText.Inlines.Add(
                new Run
                {
                    Text = name + ": ",
                    Foreground = nameColor,
                    FontWeight = FontWeights.Bold,
                    FontSize = 15
                }
            );

            // Add message
            messageText.Inlines.Add(
                new Run
                {
                    Text = message,
                    Foreground = messageColor
                }
            );


            // Put text inside border
            messageBorder.Child = messageText;

            // Add message to chat list
            chats.Items.Add(messageBorder);

        }// end of error method


    }// end of user name class


}// end of namespace