using HabitTracker.Models;
using Spectre.Console;

namespace HabitTracker.Views;

internal class ConsoleView
{
    public void DisplayHabits(IReadOnlyList<Habit> habits, string? statusMessage = null)
    {
        var table = new Table()
        .RoundedBorder()
        .BorderColor(Color.Gray)
        .Title($"[bold]Habits ({habits.Count})[/]");

        table.AddColumn(new TableColumn(new Markup("[bold cyan]Name[/]").Centered()));
        table.AddColumn(new TableColumn(new Markup("[bold cyan]Unit[/]").Centered()));

        foreach (Habit habit in habits)
        {
            table.AddRow(new Text(habit.Name), new Text(habit.Unit));
        }

        AnsiConsole.Write(table);
        
        if (statusMessage is not null)
        {
            AnsiConsole.WriteLine(statusMessage);
        }
    }
}
