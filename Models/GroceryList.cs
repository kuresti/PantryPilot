namespace PantryPilot.Models
{
    public class GroceryList
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public List<GroceryListItem> Items { get; set; }
    }
}