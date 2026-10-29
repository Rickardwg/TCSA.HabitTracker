using Microsoft.Data.Sqlite;

namespace HabitTracker.Data;

internal class DatabaseInitializer
{
    private readonly string _connectionString = "Data Source=habittracker.db;Foreign Keys=True";

    public void Initialize()
    {
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        using SqliteCommand command = connection.CreateCommand();
        command.CommandText =
        """
            CREATE TABLE IF NOT EXISTS Habits
            (
                Id INTEGER PRIMARY KEY,
                Name TEXT NOT NULL,
                Unit TEXT NOT NULL
            );
        """;
        command.ExecuteNonQuery();

        command.CommandText =
        """
            CREATE TABLE IF NOT EXISTS HabitLogs
            (
                Id INTEGER PRIMARY KEY,
                HabitId INTEGER NOT NULL REFERENCES Habits(Id),
                Date TEXT NOT NULL,
                Quantity REAL NOT NULL CHECK (Quantity > 0),
                UNIQUE (HabitId, Date)
            );
        """;
        command.ExecuteNonQuery();
    }
}
