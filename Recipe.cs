namespace RecipeApp
{
    internal class Recipe
    {
        public string Name { get; internal set; }
        public object Ingredients { get; internal set; }

        internal class Ingredient
        {
            public double Quantity { get; internal set; }
        }
    }
}