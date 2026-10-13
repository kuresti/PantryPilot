# PantryPilot

# Team Members
- Kimberly Uresti
- Austin Eaquinto
- Raquel Miller
- Tary Tupouata

**PantryPilot** is a collaborative meal-planning and recipe-management web application built with C#, ASP.NET Core Blazor, Entity Framework Core, and SQLite. It helps users organize recipes, plan meals on daily, weekly, and monthly calendars, and generate grocery lists from planned meals.

## Features

- **Accounts:** Registration, login, logout, and authenticated navigation.
- **Recipes:** Create, view, edit, and delete recipes, with ingredients and preparation steps.
- **Meal planning:** Assign recipes to meals with drag-and-drop interactions; view plans by day, week, or month.
- **Saved menus:** Retrieve a user's weekly menu and persist recipe assignments.
- **Menu management:** Remove a recipe from a planned meal without deleting the underlying recipe.
- **Grocery lists:** Generate a list from weekly planned meals, aggregate ingredient quantities, group by category, and print a formatted list.
- **Dashboard:** Account dashboard with widgets, including favorites and meal-related information.

Features were developed collaboratively; see **Team contributions** below.

## Technology stack

- **Language:** C#
- **Web application:** ASP.NET Core / Blazor (`.razor` components)
- **Data access:** Entity Framework Core and migrations
- **Database:** SQLite (`pantry.db` appears in the project history)
- **Authentication:** ASP.NET Core Identity
- **UI:** CSS and MudBlazor dashboard components
- **Collaboration:** Git, GitHub feature branches, pull requests, and code review

## Application structure

The repository separates responsibilities across:

```text
Components/          Blazor pages, layouts, widgets, and reusable UI
Data/                Database context and seed data
Models/              Application and persistence models
Services/            Business logic and service interfaces
Migrations/          Entity Framework Core database migrations
wwwroot/             Static assets and styling
Program.cs           Application startup and service registration
```

The application follows a component-and-service organization: Blazor pages and reusable components call injected services; services handle application logic and data access through Entity Framework Core.

## Getting started

1. Install the .NET SDK version required by `PantryPilot.csproj` (.NET 10).
2. Clone the repository:

   ```bash
   git clone https://github.com/kuresti/PantryPilot.git
   cd PantryPilot
   ```

3. Restore dependencies:

   ```bash
   dotnet restore
   ```

4. Check the connection string in `appsettings.json` or local configuration. Do not commit secrets or production credentials.
5. Apply migrations:

   ```bash
   dotnet ef database update
   ```

6. Run the app:

   ```bash
   dotnet run
   ```

## Team contributions

This was a **team project**. The responsibilities below are based on the Git commit history and pull-request descriptions, not a claim that any one person wrote every feature.

### Kim Uresti (`kuresti`) — Project foundation, architecture, and core implementation

Kim designed the initial project architecture and development workflow, set up the Blazor solution and repository documentation, and implemented substantial functionality across UI, services, authentication, and data integration.

**Documented contributions include:**

- **Project initialization and collaboration:** Set up the Blazor application, solution, initial README, and team contribution rules; helped standardize namespaces and organized services/interfaces for maintainability.
- **Authentication and user data:** Implemented registration, login, and logout functionality; integrated `ApplicationUser` / Identity-related model changes and migrations.
- **Meal-planning engine:** Implemented drag-and-drop recipe assignment to meal cards; added service methods to retrieve saved weekly menus, assign recipes to meals, and remove planned meals while preserving recipe records.
- **Reusable UI:** Built or substantially developed `MealCard`, `MenuRecipeList`, `DayMenuPanel`, `WeeklyMenuGrid`, `MonthlyCalendarGrid`, and `RecipeDetailsPanel`, including calendar navigation and daily/weekly/monthly views.
- **Data and integration:** Worked on service interfaces, weekly-menu user ownership, Entity Framework Core migrations, seed data, and service-to-component integration.
- **Debugging:** Fixed an issue where repeated recipes in a weekly menu were not fully counted in grocery-list ingredient totals.
- **Team integration:** Integrated contributions through branches, pull requests, and merges.

### Other team contributions

- **Raquel Miller:** Recipe create/edit form, recipe-list/detail pages, and later recipe-form enhancements and styling.
- **Tary Tapouata:** Ingredient and grocery-list services, grocery-list generation and aggregation, category grouping, printing, and related UI.
- **Austin Eaquinto:** Navigation and layout enhancements, account dashboard, MudBlazor widgets, favorites, and associated database changes.

These descriptions summarize documented contributions and are not intended as exhaustive credit for each contributor.

## Architecture and design decisions

- **Reusable Blazor components** keep calendar, meal-card, recipe-list, and detail displays modular.
- **Service interfaces** separate UI interactions from menu and recipe business logic.
- **Entity Framework Core** provides database persistence and migration management.
- **User-associated menus** support retrieving meal plans for the signed-in account.
- **Non-destructive menu removal** preserves a recipe even when it is removed from a specific meal plan.

## Development workflow

The team used GitHub branches and pull requests to integrate work. Contributors should create focused branches, explain changes in PR descriptions, review changes before merging, and test relevant functionality locally.

## Screenshots / demo

![1791922186577](image/PantryPilot_README_Proposed/1791922186577.png)
![1791922057213](image/PantryPilot_README_Proposed/1791922057213.png)
![1791922090791](image/PantryPilot_README_Proposed/1791922090791.png)
![1791922109814](image/PantryPilot_README_Proposed/1791922109814.png)
![1791922136345](image/PantryPilot_README_Proposed/1791922136345.png)

## Known limitations and next steps

Before publicizing this as a production-ready application, verify deployment configuration, error handling, authorization boundaries, test coverage, and the handling of any local database or secrets. A production deployment and automated test suite have **not** been verified from the supplied history.

---

**Repository:** https://github.com/kuresti/PantryPilot
