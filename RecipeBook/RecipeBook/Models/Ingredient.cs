namespace RecipeBook.Models
{
    public class Ingredient
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Quantity { get; set; }
        public QuantityUnit QuantityUnit { get; set; }

    }
}
