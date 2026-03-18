namespace PantryPilot.Models;

public class MenuDay
{
    public int Id { get; set; } // Required for EF Core

    public DateOnly Date { get; set; }

    public int? BreakfastRecipeId { get; set; }
    public int? LunchRecipeId { get; set; }
    public int? DinnerRecipeId { get; set; }
}