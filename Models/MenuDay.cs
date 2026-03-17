namespace PantryPilot.Models
{
    public class MenuDay
    {
        public int id { get; set; }

        public DateOnly Date { get; set; }

        public int? BreakfastRecipeId { get; set; }
        public int? LunchRecipeId { get; set; }
        public int? DinnerRecipeId { get; set; }

        public Recipe? BreakfastRecipe { get; set; }
        public Recipe? LunchRecipe { get; set; }
        public Recipe? DinnerRecipe { get; set; }
    }
}