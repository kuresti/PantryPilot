// Data to seed the pantry.db for testing features
using PantryPilot.Models;

namespace PantryPilot.Data;

public static class SeedData
{
    public static void Initialize(ApplicationDbContext context, int userId)
    {
        if (context.Recipes.Any())
            return;

        var recipes = new List<Recipe>
        {
            new Recipe
            {
                Name = "Blueberry Pancakes",
                PrepTime = 10,
                CookTime = 15,
                Servings = 2,
                UserId = userId
            },
            new Recipe
            {
                Name = "Chicken Caesar Salad",
                PrepTime = 8,
                CookTime = 10,
                Servings = 1,
                UserId = userId
            },
            new Recipe
            {
                Name = "Spaghetti Bolognese",
                PrepTime = 12,
                CookTime = 25,
                Servings = 4,
                UserId = userId
            }
        };

        context.Recipes.AddRange(recipes);
        context.SaveChanges();
    }
}