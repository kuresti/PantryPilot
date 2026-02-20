namespace PantryPilot.Models
{
    public class Menu
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? Date { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public List<MenuRecipe> MenuRecipes { get; set; }
    }
}