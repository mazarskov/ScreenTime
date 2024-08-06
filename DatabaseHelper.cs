using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics;
using System.Threading;
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
        private static ConcurrentQueue<Action<SQLiteConnection>> taskQueue = new ConcurrentQueue<Action<SQLiteConnection>>();
        private static CancellationTokenSource cts = new CancellationTokenSource();
        private static Task backgroundTask = Task.Run(() => ProcessQueue(cts.Token));

        static DatabaseHelper()
        {
            // Ensure the database is initialized when the class is first accessed
            InitializeDatabase();
        }

        private static async Task ProcessQueue(CancellationToken token)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                await connection.OpenAsync();

                while (!token.IsCancellationRequested)
                {
                    if (taskQueue.TryDequeue(out var task))
                    {
                        task(connection);
                    }
                    else
                    {
                        await Task.Delay(100); // Adjust the delay as needed
                    }
                }
            }
        }

        public static void InitializeDatabase()
        {
            EnqueueTask(connection =>
            {
                string createTableQuery = @"CREATE TABLE IF NOT EXISTS FocusSessions (
                                            Id INTEGER PRIMARY KEY AUTOINCREMENT,
                                            AppName TEXT NOT NULL,
                                            StartTime INTEGER NOT NULL,
                                            EndTime INTEGER NOT NULL
                                            );";

                using (SQLiteCommand cmd = new SQLiteCommand(createTableQuery, connection))
                {
                    cmd.ExecuteNonQuery();
                }
            });
        }

        public static void InsertData(string appName, long startTime, long endTime)
        {
            EnqueueTask(connection =>
            {
                string insertQuery = "INSERT INTO FocusSessions (AppName, StartTime, EndTime) VALUES (@AppName, @StartTime, @EndTime)";

                Stopwatch stopwatch = Stopwatch.StartNew();
                using (SQLiteCommand cmd = new SQLiteCommand(insertQuery, connection))
                {
                    cmd.Parameters.AddWithValue("@AppName", appName);
                    cmd.Parameters.AddWithValue("@StartTime", startTime);
                    cmd.Parameters.AddWithValue("@EndTime", endTime);
                    cmd.ExecuteNonQuery();
                }
                stopwatch.Stop();
                Debug.WriteLine($"Time elapsed of db operation: {stopwatch.ElapsedMilliseconds} ms");
            });
        }

        public static DataTable LoadData()
        {
            var dt = new DataTable();
            var tcs = new TaskCompletionSource<object>();
            EnqueueTask(connection =>
            {
                try
                {
                    string selectQuery = "SELECT * FROM FocusSessions";

                    using (SQLiteCommand cmd = new SQLiteCommand(selectQuery, connection))
                    {
                        using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(cmd))
                        {
                            adapter.Fill(dt);
                        }
                    }
                    tcs.SetResult(null);
                }
                catch (Exception ex)
                {
                    tcs.SetException(ex);
                }
            });
            tcs.Task.Wait();
            return dt;
        }

        public static void UpdateData(int id, string appName, long startTime, long endTime)
        {
            EnqueueTask(connection =>
            {
                string updateQuery = "UPDATE FocusSessions SET AppName = @AppName, StartTime = @StartTime, EndTime = @EndTime WHERE Id = @Id";

                using (SQLiteCommand cmd = new SQLiteCommand(updateQuery, connection))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.Parameters.AddWithValue("@AppName", appName);
                    cmd.Parameters.AddWithValue("@StartTime", startTime);
                    cmd.Parameters.AddWithValue("@EndTime", endTime);
                    cmd.ExecuteNonQuery();
                }
            });
        }

        public static void DeleteData(int id)
        {
            EnqueueTask(connection =>
            {
                string deleteQuery = "DELETE FROM FocusSessions WHERE Id = @Id";

                using (SQLiteCommand cmd = new SQLiteCommand(deleteQuery, connection))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.ExecuteNonQuery();
                }
            });
        }

        public static void DeleteAllData()
        {
            EnqueueTask(connection =>
            {
                string deleteAllQuery = "DELETE FROM FocusSessions";

                using (SQLiteCommand cmd = new SQLiteCommand(deleteAllQuery, connection))
                {
                    cmd.ExecuteNonQuery();
                }
            });
        }

        public static List<AppUsageData> GetDataFromSpecificDay(long startOfDayUnix, long endOfDayUnix)
        {
            var results = new List<AppUsageData>();
            var tcs = new TaskCompletionSource<object>();
            EnqueueTask(connection =>
            {
                try
                {
                    string selectQuery = "SELECT * FROM FocusSessions WHERE StartTime >= @StartOfDayUnix AND StartTime <= @EndOfDayUnix";

                    using (SQLiteCommand cmd = new SQLiteCommand(selectQuery, connection))
                    {
                        cmd.Parameters.AddWithValue("@StartOfDayUnix", startOfDayUnix);
                        cmd.Parameters.AddWithValue("@EndOfDayUnix", endOfDayUnix);

                        using (SQLiteDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var data = new AppUsageData
                                {
                                    Id = reader.GetInt32(0),
                                    AppName = reader.GetString(1),
                                    StartTime = reader.GetInt64(2),
                                    EndTime = reader.GetInt64(3)
                                };
                                results.Add(data);
                            }
                        }
                    }
                    tcs.SetResult(null);
                }
                catch (Exception ex)
                {
                    tcs.SetException(ex);
                }
            });
            tcs.Task.Wait();
            return results;
        }

        private static void EnqueueTask(Action<SQLiteConnection> task)
        {
            taskQueue.Enqueue(task);
        }

        public static void Dispose()
        {
            cts.Cancel();
            backgroundTask.Wait();
        }
    }
}
