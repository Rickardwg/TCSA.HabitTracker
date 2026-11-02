using HabitTracker.Models;

namespace HabitTracker.Repositories;

internal interface IHabitRepository
{
    public IReadOnlyList<Habit> GetAll();
}
