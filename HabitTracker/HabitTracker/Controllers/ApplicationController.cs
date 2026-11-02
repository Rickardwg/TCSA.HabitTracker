using HabitTracker.Services;
using HabitTracker.Views;

namespace HabitTracker.Controllers;

internal class ApplicationController(HabitTrackingService service, ConsoleView view)
{
    private readonly HabitTrackingService _service = service;
    private readonly ConsoleView _view = view;

    public void Run()
    {

    }
}
