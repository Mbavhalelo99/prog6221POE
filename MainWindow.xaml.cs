using System;
using System.Collections.Generic;
using System.Windows;

namespace RecipeApp
{
    public partial class MainWindow : Window
    {
        private RecipeIngre recipe;
        private List<RecipeIngre> recipes;

        public MainWindow()
        {
            InitializeComponent();
            recipe = new RecipeIngre();
            recipes = new List<RecipeIngre>();
        }

        private void BtnAddIngredients_Click(object sender, RoutedEventArgs e)
        {
            string recipeName = txtRecipeName.Text;
            int txtnumIngredients = int.Parse(txtNumIngredients.Text);

            recipe.Name = recipeName;

            for (int i = 0; i < txtnumIngredients; i++)
            {
                string ingredientName = PromptInput($"Enter the name of ingredient {i + 1}:");
                string unit = PromptInput($"Enter the unit of measurement for {ingredientName}:");

                var ingredients = recipe.Ingredients;
                ingredients.Add(new RecipeIngre.Ingredient
                {
                    Name = ingredientName,
                    Quantity = (double)double.Parse(PromptInput($"Enter the quantity of {ingredientName}:")),
                    OriginalQuantity = (double)double.Parse(PromptInput($"Enter the quantity of {ingredientName}:")),
                    Unit = unit
                });
            }
        }



        private void BtnAddSteps_Click(object sender, RoutedEventArgs e)
        {
            int numSteps = int.Parse(txtNumSteps.Text);

            for (int i = 0; i < numSteps; i++)
            {
                string step = PromptInput($"Enter Step {i + 1}:");
                recipe.Steps.Add(step);
            }
        }

        private void BtnDisplayRecipe_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(recipe.GetRecipeString(), "Recipe");
        }

        private void BtnScaleRecipe_Click(object sender, RoutedEventArgs e)
        {
            double scalingFactor = double.Parse(txtScalingFactor.Text);
            ScaleRecipe(recipe, scalingFactor);
            MessageBox.Show(recipe.GetRecipeString(), "Scaled Recipe");
        }

        private void BtnResetQuantities_Click(object sender, RoutedEventArgs e)
        {
            recipe.ResetQuantities();
            MessageBox.Show(recipe.GetRecipeString(), "Reset Quantities");
        }

        private void BtnAddRecipe_Click(object sender, RoutedEventArgs e)
        {
            RecipeIngre newRecipe = new RecipeIngre(PromptInput("Enter the recipe name:"));
            recipes.Add(newRecipe);
            lbRecipes.Items.Add(newRecipe.Name);
        }

        private void BtnListRecipes_Click(object sender, RoutedEventArgs e)
        {
            string recipeList = "Recipes:\n";

            foreach (RecipeIngre recipe in recipes)
            {
                recipeList += recipe.Name + "\n";
            }

            MessageBox.Show(recipeList, "Recipe List");
        }

        private void BtnScaleSelectedRecipe_Click(object sender, RoutedEventArgs e)
        {
            string recipeName = PromptInput("Enter the name of the recipe to scale:");
            RecipeIngre recipeToScale = recipes.Find(r => r.Name == recipeName);

            if (recipeToScale != null)
            {
                double scalingFactor = double.Parse(txtScalingFactor.Text);
                ScaleRecipe(recipeToScale, scalingFactor);
                MessageBox.Show(recipeToScale.GetRecipeString(), "Scaled Recipe");
            }
            else
            {
                MessageBox.Show("Recipe not found.", "Error");
            }
        }

        private string PromptInput(string prompt)
        {
            return MessageBox.Show(prompt, "Input", MessageBoxButton.OK, MessageBoxImage.Information).ToString();
        }

        private void ScaleRecipe(RecipeIngre recipe, double scalingFactor)
        {
            foreach (RecipeIngre.Ingredient ingredient in recipe.Ingredients)
            {
                ingredient.Quantity *= scalingFactor;
            }
        }
    }

    public class RecipeIngre
    {
        public string Name { get; set; }
        public List<Ingredient> Ingredients { get; set; }
        public List<string> Steps { get; set; }

        public RecipeIngre()
        {
            Ingredients = new List<Ingredient>();
            Steps = new List<string>();
        }

        public RecipeIngre(string name)
        {
            Name = name;
            Ingredients = new List<Ingredient>();
            Steps = new List<string>();
        }

        public void ResetQuantities()
        {
            foreach (Ingredient ingredient in Ingredients)
            {
                ingredient.Quantity = ingredient.OriginalQuantity;
            }
        }

        public string GetRecipeString()
        {
            string recipeString = "Recipe:\n";
            recipeString += $"Name: {Name}\n\n";

            recipeString += "Ingredients:\n";
            foreach (Ingredient ingredient in Ingredients)
            {
                recipeString += $"{ingredient.Quantity} {ingredient.Unit} of {ingredient.Name}\n";
            }

            recipeString += $"\nTotal Calories: {CalculateTotalCalories()}\n\n";

            recipeString += "Steps:\n";
            for (int i = 0; i < Steps.Count; i++)
            {
                recipeString += $"{i + 1}. {Steps[i]}\n";
            }

            return recipeString;
        }

        public int CalculateTotalCalories()
        {
            int totalCalories = 0;
            foreach (Ingredient ingredient in Ingredients)
            {
                totalCalories += ingredient.Calories;
            }
            return totalCalories;
        }


        public class Ingredient
        {
            public string? Name { get; set; }
            public double Quantity { get; set; }
            public double OriginalQuantity { get; set; }
            public string Unit { get; set; }
            public int Calories { get; internal set; }
        }
    }
}





