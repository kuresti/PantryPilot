namespace PantryPilot.Models
{
    public class Menu
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? Date { get; set; }

        // Foreign Key
        // Navigation to ApplicationUser
        public int UserId { get; set; }
        public ApplicationUser User { get; set; } // Updated User to ApplicationUser to implement authentication

        // Relationship
        public List<MenuRecipe> MenuRecipes { get; set; }
    }
}