namespace RecipeBook.Models
{
    public class Recipe
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }// = "./favicon.ico"; //The <img> tags seem to break if the value is null.
        public List<Ingredient> Ingredients { get; set; } = new List<Ingredient>();
        public string Instructions { get; set; }

    }
}
