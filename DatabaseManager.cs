
using Microsoft.Data.Sqlite;
using System;
using System.IO;
using System.Globalization;


namespace CodingTracker
{
    public class DatabaseManager
    {
        private readonly string _connectionString;

        private const string DatabaseFileName = "codingTrackerDB.db";

        /// Konstruktor
        public DatabaseManager()
        {
            _connectionString = $"Data Source={DatabaseFileName}";
        }
        /// DB initialization and create if not exist
        public void InitializeDatabase()
        {
            Console.WriteLine($"Route for DB file: {Path.Combine(Directory.GetCurrentDirectory(), DatabaseFileName)}");

            CreateTables();
            Console.WriteLine("Database created succesfully.");
        }
        /// Creates the tables in DB. 
        private void CreateTables()
        {
            string sql = @"
            CREATE TABLE IF NOT EXISTS coding_tracker (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                Name TEXT NOT NULL,
                StartDate NOT NULL,
                EndDate NOT NULL
            );
        ";
            ExecuteNonQuery(sql);
        }

        public void ExecuteNonQuery(string sqlCommand)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    using (var command = new SqliteCommand(sqlCommand, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"There is a problem with the SQL query: {ex.Message}");
                }
            }
        }

        public void InsertRowIntoDB(string name, string startDate, string endDate)
        {
            string insertSql = $"INSERT INTO coding_tracker (Name, StartDate, EndDate) VALUES ('{name}', '{startDate}', '{endDate}'";
            ExecuteNonQuery(insertSql);
            Console.WriteLine("Insert in to database was succesfully!");
        }

        public void UpdateRowInDB(int id, string name, string startDate, string endDate)
        {
            string insertSql = $"UPDATE coding_tracker SET name = '{name}', StartDate = '{startDate}', EndDate = '{endDate}' WHERE id = {id}";
            ExecuteNonQuery(insertSql);
            Console.WriteLine("Update was succesfull!");
        }
        public void DeleteRowInDB(int id)
        {
            string deleteSql = $"DELETE FROM coding_tracker WHERE id = {id}";
            ExecuteNonQuery(deleteSql);
            Console.WriteLine("Data deleted successfully!");
        }
        public List<CodingEntry> GetDataFromDB()
        {
            var entries = new List<CodingEntry>();
            string getSql = "SELECT Id, Name, StartDate, EndDate From coding_tracker";

            using (var connection = new SqliteConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    using (var command = new SqliteCommand(getSql, connection))
                    using (var reader = command.ExecuteReader())
                    {
                        if (!reader.HasRows)
                        {
                            return entries;
                        }
                        while (reader.Read())
                        {
                            var entry = new CodingEntry
                            {
                                Id = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                StartDate = DateTime.Parse(reader.GetString(2), CultureInfo.InvariantCulture),
                                EndDate = DateTime.Parse(reader.GetString(3), CultureInfo.InvariantCulture)
                            };
                            entries.Add(entry);
                        }
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred while retrieving data: {ex.Message}");
                }
            }
            return entries;
        }

        public void ViewAllRecords()
        {
            List<CodingEntry> records = GetDataFromDB();

            Console.WriteLine("\n------------------------------------------------------------------------------------");
            Console.WriteLine("------------------------------- Coding Entries -------------------------------------");
            Console.WriteLine("------------------------------------------------------------------------------------");

            if (records == null || records.Count == 0)
            {
                Console.WriteLine("There is no entry.");
                Console.WriteLine("------------------------------------------------------------------------------------");
                return;
            }

            Console.WriteLine("{0, -5} | {1, -20} | {2, -20} | {3, -20} | {4, -10}",
                            "ID", "NAME", "STARTING  DATE", "ENDING DATE", "DURATION");
            Console.WriteLine("------------------------------------------------------------------------------------");

            foreach (var record in records)
            {
                Console.WriteLine("{0, -5} | {1, -20} | {2, -20} | {3, -20} | {4, -10}",
                                record.Id,
                                record.Name,
                                record.StartDate.ToString(UserInput.DateTimeFormat),
                                record.EndDate.ToString(UserInput.DateTimeFormat),
                                $"{record.Duration.Hours:D2}:{record.Duration.Minutes:D2}");
            }

            Console.WriteLine("------------------------------------------------------------------------------------");
        }
    }
}

