namespace PantryPilot.Models;

public class WeeklyMenu
{
    public int Id { get; set; } // Required for EF Core
    public int UserId { get; set; } // Needed for user to own weeklyMenu
    public List<MenuDay> Days { get; set; } = new();
}