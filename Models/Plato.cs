using System.ComponentModel.DataAnnotations;

namespace OperacionTupper2._0.Models
{
    public enum Hora
    {
        Comida,
        Cena,
        Ambos
    }

    public enum Acompanamiento
    {
        [Display(Name = "Acompañante")]
        Acompanante,
        [Display(Name = "Plato principal")]
        Principal
    }
    public enum Unico
    {
        Si,
        No
    }
    public enum Nutricional
    {
        [Display(Name = "Hidratos de carbono")]
        Hidratos,
        [Display(Name = "Carne")]
        Carnes,
        [Display(Name = "Verdura")]
        Verduras,
        [Display(Name = "Pescado")]
        Pescados,
        [Display(Name = "Mixto")]
        Variado
    }
    public class Plato
    {
        [Key]
        public int IdPlato { get; set; }
        [Required]
        [StringLength(1000)]
        [Display(Name = "Nombre")]
        public string NombrePlato { get; set; } = string.Empty;
        [Required]
        [Display(Name = "Hora del día")]
        public Hora HoraComida { get; set; }

        [Required]
        [Display(Name = "Acompañamiento o Principal")]
        public Acompanamiento AcompanamientoPrincipal { get; set; }
        [Required]
        [Display(Name = "¿Plato Único?")]
        public Unico PlatoUnico { get; set; }

        [Required]
        [Display(Name = "Grupo Nutricional")]
        public Nutricional GrupoNutricional { get; set; }

        [StringLength(10000)]
        public string Descripcion { get; set; } = string.Empty;
        [StringLength(2048)]
        public string Url { get; set; } = string.Empty;

        public ICollection<PlatoIngrediente> PlatosIngredientes { get; set; } = new List<PlatoIngrediente>();

    }
}
