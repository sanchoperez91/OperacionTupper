using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using OperacionTupper2._0.Models;

namespace OperacionTupper2._0.ViewModels
{
    public class PlatoEditViewModel
    {
        // Los datos reales del plato (lo que se guarda en BD)
        public Plato Plato { get; set; } = new Plato();

        // IDs de ingredientes seleccionados que vendrán del formulario
        public List<int> IngredienteIds { get; set; } = new List<int>();

        // Lista para pintar los checkboxes o un <select multiple> en la vista
        public List<SelectListItem> IngredientesDisponibles { get; set; } = new List<SelectListItem>();
    }
}
