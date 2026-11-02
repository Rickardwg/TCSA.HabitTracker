using HabitTracker.Models;
using HabitTracker.Repositories;

namespace HabitTracker.Services;

internal class HabitTrackingService(IHabitRepository habitRepository)
{
    private readonly IHabitRepository _habitRepository = habitRepository;

    public IReadOnlyList<Habit> GetAllHabits()
    {
        return _habitRepository.GetAll();
    }
}
