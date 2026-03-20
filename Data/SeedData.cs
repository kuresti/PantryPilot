// Data to seed the pantry.db for testing features
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using PantryPilot.Models;

namespace PantryPilot.Data;

public static class SeedData
{
    public static async Task InitializeAsync(IServiceProvider services)
    {
        using var scope = services.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

        await context.Database.MigrateAsync();

        // Do not reseed if recipes already exist
        if (context.Recipes.Any())
            return;

        // Create test user
        var user = await userManager.FindByEmailAsync("test@pantrypilot.com");

        if (user == null)
        {
            user = new ApplicationUser
            {
                UserName = "Test",
                Email = "test@pantrypilot.com"
            };

            await userManager.CreateAsync(user, "Test123!");
        }

        int userId = user.Id;

        // Ingredients
        var flour = new Ingredient { Name = "Flour", Category = IngredientCategory.Packaged };
        var egg = new Ingredient { Name = "Egg", Category = IngredientCategory.Dairy };
        var milk = new Ingredient { Name = "Milk", Category = IngredientCategory.Dairy };
        var lettuce = new Ingredient { Name = "Lettuce", Category = IngredientCategory.Produce };
        var pasta = new Ingredient { Name = "Pasta", Category = IngredientCategory.Packaged };

        context.Ingredients.AddRange(flour, egg, milk, lettuce, pasta);

        // Recipes
        var pancakes = new Recipe
        {
            Name = "Seed Pancakes",
            PrepTime = 10,
            CookTime = 5,
            Servings = 2,
            UserId = userId,
            RecipeIngredients = new List<RecipeIngredient>
            {
                new RecipeIngredient { Ingredient = flour, Quantity = 1, Unit = "cup" },
                new RecipeIngredient { Ingredient = egg, Quantity = 2, Unit = "each" },
                new RecipeIngredient { Ingredient = milk, Quantity = 1, Unit = "cup" },
            },
            Steps = new List<RecipeStep>
            {
                new RecipeStep { StepNumber = 1, Instruction = "Mix ingredients" },
                new RecipeStep { StepNumber = 2, Instruction = "Cook on skillet" }
            }

        };

        var salad = new Recipe
        {
            Name = "Seed Salad",
            PrepTime = 5,
            CookTime = 0,
            Servings = 1,
            UserId = userId,
            RecipeIngredients = new List<RecipeIngredient>
            {
                new RecipeIngredient { Ingredient = lettuce, Quantity = 1, Unit = "cup" }
            },
            Steps = new List<RecipeStep>
            {
                new RecipeStep { StepNumber = 1, Instruction = "chop lettuce" },
                new RecipeStep { StepNumber = 2, Instruction = "Serve Fresh" }
            }
        };

        var pastaRecipe = new Recipe
        {
            Name = "Seed Pasta",
            PrepTime = 12,
            CookTime = 15,
            Servings = 3,
            UserId = userId,
            RecipeIngredients = new List<RecipeIngredient>
            {
                new RecipeIngredient { Ingredient = pasta, Quantity = 2, Unit = "cups" }
            },
            Steps = new List<RecipeStep>
            {
                new RecipeStep { StepNumber = 1, Instruction = "Boil pasta" },
                new RecipeStep { StepNumber = 2, Instruction = "Drain and serve" }
            }
        };

        context.Recipes.AddRange(pancakes, salad, pastaRecipe)

        await context.SaveChangesAsync();


    }
}