using System.ComponentModel.DataAnnotations;

namespace OperacionTupper2._0.Models
{
    public class Ingrediente
    {

        [Key]
        public int IdIngrediente { get; set; }

        [Required]
        [StringLength(1000)]
        [Display(Name = "Nombre Ingrediente")]
        public string NombreIngrediente { get; set; } = string.Empty;
    }
}
