using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace PortfolioOfEvidence_Part3
{// start of namespase
    public class message_display
    {// stat of message dispaly class
        public static void show(ListBox chats, string name, string message)
        {// start of a constructor

            // create message border
            Border messageborder = new Border
            {
                Margin = new Thickness(8),
                Padding = new Thickness(12),

                // Rounded corners
                CornerRadius = new CornerRadius(20),

                // Border thickness
                BorderThickness = new Thickness(2),

                // Width settings
                MaxWidth = 500,

                // Align chatbot left and user right
                HorizontalAlignment =
                    name.ToLower().Contains("chat")
                    ? HorizontalAlignment.Left
                    : HorizontalAlignment.Right
            };

            // set chatbot colors and styling
            if (name.ToLower().Contains("chat"))
            {
                // chatbot background
                messageborder.Background =
                    new SolidColorBrush(Color.FromRgb(26, 26, 46));

                // DeepSkyBlue border
                messageborder.BorderBrush = Brushes.DeepSkyBlue;

            }// end of if statement
            else
            {// start of else statement

                // user message background
                messageborder.Background =
                    new SolidColorBrush(Color.FromRgb(30, 58, 138));


                // Light blue border
                messageborder.BorderBrush =
                    new SolidColorBrush(Color.FromRgb(56, 189, 248));

            }// end of else statement


            // craete textblock
            TextBlock text = new TextBlock
            {
                TextWrapping = TextWrapping.Wrap,
                FontSize = 14,
                FontFamily = new FontFamily("Segoe UI"),
                Margin = new Thickness(3)
            };


            // Username color
            Brush nameColor =
                name.ToLower().Contains("chat")
                ? Brushes.DeepSkyBlue
                : Brushes.White;

            // Message color
            Brush messageColor = new SolidColorBrush(Color.FromRgb(184, 184, 197));

            // Add username
            text.Inlines.Add(
                new Run
                {
                    Text = name + ": ",
                    Foreground = nameColor,
                    FontWeight = FontWeights.Bold,
                    FontSize = 15
                }
            );


            // Add message
            text.Inlines.Add(
                new Run
                {
                    Text = message,
                    Foreground = messageColor
                }
            );

            // Put text inside border
            messageborder.Child = text;

            // Add message to chat list
            chats.Items.Add(messageborder);


            // Auto-scroll to newest message
            chats.ScrollIntoView(chats.Items[chats.Items.Count - 1]);

        }// end of a constructor

    }// end of a message display class

}// end of namespase