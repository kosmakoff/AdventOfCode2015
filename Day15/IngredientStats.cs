using System;

namespace Day15
{
    public struct IngredientStats
    {
        public int Capacity { get; }
        public int Durability { get; }
        public int Flavor { get; }
        public int Texture { get; }
        public int Calories { get; }

        public IngredientStats(int capacity, int durability, int flavor, int texture, int calories)
        {
            Capacity = capacity;
            Durability = durability;
            Flavor = flavor;
            Texture = texture;
            Calories = calories;
        }

        public static IngredientStats operator *(IngredientStats stats, int multiplier)
        {
            return new IngredientStats(
                stats.Capacity * multiplier,
                stats.Durability * multiplier,
                stats.Flavor * multiplier,
                stats.Texture * multiplier,
                stats.Calories * multiplier);
        }

        public static IngredientStats operator +(IngredientStats stats1, IngredientStats stats2)
        {
            return new IngredientStats(
                stats1.Capacity + stats2.Capacity,
                stats1.Durability + stats2.Durability,
                stats1.Flavor + stats2.Flavor,
                stats1.Texture + stats2.Texture,
                stats1.Calories + stats2.Calories);
        }

        public IngredientStats Capped()
        {
            return new IngredientStats(
                Math.Max(0, Capacity),
                Math.Max(0, Durability),
                Math.Max(0, Flavor),
                Math.Max(0, Texture),
                Math.Max(0, Calories));
        }
    }
}
