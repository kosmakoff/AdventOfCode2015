namespace Day15
{
    public class Ingredient
    {
        public string Name { get; }
        public IngredientStats Stats { get; }

        public Ingredient(string name, IngredientStats stats)
        {
            Name = name;
            Stats = stats;
        }
    }
}
