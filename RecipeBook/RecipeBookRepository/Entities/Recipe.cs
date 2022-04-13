using System.ComponentModel.DataAnnotations;

namespace RecipeBook.Repository.Entities
{
    public class Recipe
    {
        [Key]
        public int Id { get; set; }

        [MinLength(4)]
        [MaxLength(50)]
        [Required]
        public string Title { get; set; }

        [MaxLength(3000)]
        public string Description { get; set; }

        [MaxLength(5000)]
        [Url]
        public string ImageUrl { get; set; }

        public ICollection<RecipeIngredient> Ingredients { get; set; }


        [MinLength(10)]
        [Required]
        public string Instructions { get; set; }

    }
}
