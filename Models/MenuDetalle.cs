using System.ComponentModel.DataAnnotations;

namespace OperacionTupper2._0.Models
{
    public class MenuDetalle
    {
        [Key]
        public int IdMenuDetalle { get; set; }
        public int IdMenu { get; set; }
        public int DiaSemana { get; set; } // Día 1, Día 2, Día 3...
        public Hora HoraComida { get; set; }
        public int IdPlato { get; set; }

        public Menu Menu { get; set; } = null!;
        public Plato Plato { get; set; } = null!;
    }

}
