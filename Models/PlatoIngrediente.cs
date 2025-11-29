using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OperacionTupper2._0.Models
{
    public class PlatoIngrediente
    {
        [Key]
        public int IdPlatoIngrediente { get; set; }
        [ForeignKey(nameof(Plato))]
        public int IdPlato { get; set; }
        [ForeignKey(nameof(Ingrediente))]
        public int IdIngrediente { get; set; }

        // Propiedades de navegación
        public Plato Plato { get; set; } = null!;
        public Ingrediente Ingrediente { get; set; } = null!;
    }
}
