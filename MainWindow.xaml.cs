using Org.BouncyCastle.Ocsp;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PortfolioOfEvidence_Part3
{// start of namespace

    public partial class MainWindow : Window
    {// start  of mainWindow

        // arrayList to store chatbot responses
        ArrayList reply = new ArrayList();

        // ArrayList to store ignored words
        ArrayList ignore = new ArrayList();

        // object for username validation and memory recall
        user_name check_name = new user_name();

        // object for chatbot processing
        chatbot bot;

        // object for automatic interest reminders
        auto_interest interestReminder;

        // global username variable
        string username = "";


        // remembers the last task waiting for a reminder
        private string pendingReminderTask = "";

        // quiz manager object
        quiz_manager quiz;

        // track if user has answered current question
        bool quizAnswered = false;

        public MainWindow()
        {// start of constructor

            InitializeComponent();

            // Part 2 setup - unchanged
            new respond(reply, ignore) { };
            new voice_greet();

            bot = new chatbot(reply, ignore, chats);
            interestReminder = new auto_interest(chats);

            // Part 3 setup
            task_manager.initialize_db();
            activity_log.add("Application started.");

        }// end of constructor


        // SPLASH + NAME SCREENS - unchanged from Part 2

        // start app event handler
        private void start_app(object sender, RoutedEventArgs e)
        {// start of start_app method

            // set the greet_grid to collapse
            greet_grid.Visibility = Visibility.Collapsed;

            // set the welcome_grid to hidden
            welcome_grid.Visibility = Visibility.Hidden;

            // set the name_grid to visible
            name_grid.Visibility = Visibility.Visible;

        }// end of start_app method


        // submit name event handler
        private void submit_name(object sender, RoutedEventArgs e)
        {// start of submit_name method

            // check if the user has entered the name
            if (string.IsNullOrWhiteSpace(user_name.Text))
            {// start of if statement

                // display error message
                MessageBox.Show("Please enter your name before continuing.");

                // keep focus on textbox
                user_name.Focus();

                // stop method
                return;

            }// end of if statement

            // check the user name from memory recall
            username = check_name.submit_name(user_name, chats);

            // log the login
            activity_log.add($"User '{username}' logged in.");

            // hide username page grid and set chatting grid visible
            name_grid.Visibility = Visibility.Hidden;
            chatting_grid.Visibility = Visibility.Visible;

            // highlight chat tab as default active tab
            set_active_tab(tab_chat);

        }// end of submit_name method


        // TAB NAVIGATION - Part 3

        // chat tab click
        private void tab_chat_click(object sender, RoutedEventArgs e)
        {// start of tab_chat_click

            show_panel(panel_chat);
            set_active_tab(tab_chat);

        }// end of tab_chat_click


        // task manager tab click
        private void tab_tasks_click(object sender, RoutedEventArgs e)
        {// start of tab_tasks_click

            show_panel(panel_tasks);
            set_active_tab(tab_tasks);
            load_tasks();

        }// end of tab_tasks_click


        // quiz tab click
        private void tab_quiz_click(object sender, RoutedEventArgs e)
        {// start of tab_quiz_click

            show_panel(panel_quiz);
            set_active_tab(tab_quiz);

        }// end of tab_quiz_click


        // activity log tab click
        private void tab_log_click(object sender, RoutedEventArgs e)
        {// start of tab_log_click

            show_panel(panel_log);
            set_active_tab(tab_log);
            load_log();

        }// end of tab_log_click


        // show one panel and collapse all others
        private void show_panel(Grid target)
        {// start of show_panel method

            panel_chat.Visibility = Visibility.Collapsed;
            panel_tasks.Visibility = Visibility.Collapsed;
            panel_quiz.Visibility = Visibility.Collapsed;
            panel_log.Visibility = Visibility.Collapsed;

            target.Visibility = Visibility.Visible;

        }// end of show_panel method


        // highlight the active tab and reset all others
        private void set_active_tab(Button active)
        {// start of set_active_tab method

            // all tab buttons
            Button[] tabs = { tab_chat, tab_tasks, tab_quiz, tab_log };

            // reset all tabs to inactive style
            foreach (Button b in tabs)
            {// start of foreach loop

                b.Background = Brushes.Transparent;
                b.Foreground = new SolidColorBrush(
                    Color.FromRgb(184, 184, 197)
                );

            }// end of foreach loop

            // set active tab style
            active.Background = new SolidColorBrush(
                Color.FromRgb(30, 58, 138)
            );
            active.Foreground = Brushes.White;

        }// end of set_active_tab method


        // CHAT PANEL - upgraded with NLP for Part 3

        // send question event handler
        private void send(object sender, RoutedEventArgs e)
        {// start of send event handler

            string rawQuestion = question.Text.Trim();

            // check if the question is empty
            if (string.IsNullOrWhiteSpace(rawQuestion))
            {// start of if statement

                message_display.show(chats, "ChatBot", "Please enter a question.");
                return;

            }// end of if statement

            // user is answering the reminder question
            if (!string.IsNullOrWhiteSpace(pendingReminderTask))
            {// start of if statement
                if (rawQuestion.ToLower().Contains("yes"))
                {// start of if statement
                    message_display.show(
                        chats,
                        "ChatBot",
                        "Great! Tell me when you would like to be reminded.\nExample:\nTomorrow at 3 PM"
                    );
                    // wait for the next input to set the reminder
                    return;
                }// end of if statemt
            }// end of if statement

            // display user message in the chat area
            message_display.show(chats, username, rawQuestion);

            // clear textbox after sending
            question.Clear();

            // run NLP processor first to detect intent
            NlpResult nlp = nlp_processor.analyse(rawQuestion);

            // route based on detected intent
            switch (nlp.Intent)
            {// start of switch

                case UserIntent.AddTask:
                    handle_nlp_add_task(nlp);
                    break;

                case UserIntent.SetReminder:
                    handle_nlp_reminder(nlp);
                    break;

                case UserIntent.ViewTasks:
                    handle_nlp_view_tasks();
                    break;

                case UserIntent.StartQuiz:
                    message_display.show(
                        chats, "ChatBot",
                        "Sure! Click the 🎮 Cyber Quiz tab at the top to start the quiz."
                    );
                    activity_log.add("NLP: User asked to start quiz via chat.");
                    break;

                case UserIntent.ShowActivityLog:
                    string summary = activity_log.get_summary();
                    message_display.show(chats, "ChatBot", summary);
                    activity_log.add("NLP: User requested activity log via chat.");
                    break;

                // fall through to existing chatbot for cybersecurity queries
                case UserIntent.CybersecurityQuery:
                case UserIntent.Unknown:
                default:
                    bot.ai_check(rawQuestion, username);
                    interestReminder.auto_show_interest(username);
                    break;

            }// end of switch

        }// end of send event handler


        // NLP: handle add task from chat
        private void handle_nlp_add_task(NlpResult nlp)
        {// start of handle_nlp_add_task method

            // use extracted text or a default title
            string title = string.IsNullOrWhiteSpace(nlp.ExtractedText)
                           ? "New cybersecurity task"
                           : nlp.ExtractedText;
            
            // remember the last task waiting for a reminder
            pendingReminderTask = title;

            bool ok = task_manager.quick_add_task(title);

            if (ok)
            {// start of if statement

                message_display.show(
                    chats, "ChatBot",
                    $"Task added: '{title}'.\n" +
                    "Would you like to set a reminder? " +
                    "Go to the 📋 Task Manager tab to manage your tasks."
                );

                activity_log.add($"NLP: Task added via chat — '{title}'.");

            }// end of if statement
            else
            {// start of else statement

                message_display.show(
                    chats, "ChatBot",
                    "Sorry, I could not save that task. Please use the Task Manager tab."
                );

            }// end of else statement

        }// end of handle_nlp_add_task method


        // NLP: handle set reminder from chat
        private void handle_nlp_reminder(NlpResult nlp)
        {// start of handle_nlp_reminder method

            string taskText = string.IsNullOrWhiteSpace(pendingReminderTask)
                                ?
                                nlp.ExtractedText
                                : pendingReminderTask;

            string reminderTime = string.IsNullOrWhiteSpace(nlp.ReminderTime)
                                  ? "No specific time set"
                                  : nlp.ReminderTime;

            bool ok = task_manager.quick_set_reminder(taskText, reminderTime);

            if (ok)
            {// start of if statement

                message_display.show(
                    chats, "ChatBot",
                    $"Got it! Reminder set for '{taskText}' — {reminderTime}."
                );

                activity_log.add(
                    $"NLP: Reminder set via chat — '{taskText}' on {reminderTime}."
                );

            }// end of if statement
            else
            {// start of else statement

                message_display.show(
                    chats, "ChatBot",
                    "Sorry, I could not set that reminder. Please use the Task Manager tab."
                );

            }// end of else statement

        }// end of handle_nlp_reminder method


        // NLP: show task summary in chat
        private void handle_nlp_view_tasks()
        {// start of handle_nlp_view_tasks method

            List<CyberTask> tasks = task_manager.get_all_tasks();

            // check if there are any tasks
            if (tasks.Count == 0)
            {// start of if statement

                message_display.show(
                    chats, "ChatBot",
                    "You have no tasks yet. Add tasks in the 📋 Task Manager tab."
                );
                return;

            }// end of if statement

            // show up to 5 tasks in chat
            int shown = Math.Min(tasks.Count, 5);
            string msg = $"Here are your latest {shown} task(s):\n";

            for (int i = 0; i < shown; i++)
            {// start of for loop

                CyberTask t = tasks[i];
                string status = t.IsCompleted ? "✅" : "⏳";
                msg += $"  {i + 1}. {status} {t.Title}";

                if (!string.IsNullOrWhiteSpace(t.Reminder))
                    msg += $" (Reminder: {t.Reminder})";

                msg += "\n";

            }// end of for loop

            message_display.show(chats, "ChatBot", msg.Trim());
            activity_log.add("NLP: Task list viewed via chat.");

        }// end of handle_nlp_view_tasks method


        // TASK MANAGER PANEL - Part 3

        // add task button click
        private void add_task_click(object sender, RoutedEventArgs e)
        {// start of add_task_click method

            string title = task_title.Text.Trim();
            string desc = task_desc.Text.Trim();
            string reminder = task_reminder.Text.Trim();

            // validate title
            if (string.IsNullOrWhiteSpace(title))
            {// start of if statement

                MessageBox.Show(
                    "Please enter a task title.",
                    "Missing Title",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning
                );
                return;

            }// end of if statement

            // default description if empty
            if (string.IsNullOrWhiteSpace(desc))
                desc = "No description provided.";

            // save to database
            bool ok = task_manager.add_task(title, desc, reminder);

            if (ok)
            {// start of if statement

                MessageBox.Show(
                    $"Task '{title}' added successfully!",
                    "Task Added",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information
                );

                // log the action
                activity_log.add(
                    $"Task added: '{title}'" +
                    (string.IsNullOrWhiteSpace(reminder)
                        ? "."
                        : $" | Reminder: {reminder}.")
                );

                // clear pending reminder task
                pendingReminderTask = "";

                // clear input fields
                task_title.Clear();
                task_desc.Clear();
                task_reminder.Clear();

                // refresh task list
                load_tasks();

            }// end of if statement
            else
            {// start of else statement

                MessageBox.Show(
                    "Failed to add task. Please try again.",
                    "Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error
                );

            }// end of else statement

        }// end of add_task_click method


        // complete task button click
        private void complete_task_click(object sender, RoutedEventArgs e)
        {// start of complete_task_click method

            // check if a task is selected
            if (task_list.SelectedItem == null)
            {// start of if statement

                MessageBox.Show(
                    "Please select a task to mark as complete.",
                    "No Selection",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning
                );
                return;

            }// end of if statement

            // get selected task
            CyberTask selected = task_list.SelectedItem as CyberTask;
            if (selected == null) return;

            // check if already completed
            if (selected.IsCompleted)
            {// start of if statement

                MessageBox.Show(
                    "This task is already completed.",
                    "Already Done",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information
                );
                return;

            }// end of if statement

            // mark as complete in database
            bool ok = task_manager.complete_task(selected.Id);

            if (ok)
            {// start of if statement

                activity_log.add($"Task completed: '{selected.Title}'.");
                load_tasks();

            }// end of if statement

        }// end of complete_task_click method


        // delete task button click
        private void delete_task_click(object sender, RoutedEventArgs e)
        {// start of delete_task_click method

            // check if a task is selected
            if (task_list.SelectedItem == null)
            {// start of if statement

                MessageBox.Show(
                    "Please select a task to delete.",
                    "No Selection",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning
                );
                return;

            }// end of if statement

            // get selected task
            CyberTask selected = task_list.SelectedItem as CyberTask;
            if (selected == null) return;

            // confirm deletion
            MessageBoxResult confirm = MessageBox.Show(
                $"Are you sure you want to delete '{selected.Title}'?",
                "Confirm Delete",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question
            );

            if (confirm == MessageBoxResult.Yes)
            {// start of if statement

                bool ok = task_manager.delete_task(selected.Id);

                if (ok)
                {// start of inner if statement

                    activity_log.add($"Task deleted: '{selected.Title}'.");
                    load_tasks();

                }// end of inner if statement

            }// end of if statement

        }// end of delete_task_click method


        // refresh tasks button click
        private void refresh_tasks_click(object sender, RoutedEventArgs e)
        {// start of refresh_tasks_click method

            load_tasks();

        }// end of refresh_tasks_click method


        // load all tasks from database into the ListView
        private void load_tasks()
        {// start of load_tasks method

            task_list.Items.Clear();

            List<CyberTask> tasks = task_manager.get_all_tasks();

            foreach (CyberTask t in tasks)
            {// start of foreach loop

                // build a styled display row for each task
                Border row = new Border
                {
                    Padding = new Thickness(10, 8, 10, 8),
                    Margin = new Thickness(2, 3, 2, 3),
                    CornerRadius = new CornerRadius(10),
                    Background = t.IsCompleted
                                      ? new SolidColorBrush(Color.FromRgb(10, 40, 10))
                                      : new SolidColorBrush(Color.FromRgb(18, 18, 42)),
                    BorderBrush = t.IsCompleted
                                      ? Brushes.LimeGreen
                                      : new SolidColorBrush(Color.FromRgb(56, 189, 248)),
                    BorderThickness = new Thickness(1.5)
                };

                // status icon
                string status = t.IsCompleted ? "✅" : "⏳";

                // reminder text
                string reminder = string.IsNullOrWhiteSpace(t.Reminder)
                                  ? ""
                                  : $"  🔔 {t.Reminder}";

                // task display text
                TextBlock tb = new TextBlock
                {
                    TextWrapping = TextWrapping.Wrap,
                    Foreground = Brushes.White,
                    FontFamily = new FontFamily("Segoe UI"),
                    FontSize = 13,
                    Text = $"{status}  {t.Title}{reminder}\n" +
                                   $"     {t.Description}\n" +
                                   $"     Added: {t.CreatedAt:dd MMM yyyy  HH:mm}"
                };

                row.Child = tb;

                // add CyberTask object so selection works
                task_list.Items.Add(t);

            }// end of foreach loop

        }// end of load_tasks method


        // QUIZ PANEL - Part 3

        // start or restart quiz button click
        private void start_quiz_click(object sender, RoutedEventArgs e)
        {// start of start_quiz_click method

            quiz = new quiz_manager();
            quizAnswered = false;

            activity_log.add("Quiz started.");

            // load the first question
            update_quiz_ui();

        }// end of start_quiz_click method


        // quiz answer button click
        private void quiz_answer_click(object sender, RoutedEventArgs e)
        {// start of quiz_answer_click method

            // do nothing if quiz not started or already answered
            if (quiz == null || quizAnswered) return;

            // get which button was clicked
            Button clicked = sender as Button;
            int index = int.Parse(clicked.Tag.ToString());

            // submit the answer and get feedback
            string feedback = quiz.submit_answer(index);
            quizAnswered = true;

            // show feedback
            quiz_feedback_text.Text = feedback;
            quiz_feedback_border.Visibility = Visibility.Visible;

            // disable answer buttons until next question
            set_quiz_buttons_enabled(false);

            // show next button or results button
            if (quiz.is_finished())
            {
                quiz_next_btn.Content = "See Results";
                quiz_next_btn.Visibility = Visibility.Visible;
            }
            else
            {
                quiz_next_btn.Content = "Next ▶";
                quiz_next_btn.Visibility = Visibility.Visible;
            }

            // update score label
            quiz_score_label.Text = $"  |  Score: {quiz.Score}";

        }// end of quiz_answer_click method


        // next question button click
        private void quiz_next_click(object sender, RoutedEventArgs e)
        {// start of quiz_next_click method

            if (quiz == null) return;

            if (quiz.is_finished())
            {// start of if statement - show final results

                // display final score message
                quiz_question_text.Text = quiz.get_final_message();
                quiz_question_number.Text = "Quiz Complete!";
                quiz_score_label.Text = $"  |  Final Score: {quiz.Score}/{quiz.TotalQuestions}";

                // hide feedback and next button
                quiz_feedback_border.Visibility = Visibility.Collapsed;
                quiz_next_btn.Visibility = Visibility.Collapsed;

                // hide answer buttons
                set_quiz_buttons_visible(false);

                // fill progress bar
                quiz_progress.Value = quiz.TotalQuestions;

                // log completion
                activity_log.add(
                    $"Quiz completed — Score: {quiz.Score}/{quiz.TotalQuestions}."
                );

            }// end of if statement
            else
            {// start of else statement - go to next question

                quizAnswered = false;

                // hide feedback and next button
                quiz_feedback_border.Visibility = Visibility.Collapsed;
                quiz_next_btn.Visibility = Visibility.Collapsed;

                // re-enable answer buttons
                set_quiz_buttons_enabled(true);

                // load next question
                update_quiz_ui();

            }// end of else statement

        }// end of quiz_next_click method


        // refresh the quiz question display
        private void update_quiz_ui()
        {// start of update_quiz_ui method

            if (quiz == null) return;

            QuizQuestion q = quiz.get_current_question();
            if (q == null) return;

            // question number display
            int current = quiz.CurrentIndex + 1;
            int total = quiz.TotalQuestions;

            quiz_question_number.Text = $"Question {current} of {total}";
            quiz_question_text.Text = q.Question;
            quiz_score_label.Text = $"  |  Score: {quiz.Score}";
            quiz_progress.Value = quiz.CurrentIndex;

            // all 4 answer buttons
            Button[] btns = { quiz_btn_0, quiz_btn_1, quiz_btn_2, quiz_btn_3 };

            for (int i = 0; i < btns.Length; i++)
            {// start of for loop

                if (i < q.Options.Length)
                {// start of if statement - show button with option text

                    btns[i].Content = q.Options[i];
                    btns[i].Visibility = Visibility.Visible;

                }// end of if statement
                else
                {// start of else statement - hide unused buttons

                    btns[i].Visibility = Visibility.Collapsed;

                }// end of else statement

            }// end of for loop

            // enable all answer buttons
            set_quiz_buttons_enabled(true);

            // hide feedback and next button
            quiz_feedback_border.Visibility = Visibility.Collapsed;
            quiz_next_btn.Visibility = Visibility.Collapsed;

        }// end of update_quiz_ui method


        // enable or disable all quiz answer buttons
        private void set_quiz_buttons_enabled(bool enabled)
        {// start of set_quiz_buttons_enabled method

            quiz_btn_0.IsEnabled = enabled;
            quiz_btn_1.IsEnabled = enabled;
            quiz_btn_2.IsEnabled = enabled;
            quiz_btn_3.IsEnabled = enabled;

        }// end of set_quiz_buttons_enabled method


        // show or hide all quiz answer buttons
        private void set_quiz_buttons_visible(bool visible)
        {// start of set_quiz_buttons_visible method

            Visibility v = visible ? Visibility.Visible : Visibility.Collapsed;

            quiz_btn_0.Visibility = v;
            quiz_btn_1.Visibility = v;
            quiz_btn_2.Visibility = v;
            quiz_btn_3.Visibility = v;

        }// end of set_quiz_buttons_visible method


        // ACTIVITY LOG PANEL - Part 3

        // refresh log button click
        private void refresh_log_click(object sender, RoutedEventArgs e)
        {// start of refresh_log_click method

            load_log();

        }// end of refresh_log_click method


        // clear log button click
        private void clear_log_click(object sender, RoutedEventArgs e)
        {// start of clear_log_click method

            // confirm before clearing
            MessageBoxResult confirm = MessageBox.Show(
                "Are you sure you want to clear the activity log?",
                "Clear Log",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question
            );

            if (confirm == MessageBoxResult.Yes)
            {// start of if statement

                activity_log.clear();
                load_log();

            }// end of if statement

        }// end of clear_log_click method


        // load activity log entries into the ListView
        private void load_log()
        {// start of load_log method

            log_list.Items.Clear();

            List<LogEntry> entries = activity_log.get_recent();

            // show message if no entries exist
            if (entries.Count == 0)
            {// start of if statement

                log_list.Items.Add("No activity recorded yet.");
                return;

            }// end of if statement

            int number = 1;

            foreach (LogEntry entry in entries)
            {// start of foreach loop

                // build a styled row for each log entry
                Border row = new Border
                {
                    Padding = new Thickness(12, 8, 12, 8),
                    Margin = new Thickness(2, 3, 2, 3),
                    CornerRadius = new CornerRadius(10),
                    Background = new SolidColorBrush(Color.FromRgb(18, 18, 42)),
                    BorderBrush = new SolidColorBrush(Color.FromRgb(56, 189, 248)),
                    BorderThickness = new Thickness(1)
                };

                TextBlock tb = new TextBlock
                {
                    Text = $"{number}.  [{entry.Timestamp:HH:mm:ss}]  {entry.Description}",
                    Foreground = Brushes.White,
                    FontFamily = new FontFamily("Segoe UI"),
                    FontSize = 13,
                    TextWrapping = TextWrapping.Wrap
                };

                row.Child = tb;
                log_list.Items.Add(row);
                number++;

            }// end of foreach loop

        }// end of load_log method


    }// end of mainWindow

}// end of namespace
