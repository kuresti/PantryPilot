namespace PantryPilot.Models
{
    public class WeeklyMenu
    {
        public int Id { get; set; }

        public List<MenuDay> Days { get; set; } = new();
    }
}