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
    public class ReseñaController : Controller
    {
        private readonly TirriFashionWebJMContext _context;

        public ReseñaController(TirriFashionWebJMContext context)
        {
            _context = context;
        }

        // GET: Reseña
        public async Task<IActionResult> Index()
        {
            var tirriFashionWebJMContext = _context.Reseñas.Include(r => r.IdCatalogoNavigation).Include(r => r.IdUsuarioNavigation);
            return View(await tirriFashionWebJMContext.ToListAsync());
        }

        // GET: Reseña/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Reseñas == null)
            {
                return NotFound();
            }

            var reseña = await _context.Reseñas
                .Include(r => r.IdCatalogoNavigation)
                .Include(r => r.IdUsuarioNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reseña == null)
            {
                return NotFound();
            }

            return View(reseña);
        }

        // GET: Reseña/Create
        public IActionResult Create()
        {
            ViewData["IdCatalogo"] = new SelectList(_context.Catalogos, "Id", "Nombre");
            ViewData["IdUsuario"] = new SelectList(_context.Usuarios, "Id", "Nombre");
            return View();
        }

        // POST: Reseña/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IdCatalogo,IdUsuario,Comentario,Calificacion")] Reseña reseña)
        {

            _context.Add(reseña);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
            //if (ModelState.IsValid)
            //{

            //}
            //ViewData["IdCatalogo"] = new SelectList(_context.Catalogos, "Id", "Id", reseña.IdCatalogo);
            //ViewData["IdUsuario"] = new SelectList(_context.Usuarios, "Id", "Id", reseña.IdUsuario);
            return View(reseña);
        }

        // GET: Reseña/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Reseñas == null)
            {
                return NotFound();
            }

            var reseña = await _context.Reseñas.FindAsync(id);
            if (reseña == null)
            {
                return NotFound();
            }
            ViewData["IdCatalogo"] = new SelectList(_context.Catalogos, "Id", "Nombre", reseña.IdCatalogo);
            ViewData["IdUsuario"] = new SelectList(_context.Usuarios, "Id", "Nombre", reseña.IdUsuario);
            return View(reseña);
        }

        // POST: Reseña/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IdCatalogo,IdUsuario,Comentario,Calificacion")] Reseña reseña)
        {
            if (id != reseña.Id)
            {
                return NotFound();
            }

            try
            {
                _context.Update(reseña);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReseñaExists(reseña.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
            //ViewData["IdCatalogo"] = new SelectList(_context.Catalogos, "Id", "Id", reseña.IdCatalogo);
            //ViewData["IdUsuario"] = new SelectList(_context.Usuarios, "Id", "Id", reseña.IdUsuario);
            return View(reseña);
        }

        // GET: Reseña/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Reseñas == null)
            {
                return NotFound();
            }

            var reseña = await _context.Reseñas
                .Include(r => r.IdCatalogoNavigation)
                .Include(r => r.IdUsuarioNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reseña == null)
            {
                return NotFound();
            }

            return View(reseña);
        }

        // POST: Reseña/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Reseñas == null)
            {
                return Problem("Entity set 'TirriFashionWebJMContext.Reseñas'  is null.");
            }
            var reseña = await _context.Reseñas.FindAsync(id);
            if (reseña != null)
            {
                _context.Reseñas.Remove(reseña);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReseñaExists(int id)
        {
          return (_context.Reseñas?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
