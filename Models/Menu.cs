using System.ComponentModel.DataAnnotations;

namespace OperacionTupper2._0.Models
{
    public class Menu
    {
        [Key]
        public int IdMenu { get; set; }

        [StringLength(1000)]
        [Display(Name = "Nombre Menú")]
        public string NombreMenu { get; set; } = string.Empty;
        [Display(Name = "Numero de Días menu ")]
        [Required]
        public int DiasMenu { get; set; }

    }
}
