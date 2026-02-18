// Add the following depending on the DB setup

// Add using EntityFrameworkCore
// Add using Microsoft.Extensions.DependencyInjections
// Add using System
// Add using System.Linq


// Also add the namespace name and class SeedData which
// should include initialization of the IServiceProvider

using System.ComponentModel;
using BlazorApp.Components.Pages;

namespace PantryPilot.Data;

public static class SeedData
{
    public static void Initialize(PantryPilotContext db)
    {
        //--------------------------
        // Recipes seed data
        //--------------------------
        var pancakes = new Recipe
        {
            Name = "Pancakes",
            Description = "Light fluffy pancakes",
            PrepTime = 10,
            CookTime = 15,
            Servings = 4
        };

        context.RecipeIngredients.AddRange(

        // Pancakes
        new RecipeIngredient
        {
            RecipeId = pancakes.Id,
            IngredientId = flour.Id,
            Quantity = 2,
            Unit = "cups"
        },

        new RecipeIngredient
        {
            RecipeId = pancakes.Id,
            IngredientId = milk.Id,
            Quantity = 1.5m,
            Unit = "cups",
        },

        new RecipeIngredient
        {
            RecipeId = pancakes.Id,
            IngredientId = eggs.Id,
            Quantity = 2,
            Unit = "large"
        }
    );
    }
}

public class Recipe
{
    public int Id { get; setl; }
    public string Name { get; set; }
    public string Description { get; set; }

    public int PrepTime { get; set; }
    public int CookTime { get; set; }
    public int Servings { get; set; }

    public List<RecipeIngredient> RecipeIngredients { get; set; } = new();
    public List<RecipeStep> Steps { get; set; } = new();
}

public class Ingredient
{
    public int Id { get; set; }
    public string Name { get; set; }

    public List<RecipeIngredient> RecipeIngredients { get; set; } = new();
}

//RecipeIngredient (JoinTable)
public class RecipeIngredient
{
    public int Id { get; set; }
    
    public int RecipeId { get; set; }
    public Recipe Recipe { get; set; }

    public int IngredientsId { get; set; }
    public Ingredient Ingredient { get; set; }

    public decimal Quantity { get; set; }
    public string Unit { get; set; }
}

public class RecipeStep
{
    public int Id { get; set; }
    public int StepNumber { get; set; }
    public string Instruction { get; set; }

    public int RecipeId { get; set; }
    public Recipe Recipe { get; set; }
}