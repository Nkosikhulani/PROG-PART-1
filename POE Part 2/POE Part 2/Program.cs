class Part2 
{
    static void Main(string[] args)
    {
        Dictionary<string, Recipe> recipeDictionary = new Dictionary<string, Recipe>();
        AppMenu menu = new AppMenu(recipeDictionary);
        menu.appMenu();

    }

}
class NumOfRecipes
{
    private Dictionary<string, Recipe> recipeDictionary;

    public NumOfRecipes(Dictionary<string, Recipe> recipeDictionary)
    {
        this.recipeDictionary = recipeDictionary;
    }

    public void LogDetails()
    {  //Allows user to enter the number of recipes
        Console.Write("Enter the number of recipes: ");
        int repNum;
        if (int.TryParse(Console.ReadLine(), out repNum))
        {
            for (int i = 0; i < repNum; i++)
            { //allows user to enter the name of the recipe
                Console.Write("Enter Recipe Name: ");
                string repName = Console.ReadLine();
                Recipe recipe = new Recipe();
                recipe.DetailEntry();
                recipeDictionary.Add(repName, recipe);
            }

            string answer;
            do
            { //allow user too either display their ingredients and steps or not
                Console.WriteLine("Display Ingredients and steps? (Yes/No)");
                answer = Console.ReadLine();
                switch (answer)
                {
                    case "Yes":
                        foreach (var recipeEntry in recipeDictionary)
                        {
                            Console.WriteLine($"Recipe Name: {recipeEntry.Key}");
                            recipeEntry.Value.DisplayRecipe();
                        }
                        break;
                    case "No":
                        AppMenu menu = new AppMenu(recipeDictionary);
                        menu.appMenu();
                        break;
                    default: //if you enter nothing the programm notices and then asks you to select between yes or no
                        Console.WriteLine("Please select a valid input");
                        break;
                }
            } while (answer != "No");
        }
        else
        { //allows user to enter a number 
            Console.WriteLine("Please enter a number");
        }
    }

    public void RecipeList()
    {
        foreach (var recipeEntry in recipeDictionary)
        {
            Console.WriteLine($"Recipe Name: {recipeEntry.Key}");
        }
    }

    public void ChooseRecipe()
    {
        Console.Write("Please enter the name of the recipe: ");
        string recipeName = Console.ReadLine();
        if (recipeDictionary.ContainsKey(recipeName))
        {
            Console.WriteLine($"Recipe Name: {recipeName}");
            recipeDictionary[recipeName].DisplayRecipe();
        }
        else
        {
            Console.WriteLine("Recipe does not exist");
        }
    }
}

class IngMenu
{
    private Dictionary<string, Recipe> recipeDictionary;

    public IngMenu(Dictionary<string, Recipe> recipeDictionary)
    {
        this.recipeDictionary = recipeDictionary;
        Recipe recipe = new Recipe();
        while (true)
        {
            Console.WriteLine("===================================");
            Console.WriteLine("Enter '1' to enter recipe details");
            Console.WriteLine("Enter '2' to display recipe");
            Console.WriteLine("Enter '3' to scale recipe");
            Console.WriteLine("Enter '4' to reset quantities");
            Console.WriteLine("Enter '5' to clear recipe");
            Console.WriteLine("Enter '6' for Main Menu");
            Console.WriteLine("===================================");

            string ans = Console.ReadLine();
            Console.WriteLine("                                     ");
            switch (ans)
            {
                case "1":
                    recipe.DetailEntry();
                    break;
                case "2":
                    recipe.DisplayRecipe();
                    break;
                case "3": 
                    Console.Write("Enter scaling factor (0.5, 2, or 3): ");
                    double scale1;
                    if (double.TryParse(Console.ReadLine(), out scale1))
                    {
                        recipe.ScaleRecipe(scale1);
                    }
                    else
                    {
                        Console.WriteLine("\nPlease Enter a valid number\n");
                    }
                    break;
                case "4":
                    recipe.ResetQuantities();
                    break;
                case "5":
                    recipe.ClearRecipe();
                    break;
                case "6":
                    AppMenu appMenu = new AppMenu(recipeDictionary);
                    appMenu.appMenu();
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please enter a valid choice.");
                    break;
            }
        }
    }
}

public class Recipe
{ //the following code will calculate the required different methods, therefore the methdos will then call out the main statement
    private string[] ingredients;
    private double[] amount;
    private string[] units;
    private string[] steps;
    private double[] calories;
    private string[] foodGroup;

    public Recipe()
    {
        //Initialize empty arrays for ingredients, quantities, units, and steps
        ingredients = new string[0];
        amount = new double[0];
        units = new string[0];
        calories = new double[0];
        foodGroup = new string[0];
        steps = new string[0];
    }

    public void DetailEntry()
    { //allows a user to enter the number of ingredients they wish to input
        Console.Write("Enter the number of ingredients: ");
        int ingNum;
        if (int.TryParse(Console.ReadLine(), out ingNum))
        {
            ingredients = new string[ingNum];
            amount = new double[ingNum];
            units = new string[ingNum];
            calories = new double[ingNum];
            foodGroup = new string[ingNum];

            for (int i = 0; i < ingNum; i++)
            {
                Console.WriteLine($"Enter details for ingredient #{i + 1}:");
                Console.Write("Name: ");
                ingredients[i] = Console.ReadLine();
                do
                {
                    Console.Write("Quantity: ");
                }
                while (!double.TryParse(Console.ReadLine(), out amount[i]));
                Console.Write("Unit of measurement: ");
                units[i] = Console.ReadLine();
                do
                {
                    Console.Write("Number of Calories: ");
                }
                while (!double.TryParse(Console.ReadLine(), out calories[i]));
                Console.Write("Food Group: ");
                foodGroup[i] = Console.ReadLine();
            }

            double calExceed = TotalCalories(calories);
            Console.WriteLine("TOTAL CALORIES: " + calExceed);
            if (calExceed > 300)
            {
                Console.WriteLine("!!!-TOTAL CALORIES EXCEED 300-!!!");
            }

            int Stnum;
            do
            { //allows  a user to enter the amount of steps
                Console.Write("Enter the number of steps: ");
            }
            while (!int.TryParse(Console.ReadLine(), out Stnum));

            steps = new string[Stnum];

            for (int i = 0; i < Stnum; i++)
            {
                Console.Write($"Enter step #{i + 1}: ");
                steps[i] = Console.ReadLine();
            }
        }
        else
        {
            Console.WriteLine("Please enter a number");
        }
    }

    public double TotalCalories(double[] calories)
    {
        double result = 0;
        for (int i = 0; i < calories.Length; i++)
        {
            result += calories[i];
        }
        return result;
    }

    public void DisplayRecipe()
    {
        Console.WriteLine("Ingredients:");
        for (int i = 0; i < ingredients.Length; i++)
        {
            Console.WriteLine($"- {amount[i]} {units[i]} of {ingredients[i]} at {calories[i]} Calories, Food Group: {foodGroup[i]}");
        }

        Console.WriteLine("Total Calories:");
        double result = 0;
        for (int i = 0; i < calories.Length; i++)
        {
            result += calories[i];
        }
        Console.WriteLine(result);

        if (result > 300)
        {
            Console.WriteLine("!!!-TOTAL CALORIES EXCEED 300-!!!");
        }

        Console.WriteLine("Steps:");
        for (int i = 0; i < steps.Length; i++)
        {
            Console.WriteLine($"- {steps[i]}");
        }
    }

    public void ScaleRecipe(double scale)
    {
        for (int i = 0; i < amount.Length; i++)
        {
            amount[i] *= scale;
        }
    }

    public void ResetQuantities()
    { //this will reset all the amount to their original values 
        for (int i = 0; i < amount.Length; i++)
        {
            amount[i] /= 2;
        }
    }

    public void ClearRecipe()
    { //this will reset all the arrays to become empty
        ingredients = new string[0];
        amount = new double[0];
        units = new string[0];
        steps = new string[0];
    }
}

class AppMenu
{
    private Dictionary<string, Recipe> recipeDictionary;
    private NumOfRecipes rep;

    public AppMenu(Dictionary<string, Recipe> recipeDictionary)
    {
        this.recipeDictionary = recipeDictionary;
        rep = new NumOfRecipes(recipeDictionary);
    }

    public void appMenu()
    {
        while (true)
        {
            Console.WriteLine("========================================================================================");
            Console.WriteLine("RECIPE APP");
            Console.WriteLine("========================================================================================");
            Console.WriteLine("1) Create Recipe");
            Console.WriteLine("2) Find Recipe");
            Console.WriteLine("3) Display All Recipes");
            Console.WriteLine("4) Exit App");
            Console.WriteLine("========================================================================================");
            Console.Write("Please Select Option:");
            string ans = Console.ReadLine();
            switch (ans)
            {
                case "1":
                    rep.LogDetails();
                    break;
                case "2":
                    rep.ChooseRecipe();
                    break;
                case "3":
                    rep.RecipeList();
                    break;
                case "4":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Please select a valid input");
                    break;
            }
        }
    }
}