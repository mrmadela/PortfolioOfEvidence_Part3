using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Windows;

namespace PortfolioOfEvidence_Part3
{// start of namespace

    // Model class for cybersecurity tasks
    public class CyberTask
    {// start of CyberTask class

        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Reminder { get; set; }

        public bool IsCompleted { get; set; }

        public DateTime CreatedAt { get; set; }

    }// end of CyberTask class


    // Class responsible for all database operations
    public class task_manager
    {// start of task_manager class

        // MySQL connection string
        // Change password if needed
        private static string connectionString =
            "server=localhost;" +
            "database=cybertasksdb;" +
            "uid=root;" +
            "pwd=Snabo@15506;";


        // Test and handle database connection exception
        public static void test_connection()
        {// start of test_connection method
            try
            {// start of try
                using (MySqlConnection conn =
                    new MySqlConnection(connectionString))
                {
                    conn.Open();

                    MessageBox.Show(
                        "Database Connected Successfully!"
                    );
                }
            }// end of try
            catch (Exception ex)
            {// start of catch
                MessageBox.Show(
                    "Connection Error: " +
                    ex.Message
                );
            }// end of catch
        }// end of test_connection method


        // Create database table if it does not exist
        public static void initialize_db()
        {// start initialize_db method

            try
            {// start of try
                using (MySqlConnection conn =
                    new MySqlConnection(connectionString))
                {
                    conn.Open();

                    string createTable =
                    @"CREATE TABLE IF NOT EXISTS Tasks
                    (
                        Id INT AUTO_INCREMENT PRIMARY KEY,
                        Title VARCHAR(255) NOT NULL,
                        Description TEXT NOT NULL,
                        Reminder VARCHAR(255),
                        IsCompleted BOOLEAN DEFAULT FALSE,
                        CreatedAt DATETIME DEFAULT CURRENT_TIMESTAMP
                    );";

                    using (MySqlCommand cmd =
                        new MySqlCommand(createTable, conn))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
            }// end of try
            catch (Exception ex)
            {// start of catch
                MessageBox.Show(
                    "Database Initialization Error: "
                    + ex.Message
                );
            }// end of catch

        }// end initialize_db method


        // Add a new task
        public static bool add_task
        (
            string title,
            string description,
            string reminder
        )
        {// start add_task method

            try
            {// start of try
                using (MySqlConnection conn =
                    new MySqlConnection(connectionString))
                {
                    conn.Open();

                    string insert =
                    @"INSERT INTO Tasks
                    (
                        Title,
                        Description,
                        Reminder,
                        IsCompleted
                    )
                    VALUES
                    (
                        @title,
                        @description,
                        @reminder,
                        0
                    );";

                    using (MySqlCommand cmd =
                        new MySqlCommand(insert, conn))
                    {
                        cmd.Parameters.AddWithValue(
                            "@title",
                            title
                        );

                        cmd.Parameters.AddWithValue(
                            "@description",
                            description
                        );

                        cmd.Parameters.AddWithValue(
                            "@reminder",
                            reminder
                        );

                        cmd.ExecuteNonQuery();
                    }
                }

                return true;
            }// end of try
            catch (Exception ex)
            {// start of catch
                MessageBox.Show(
                    "Add Task Error: "
                    + ex.Message
                );

                return false;
            }// end of catch

        }// end add_task method


        // Retrieve all tasks from database
        public static List<CyberTask> get_all_tasks()
        {// start get_all_tasks method

            List<CyberTask> tasks =
                new List<CyberTask>();

            try
            {// start of try
                using (MySqlConnection conn =
                    new MySqlConnection(connectionString))
                {
                    conn.Open();

                    string select =
                        "SELECT * FROM Tasks " +
                        "ORDER BY CreatedAt DESC";

                    using (MySqlCommand cmd =
                        new MySqlCommand(select, conn))
                    {
                        using (MySqlDataReader reader =
                            cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {// start of while loop
                                CyberTask task =
                                    new CyberTask();

                                task.Id =
                                    Convert.ToInt32(
                                        reader["Id"]
                                    );

                                task.Title =
                                    reader["Title"]
                                    .ToString();

                                task.Description =
                                    reader["Description"]
                                    .ToString();

                                task.Reminder =
                                    reader["Reminder"]
                                    .ToString();

                                task.IsCompleted =
                                    Convert.ToBoolean(
                                        reader["IsCompleted"]
                                    );

                                task.CreatedAt =
                                    Convert.ToDateTime(
                                        reader["CreatedAt"]
                                    );

                                tasks.Add(task);
                            }// end of while looop
                        }
                    }
                }
            }// end of try
            catch (Exception ex)
            {// start of catch
                MessageBox.Show(
                    "Retrieve Task Error: "
                    + ex.Message
                );
            }// end of catch

            return tasks;

        }// end get_all_tasks method


        // Mark task as completed
        public static bool complete_task(int id)
        {// start complete_task method

            try
            {// start of try and catch
                using (MySqlConnection conn =
                    new MySqlConnection(connectionString))
                {
                    conn.Open();

                    string update =
                    @"UPDATE Tasks
                      SET IsCompleted = 1
                      WHERE Id = @id";

                    using (MySqlCommand cmd =
                        new MySqlCommand(update, conn))
                    {
                        cmd.Parameters.AddWithValue(
                            "@id",
                            id
                        );

                        cmd.ExecuteNonQuery();
                    }
                }

                return true;
            }// end of try
            catch (Exception ex)
            {// start of catch
                MessageBox.Show(
                    "Complete Task Error: "
                    + ex.Message
                );

                return false;
            }// end of catch

        }// end complete_task method


        // Delete task
        public static bool delete_task(int id)
        {// start delete_task method

            try
            {
                using (MySqlConnection conn =
                    new MySqlConnection(connectionString))
                {
                    conn.Open();

                    string delete =
                    @"DELETE FROM Tasks
                      WHERE Id = @id";

                    using (MySqlCommand cmd =
                        new MySqlCommand(delete, conn))
                    {
                        cmd.Parameters.AddWithValue(
                            "@id",
                            id
                        );

                        cmd.ExecuteNonQuery();
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Delete Task Error: "
                    + ex.Message
                );

                return false;
            }

        }// end delete_task method


        // Quick add task from chatbot
        public static bool quick_add_task(string title)
        {// start quick_add_task

            string description =
                "Task added via chatbot: "
                + title;

            return add_task(
                title,
                description,
                ""
            );

        }// end quick_add_task


        // Set or update reminder
        public static bool quick_set_reminder
        (
            string taskTitle,
            string reminderText
        )
        {// start quick_set_reminder

            try
            {// start of tr
                using (MySqlConnection conn =
                    new MySqlConnection(connectionString))
                {
                    conn.Open();

                    string update =
                    @"UPDATE Tasks
                      SET Reminder = @reminder
                      WHERE LOWER(Title)
                      LIKE @title";

                    using (MySqlCommand cmd =
                        new MySqlCommand(update, conn))
                    {
                        cmd.Parameters.AddWithValue(
                            "@reminder",
                            reminderText
                        );

                        cmd.Parameters.AddWithValue(
                            "@title",
                            "%" +
                            taskTitle.ToLower() +
                            "%"
                        );

                        cmd.ExecuteNonQuery();
                    }
                }

                return true;
            }// end of try
            catch (Exception ex)
            {// start of catch
                MessageBox.Show(
                    "Reminder Error: "
                    + ex.Message
                );

                return false;
            }// end of catch

        }// end quick_set_reminder

    }// end task_manager class

}// end namespace