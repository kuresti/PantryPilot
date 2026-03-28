namespace PantryPilot.Models;

public class WeeklyMenu
{
    public int Id { get; set; } // Required for EF Core
    public int UserId { get; set; } // Needed for user to own weeklyMenu
    public int UserId { get; set; } // Required to persist ownership of user menus
                                    // and also drag and drop functionality in Menu
    public List<MenuDay> Days { get; set; } = new();
}