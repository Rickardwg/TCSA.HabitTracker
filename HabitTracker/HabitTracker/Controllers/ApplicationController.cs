using HabitTracker.Models;
using HabitTracker.Services;
using HabitTracker.Views;

namespace HabitTracker.Controllers;

internal class ApplicationController(HabitTrackingService service, ConsoleView view)
{
    private readonly HabitTrackingService _service = service;
    private readonly ConsoleView _view = view;

    public void Run()
    {
        while (true)
        {
            MainMenuAction action = _view.ShowMainMenu();

            switch (action)
            {
                case MainMenuAction.ViewHabits:
                    ManageHabits();
                    break;

                case MainMenuAction.ViewLogs:
                    break;

                case MainMenuAction.Exit:
                    return;
            }
        }
    }

    private void ManageHabits()
    {
        while (true)
        {
            IReadOnlyList<Habit> habits = _service.GetAllHabits();

            _view.DisplayHabits(habits);
            HabitMenuAction action = _view.ShowHabitMenu();

            switch (action)
            {
                case HabitMenuAction.Create:
                    break;

                case HabitMenuAction.Edit:
                    break;

                case HabitMenuAction.Delete:
                    break;

                case HabitMenuAction.Back:
                    return;
            }
        }
    }
}
