namespace PantryPilot.Models
{
    public class GroceryList
    {
        public int Id { get; set; }
        public string Name { get; set; }

        // Foreign Key
        // Navigation to ApplicationUser
        public int UserId { get; set; } 
        public ApplicationUser User { get; set; } // Updated User to ApplicationUser to implement authentication

        // Relationships
        public List<GroceryListItem> Items { get; set; }
    }
}