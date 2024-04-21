using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TirriFashionWebJM.Models;

namespace TirriFashionWebJM.Controllers
{
    public class CatalogoesController : Controller
    {
        private readonly TirriFashionWebJMContext _context;

        public CatalogoesController(TirriFashionWebJMContext context)
        {
            _context = context;
        }

        // GET: Catalogoes
        public async Task<IActionResult> Index()
        {
            var tirriFashionWebJMContext = _context.Catalogos.Include(c => c.IdCategoriaNavigation).Include(c => c.IdUsuarioNavigation);
            return View(await tirriFashionWebJMContext.ToListAsync());
        }

        // GET: Catalogoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Catalogos == null)
            {
                return NotFound();
            }

            var catalogo = await _context.Catalogos
                .Include(c => c.IdCategoriaNavigation)
                .Include(c => c.IdUsuarioNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (catalogo == null)
            {
                return NotFound();
            }

            return View(catalogo);
        }

        // GET: Catalogoes/Create
        public IActionResult Create()
        {
            ViewData["IdCategoria"] = new SelectList(_context.Categoria, "Id", "Nombre");
            ViewData["IdUsuario"] = new SelectList(_context.Usuarios, "Id", "Nombre");
            return View();
        }

        // POST: Catalogoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Descripcion,AñoFabricacion,IdUsuario,IdCategoria")] Catalogo catalogo, IFormFile imagen)
        {
            if (imagen != null && imagen.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await imagen.CopyToAsync(memoryStream);
                    catalogo.Imagen = memoryStream.ToArray();
                }
            }



            _context.Add(catalogo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
            //if (ModelState.IsValid)
            //{

            //}
            //ViewData["CategoriaId"] = new SelectList(_context.Categorias, "CategoriaId", "Nombre", producto.CategoriaId);
            //return View(producto);
        }

        // GET: Catalogoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Catalogos == null)
            {
                return NotFound();
            }

            var catalogo = await _context.Catalogos.FindAsync(id);
            if (catalogo == null)
            {
                return NotFound();
            }
            ViewData["IdCategoria"] = new SelectList(_context.Categoria, "Id", "Nombre", catalogo.IdCategoria);
            ViewData["IdUsuario"] = new SelectList(_context.Usuarios, "Id", "Nombre", catalogo.IdUsuario);
            return View(catalogo);
        }

        // POST: Catalogoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Imagen,Descripcion,AñoFabricacion,IdUsuario,IdCategoria")] Catalogo catalogo, IFormFile imagen)
        {
            if (id != catalogo.Id)
            {
                return NotFound();
            }

            var existingCatalogo = await _context.Catalogos.FindAsync(id);

            if (existingCatalogo == null)
            {
                return NotFound();
            }

            if (imagen != null && imagen.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await imagen.CopyToAsync(memoryStream);
                    existingCatalogo.Imagen = memoryStream.ToArray();
                }
            }

            existingCatalogo.Nombre = catalogo.Nombre;
            existingCatalogo.Descripcion = catalogo.Descripcion;
            existingCatalogo.AñoFabricacion = catalogo.AñoFabricacion;
            existingCatalogo.IdUsuario = catalogo.IdUsuario;
            existingCatalogo.IdCategoria = catalogo.IdCategoria;

            try
            {
                _context.Update(existingCatalogo);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CatalogoExists(catalogo.Id))
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
        // GET: Catalogoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Catalogos == null)
            {
                return NotFound();
            }

            var catalogo = await _context.Catalogos
                .Include(c => c.IdCategoriaNavigation)
                .Include(c => c.IdUsuarioNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (catalogo == null)
            {
                return NotFound();
            }

            return View(catalogo);
        }

        // POST: Catalogoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Catalogos == null)
            {
                return Problem("Entity set 'TirriFashionWebJMContext.Catalogos'  is null.");
            }
            var catalogo = await _context.Catalogos.FindAsync(id);
            if (catalogo != null)
            {
                _context.Catalogos.Remove(catalogo);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CatalogoExists(int id)
        {
          return (_context.Catalogos?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
