using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Warehouse.Data;
using Warehouse.Models;
using Warehouse.ViewModels;
using X.PagedList;

namespace Warehouse.Controllers
{
    public class MarcasController : Controller
    {
        private readonly WarehouseContext _context;
        private readonly IConfiguration _configuration; //se agrega para poder acceder a registros por pagina de la configuración.

        public MarcasController(WarehouseContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration; //se agrega para poder acceder a registros por pagina de la configuración.
        }

        // GET: Marcas
        public async Task<IActionResult> Index(ListadoMarcasViewModel ViewModel) //se crea un folder de ViewModels y se agrega la clase ListadoMarcasViewModel.cs
        {
            var registrosPorPagina = _configuration.GetValue<int>("RegistrosPorPagina", 5); //se agrega para poder acceder a registros por pagina de la configuración.

            var query = _context.Marcas.OrderBy(m => m.Nombre).AsQueryable();

            if (!String.IsNullOrEmpty(ViewModel.SearchTerm))
            {
                query = query.Where(m => m.Nombre.Contains(ViewModel.SearchTerm));
            }

            ViewModel.Total = query.Count();

            var numeroDePagina = ViewModel.Pagina ?? 1;

            ViewModel.Marcas = await query.ToPagedListAsync(numeroDePagina, registrosPorPagina);

            return View(ViewModel);
        }

        // GET: Marcas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Marcas == null)
            {
                return NotFound();
            }

            var marca = await _context.Marcas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (marca == null)
            {
                return NotFound();
            }

            return View(marca);
        }

        // GET: Marcas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Marcas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nombre")] Marca marca)
        {
            if (ModelState.IsValid)
            {

                //se agrega este bloque de codigo para evitar que se agregue una marca con el mismo nombre.
                if (_context.Marcas != null && _context.Marcas.Any(m => m.Nombre == marca.Nombre))
                {
                    ModelState.AddModelError("Nombre", "Ya existe una marca con el mismo nombre.");
                    return View(marca);
                }
                

                try
                {
                    _context.Add(marca);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException)
                {
                    ModelState.AddModelError("", "Lo sentimos, ha ocurrido un error. Intenta otra vez."); //se agrega ya que cuando hay una excepción de estas no es un error de programación, por lo general son factores externos.
                    return View(marca);
                }
                return RedirectToAction(nameof(Index));
            }
            return View(marca);
        }

        // GET: Marcas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Marcas == null)
            {
                return NotFound();
            }

            var marca = await _context.Marcas.FindAsync(id);
            if (marca == null)
            {
                return NotFound();
            }
            return View(marca);
        }

        // POST: Marcas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre")] Marca marca)
        {

           
            if (id != marca.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {

                //Este bloque de código se agrega para evitar que se agregue una marca con el mismo nombre. Agregue el Id en el query.
                if (_context.Marcas != null && _context.Marcas.Any(m => m.Nombre == marca.Nombre && m.Id != marca.Id))
                {
                    ModelState.AddModelError("Nombre", "Ya existe una marca con el mismo nombre.");
                    return View(marca);
                }



                try
                {
                    _context.Update(marca);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MarcaExists(marca.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(marca);
        }

        // GET: Marcas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Marcas == null)
            {
                return NotFound();
            }

            var marca = await _context.Marcas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (marca == null)
            {
                return NotFound();
            }

            return View(marca);
        }

        // POST: Marcas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Marcas == null)
            {
                return Problem("Entity set 'WarehouseContext.Marca'  is null.");
            }
            var marca = await _context.Marcas.FindAsync(id);
            if (marca != null)
            {
                _context.Marcas.Remove(marca);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MarcaExists(int id)
        {
          return (_context.Marcas?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
