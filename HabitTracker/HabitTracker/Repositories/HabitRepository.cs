using HabitTracker.Models;
using Microsoft.Data.Sqlite;

namespace HabitTracker.Repositories;

internal class HabitRepository(string connectionString) : IHabitRepository
{
    private readonly string _connectionString = connectionString;

    public IReadOnlyList<Habit> GetAll()
    {
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        using SqliteCommand command = connection.CreateCommand();
        command.CommandText =
            """
            SELECT Id, Name, Unit
            FROM Habits
            ORDER BY Id;
            """;
        using SqliteDataReader reader = command.ExecuteReader();

        List<Habit> habits = [];

        while (reader.Read())
        {
            habits.Add(new Habit
            {
                Id = reader.GetInt32(0),
                Name = reader.GetString(1),
                Unit = reader.GetString(2)
            });
        }

        return habits;
    }
}
