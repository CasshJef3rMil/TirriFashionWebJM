using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TirriFashionWebJM.Models;

namespace TirriFashionWebJM.Controllers
{

    [Authorize]
    public class CategoriumsController : Controller
    {
        private readonly TirriFashionWebJMContext _context;

        public CategoriumsController(TirriFashionWebJMContext context)
        {
            _context = context;
        }

       
        // GET: Categoriums
        public async Task<IActionResult> Index()
        {
              return _context.Categoria != null ? 
                          View(await _context.Categoria.ToListAsync()) :
                          Problem("Entity set 'TirriFashionWebJMContext.Categoria'  is null.");
        }

        [Authorize(Roles = "Administrador,Usuario")]
        // GET: Categoriums/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Categoria == null)
            {
                return NotFound();
            }

            var categorium = await _context.Categoria
                .FirstOrDefaultAsync(m => m.Id == id);
            if (categorium == null)
            {
                return NotFound();
            }

            return View(categorium);
        }

        [Authorize(Roles = "Administrador")]
        // GET: Categoriums/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Categoriums/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Administrador")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre")] Categorium categorium)
        {
            if (ModelState.IsValid)
            {
                _context.Add(categorium);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(categorium);
        }

        [Authorize(Roles = "Administrador")]
        // GET: Categoriums/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Categoria == null)
            {
                return NotFound();
            }

            var categorium = await _context.Categoria.FindAsync(id);
            if (categorium == null)
            {
                return NotFound();
            }
            return View(categorium);
        }

        // POST: Categoriums/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        [Authorize(Roles = "Administrador")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre")] Categorium categorium)
        {
            if (id != categorium.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(categorium);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoriumExists(categorium.Id))
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
            return View(categorium);
        }

        [Authorize(Roles = "Administrador")] 
        // GET: Categoriums/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Categoria == null)
            {
                return NotFound();
            }

            var categorium = await _context.Categoria
                .FirstOrDefaultAsync(m => m.Id == id);
            if (categorium == null)
            {
                return NotFound();
            }

            return View(categorium);
        }

        // POST: Categoriums/Delete/5
        [Authorize(Roles = "Administrador")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Categoria == null)
            {
                return Problem("Entity set 'TirriFashionWebJMContext.Categoria'  is null.");
            }
            var categorium = await _context.Categoria.FindAsync(id);
            if (categorium != null)
            {
                _context.Categoria.Remove(categorium);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoriumExists(int id)
        {
          return (_context.Categoria?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
