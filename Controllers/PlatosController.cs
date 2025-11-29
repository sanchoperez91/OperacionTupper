using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OperacionTupper2._0.Data;
using OperacionTupper2._0.Models;
using OperacionTupper2._0.ViewModels;


namespace OperacionTupper2._0.Controllers
{
    public class PlatosController : Controller
    {
        private readonly OperacionTupperContext _context;

        public PlatosController(OperacionTupperContext context)
        {
            _context = context;
        }

        // GET: Platos
        public async Task<IActionResult> Index()
        {
            return View(await _context.Platos.ToListAsync());
        }

        // GET: Platos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var plato = await _context.Platos
                .Include(p => p.PlatosIngredientes)
                    .ThenInclude(pi => pi.Ingrediente)
                .FirstOrDefaultAsync(m => m.IdPlato == id);
            if (plato == null)
            {
                return NotFound();
            }

            return View(plato);
        }

        // GET: Platos/Create
        public IActionResult Create()
        {
            // 1) Traemos los ingredientes de la BD y los convertimos a SelectListItem
            var listaIngredientes = _context.Ingredientes
                .OrderBy(i => i.NombreIngrediente)
                .Select(i => new SelectListItem
                {
                    Value = i.IdIngrediente.ToString(), // valor que se enviará en el formulario
                    Text = i.NombreIngrediente          // texto que verá el usuario
                })
                .ToList();

            // 2) Creamos el ViewModel que usará la vista
            var vm = new PlatoEditViewModel
            {
                Plato = new Plato(),                     // plato vacío
                IngredientesDisponibles = listaIngredientes,
                IngredienteIds = new List<int>()         // aún no hay ingredientes seleccionados
            };

            // 3) Enviamos el ViewModel a la vista
            return View(vm);
        }


        // POST: Platos/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PlatoEditViewModel vm)
        {
            // Si el formulario tiene fallos, hay que recargar los ingredientes
            if (!ModelState.IsValid)
            {
                vm.IngredientesDisponibles = _context.Ingredientes
                    .OrderBy(i => i.NombreIngrediente)
                    .Select(i => new SelectListItem
                    {
                        Value = i.IdIngrediente.ToString(),
                        Text = i.NombreIngrediente
                    })
                    .ToList();

                // Devuelvo la vista con el modelo para que muestre errores
                return View(vm);
            }

            // 1) Guardar el plato en la BD
            _context.Add(vm.Plato);
            await _context.SaveChangesAsync(); // Se genera IdPlato automáticamente

            // 2) Guardar los ingredientes seleccionados en la tabla puente
            if (vm.IngredienteIds != null && vm.IngredienteIds.Any())
            {
                foreach (var idIng in vm.IngredienteIds)
                {
                    var relacion = new PlatoIngrediente
                    {
                        IdPlato = vm.Plato.IdPlato,
                        IdIngrediente = idIng
                    };

                    _context.PlatoIngredientes.Add(relacion);
                }

                await _context.SaveChangesAsync();
            }

            // 3) Volver al listado
            return RedirectToAction(nameof(Index));
        }
        // GET: Platos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // 1) Buscar el plato
            var plato = await _context.Platos.FindAsync(id);
            if (plato == null)
            {
                return NotFound();
            }

            // 2) Obtener los IDs de ingredientes que ya tiene ese plato
            var ingredientesSeleccionados = await _context.PlatoIngredientes
                .Where(pi => pi.IdPlato == plato.IdPlato)
                .Select(pi => pi.IdIngrediente)
                .ToListAsync();

            // 3) Obtener todos los ingredientes disponibles
            var ingredientesDisponibles = await _context.Ingredientes
                .OrderBy(i => i.NombreIngrediente)
                .Select(i => new SelectListItem
                {
                    Value = i.IdIngrediente.ToString(),
                    Text = i.NombreIngrediente
                })
                .ToListAsync();

            // 4) Construir el ViewModel
            var vm = new PlatoEditViewModel
            {
                Plato = plato,
                IngredienteIds = ingredientesSeleccionados,
                IngredientesDisponibles = ingredientesDisponibles
            };

            // 5) Enviar el ViewModel a la vista
            return View(vm);
        }

        // POST: Platos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, PlatoEditViewModel vm)
        {
            if (id != vm.Plato.IdPlato)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                // Si hay errores, recargar la lista de ingredientes
                vm.IngredientesDisponibles = await _context.Ingredientes
                    .OrderBy(i => i.NombreIngrediente)
                    .Select(i => new SelectListItem
                    {
                        Value = i.IdIngrediente.ToString(),
                        Text = i.NombreIngrediente
                    })
                    .ToListAsync();

                return View(vm);
            }

            // 1) Actualizar datos del plato
            _context.Update(vm.Plato);
            await _context.SaveChangesAsync();

            // 2) Ingredientes seleccionados actualmente
            var actual = await _context.PlatoIngredientes
                .Where(pi => pi.IdPlato == vm.Plato.IdPlato)
                .Select(pi => pi.IdIngrediente)
                .ToListAsync();

            var nuevos = vm.IngredienteIds ?? new List<int>();

            // 3) Ingredientes que hay que agregar
            var agregar = nuevos.Except(actual);

            foreach (var idIng in agregar)
            {
                _context.PlatoIngredientes.Add(new PlatoIngrediente
                {
                    IdPlato = vm.Plato.IdPlato,
                    IdIngrediente = idIng
                });
            }

            // 4) Ingredientes que hay que quitar
            var quitar = actual.Except(nuevos);

            foreach (var idIng in quitar)
            {
                var relacion = await _context.PlatoIngredientes
                    .FirstAsync(pi =>
                        pi.IdPlato == vm.Plato.IdPlato &&
                        pi.IdIngrediente == idIng);

                _context.PlatoIngredientes.Remove(relacion);
            }

            // 5) Guardar cambios
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


        // GET: Platos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var plato = await _context.Platos
                .FirstOrDefaultAsync(m => m.IdPlato == id);
            if (plato == null)
            {
                return NotFound();
            }

            return View(plato);
        }

        // POST: Platos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var plato = await _context.Platos.FindAsync(id);
            if (plato != null)
            {
                _context.Platos.Remove(plato);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PlatoExists(int id)
        {
            return _context.Platos.Any(e => e.IdPlato == id);
        }

        public IActionResult Buscar(string busqueda)
        {
            var resultados = _context.Platos
            .Where(p => p.NombrePlato.Contains(busqueda)).ToList();

            return View(resultados);

           
        }

    }
}
