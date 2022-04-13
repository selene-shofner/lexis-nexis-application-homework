namespace RecipeBook.Models
{
    public class Recipe
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public List<Ingredient> Ingredients { get; set; }
        public string Instructions { get; set; }

    }
}
