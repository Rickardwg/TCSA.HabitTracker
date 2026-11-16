using HabitTracker.Models;
using Spectre.Console;

namespace HabitTracker.Views;

internal class ConsoleView
{
    public MainMenuAction ShowMainMenu()
    {
        Console.Clear();

        return AnsiConsole.Prompt(new SelectionPrompt<MainMenuAction>()
            .Title("[bold cyan]Habit Tracker[/]")
            .HighlightStyle(new Style(Color.Cyan, decoration: Decoration.Bold))
            .AddChoices(MainMenuAction.ViewHabits, MainMenuAction.ViewLogs, MainMenuAction.Exit)
            .AddCancelResult(MainMenuAction.Exit)
            .UseConverter(choice => choice switch
            {
                MainMenuAction.ViewHabits => "View Habits",
                MainMenuAction.ViewLogs => "View Logs",
                MainMenuAction.Exit => "Exit",
                _ => throw new InvalidOperationException("Unexpected menu choice.")
            }));
    }

    public HabitMenuAction ShowHabitMenu()
    {
        return AnsiConsole.Prompt(new SelectionPrompt<HabitMenuAction>()
            .HighlightStyle(new Style(Color.Cyan, decoration: Decoration.Bold))
            .AddChoices(HabitMenuAction.Create, HabitMenuAction.Edit, HabitMenuAction.Delete, HabitMenuAction.Back)
            .AddCancelResult(HabitMenuAction.Back)
            .UseConverter(choice => choice switch
            {
                HabitMenuAction.Create => "Create",
                HabitMenuAction.Edit => "Edit",
                HabitMenuAction.Delete => "Delete",
                HabitMenuAction.Back => "Back",
                _ => throw new InvalidOperationException("Unexpected menu choice.")
            }));
    }

    public void DisplayHabits(IReadOnlyList<Habit> habits, string? statusMessage = null)
    {
        Console.Clear();

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
