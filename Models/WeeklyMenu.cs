namespace PantryPilot.Models;

public class WeeklyMenu
{
    public int Id { get; set; } // Required for EF Core
    public List<MenuDay> Days { get; set; } = new();
}