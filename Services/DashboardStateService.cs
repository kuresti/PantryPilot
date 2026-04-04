
namespace PantryPilot.Services
{
    public class DashboardStateService
    {
        // the list of widgets that can be created/appear
        public enum DashboardState { FavoriteRecipes, Ingredients, DinnerSuggestion, CookingTips }
        public List<DashboardState> ActiveWidgets { get; private set; }
        public event Action? OnChange;
        
        // hardcoded for testing the UI layout
        public DashboardStateService()
        {
            // removes the need for a '?' in the ActiveWidgets property
            ActiveWidgets = new List<DashboardState>
            {
                DashboardState.FavoriteRecipes,
                DashboardState.DinnerSuggestion,
                DashboardState.Ingredients,
                DashboardState.CookingTips
            };
        }

        public void ToggleWidget(DashboardState widget)
        {
            if (ActiveWidgets.Contains(widget))
            {
                ActiveWidgets.Remove(widget);
            }
            else
            {
                ActiveWidgets.Add(widget);
            }

            // trigger the created event change/check for an event change
            OnChange?.Invoke();
        }
    }
}