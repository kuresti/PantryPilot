namespace PantryPilot.Models
{
    public class RecipeStep
    {
        public int Id { get; set; }
        public int StepNumber { get; set; }
        public string Instruction { get; set; }

        public int RecipeId { get; set; }
        public Recipe Recipe { get; set; }
    }
}