namespace HabitTracker.Models;

internal class HabitLog
{
    public int Id { get; set; }
    public int HabitId { get; set; }
    public DateOnly Date {  get; set; }
    public double Quantity { get; set; }
}