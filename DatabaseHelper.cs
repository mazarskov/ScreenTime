using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics;
using System.Threading.Tasks;

namespace ScreenTime
{
    public static class DatabaseHelper
    {
        public class AppUsageData
        {
            public int Id { get; set; }
            public string AppName { get; set; }
            public long StartTime { get; set; }
            public long EndTime { get; set; }
        }


        private static string connectionString = "Data Source=mydatabase.db;Version=3;";

        public static void InitializeDatabase()
        {
            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                conn.Open();

                string createTableQuery = @"CREATE TABLE IF NOT EXISTS FocusSessions (
                                            Id INTEGER PRIMARY KEY AUTOINCREMENT,
                                            AppName TEXT NOT NULL,
                                            StartTime INTEGER NOT NULL,
                                            EndTime INTEGER NOT NULL
                                            );";

                using (SQLiteCommand cmd = new SQLiteCommand(createTableQuery, conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void InsertData(string appName, long startTime, long endTime)
        {
            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                conn.Open();

                string insertQuery = "INSERT INTO FocusSessions (AppName, StartTime, EndTime) VALUES (@AppName, @StartTime, @EndTime)";

                using (SQLiteCommand cmd = new SQLiteCommand(insertQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@AppName", appName);
                    cmd.Parameters.AddWithValue("@StartTime", startTime);
                    cmd.Parameters.AddWithValue("@EndTime", endTime);
                    cmd.ExecuteNonQuery();
                    
                }
            }
        }

        
        public static async Task InsertDataAsync(string appName, long startTime, long endTime)
        {

            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(connectionString))
                {
                    await conn.OpenAsync();

                    string insertQuery = "INSERT INTO FocusSessions (AppName, StartTime, EndTime) VALUES (@AppName, @StartTime, @EndTime)";

                    using (SQLiteCommand cmd = new SQLiteCommand(insertQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@AppName", appName);
                        cmd.Parameters.AddWithValue("@StartTime", startTime);
                        cmd.Parameters.AddWithValue("@EndTime", endTime);
                        await cmd.ExecuteNonQueryAsync();
                    }
                }
            }
            catch (System.IO.FileLoadException ex)
            {
                // Log detailed information
                Console.WriteLine($"FileLoadException: {ex.Message}");
                Console.WriteLine($"Fusion Log: {ex.FusionLog}");
                throw;
            }
            catch (Exception ex)
            {
                // Log other exceptions
                Console.WriteLine($"Exception: {ex.Message}");
                throw;
            }
        }
        

        public static DataTable LoadData()
        {
            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                conn.Open();

                string selectQuery = "SELECT * FROM FocusSessions";

                using (SQLiteCommand cmd = new SQLiteCommand(selectQuery, conn))
                {
                    using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        return dt;
                    }
                }
            }
        }

        public static void UpdateData(int id, string appName, long startTime, long endTime)
        {
            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                conn.Open();

                string updateQuery = "UPDATE FocusSessions SET AppName = @AppName, StartTime = @StartTime, EndTime = @EndTime WHERE Id = @Id";

                using (SQLiteCommand cmd = new SQLiteCommand(updateQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.Parameters.AddWithValue("@AppName", appName);
                    cmd.Parameters.AddWithValue("@StartTime", startTime);
                    cmd.Parameters.AddWithValue("@EndTime", endTime);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void DeleteData(int id)
        {
            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                conn.Open();

                string deleteQuery = "DELETE FROM FocusSessions WHERE Id = @Id";

                using (SQLiteCommand cmd = new SQLiteCommand(deleteQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void DeleteAllData()
        {
            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                conn.Open();

                string deleteAllQuery = "DELETE FROM FocusSessions";

                using (SQLiteCommand cmd = new SQLiteCommand(deleteAllQuery, conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static List<AppUsageData> GetDataFromSpecificDay(long startOfDayUnix, long endOfDayUnix)
        {
            List<AppUsageData> results = new List<AppUsageData>();

            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                conn.Open();

                string selectQuery = "SELECT * FROM FocusSessions WHERE StartTime >= @StartOfDayUnix AND StartTime <= @EndOfDayUnix";

                using (SQLiteCommand cmd = new SQLiteCommand(selectQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@StartOfDayUnix", startOfDayUnix);
                    cmd.Parameters.AddWithValue("@EndOfDayUnix", endOfDayUnix);

                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            AppUsageData data = new AppUsageData();
                            data.Id = reader.GetInt32(0);
                            data.AppName = reader.GetString(1);
                            data.StartTime = reader.GetInt64(2);
                            data.EndTime = reader.GetInt64(3);

                            results.Add(data);
                        }
                    }
                }
            }

            return results;
        }

    }
}
