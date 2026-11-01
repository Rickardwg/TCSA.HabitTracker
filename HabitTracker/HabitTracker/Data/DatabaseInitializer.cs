using HabitTracker.Models;
using Microsoft.Data.Sqlite;

namespace HabitTracker.Data;

internal class DatabaseInitializer
{
    private readonly string _connectionString = "Data Source=habittracker.db;Foreign Keys=True";

    public void Initialize()
    {
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        using SqliteTransaction transaction = connection.BeginTransaction();
        using SqliteCommand command = connection.CreateCommand();

        command.CommandText = "PRAGMA user_version;";
        long databaseVersion = (long)command.ExecuteScalar()!;

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

        if (databaseVersion == 0)
        {
            Habit[] seedHabits =
            [
                new Habit { Name = "Walking", Unit = "steps" },
                new Habit { Name = "Reading", Unit = "pages" },
                new Habit { Name = "Drinking water", Unit = "glasses" },
                new Habit { Name = "Push-ups", Unit = "repetitions" }
            ];

            DateOnly today = DateOnly.FromDateTime(DateTime.Today);

            foreach (Habit habit in seedHabits)
            {
                command.Parameters.Clear();

                command.CommandText =
                """
                INSERT INTO Habits (Name, Unit)
                VALUES ($name, $unit)
                RETURNING Id;
                """;
                command.Parameters.AddWithValue("$name", habit.Name);
                command.Parameters.AddWithValue("$unit", habit.Unit);

                long habitId = (long)command.ExecuteScalar()!;

                for (int dayOffset = 0; dayOffset < 25; dayOffset++)
                {
                    command.Parameters.Clear();

                    command.CommandText =
                        """
                        INSERT INTO HabitLogs (HabitId, Date, Quantity)
                        VALUES ($habitId, $date, $quantity);
                        """;

                    double quantity = Random.Shared.Next(1, 101);
                    command.Parameters.AddWithValue("$habitId", habitId);
                    command.Parameters.AddWithValue("$date", today.AddDays(-dayOffset));
                    command.Parameters.AddWithValue("$quantity", quantity);

                    command.ExecuteNonQuery();
                }
            }

            command.Parameters.Clear();
            command.CommandText = "PRAGMA user_version = 1;";
            command.ExecuteNonQuery();
        }

        transaction.Commit();
    }
}
