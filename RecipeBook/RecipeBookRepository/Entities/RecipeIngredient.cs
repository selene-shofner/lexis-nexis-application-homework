using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecipeBook.Repository.Entities
{
    public class RecipeIngredient
    {
        [Key]
        public int Id { get; set; }

        [MinLength(4)]
        [MaxLength(500)]
        [Required]
        public string Name { get; set; }

        [Required]
        public float Quantity { get; set; }

        [Required]
        public QuantityUnit QuantityUnit { get; set; }

        [Required]
        [Url]
        public int RecipeId { get; set; }

        [ForeignKey(nameof(RecipeId))]
        public Recipe Recipe { get; set; }

    }
}
