using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TirriFashionWebJM.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace TirriFashionWebJM.Controllers
{
    [Authorize]
    public class UsuariosController : Controller
    {
        private readonly TirriFashionWebJMContext _context;

        public UsuariosController(TirriFashionWebJMContext context)
        {
            _context = context;
        }

        // GET: Usuarios
        public async Task<IActionResult> Index(Usuario usuario)
        {

            //esta parte es para el filtro
            var query = _context.Usuarios.AsQueryable();
            if (string.IsNullOrWhiteSpace(usuario.Nombre) == false)
            {
                query = query.Where(s => s.Nombre.Contains(usuario.Nombre));
            }
            if (string.IsNullOrWhiteSpace(usuario.Apellido) == false)
            {
                query = query.Where(s => s.Apellido.Contains(usuario.Apellido));
            }
            if (usuario.Estatus == 1 || usuario.Estatus == 2)
            {
                query = query.Where(s => s.Estatus == usuario.Estatus);
            }
            if (usuario.Take == 0)
                usuario.Take = 10;
            query = query.Take(usuario.Take);
            return query != null ?
                                    View(await query.ToListAsync()) :
                                    Problem("Entity set 'TirriFashionWebJMContext.Usuarios'  is null.");
        }

        // GET: Usuarios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Usuarios == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(m => m.Id == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        [AllowAnonymous]
        public async Task<IActionResult> Login(string ReturnUrl)
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            ViewBag.ReturnUrl = ReturnUrl;
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login([Bind("Email,Contraseña")] Usuario usuario, string ReturnUrl)
        {
            //Encriptar contraseña
            usuario.Contraseña = CalcularHashMD5(usuario.Contraseña);

            //que me inicie session todos los usuarios que coincidan EMail y contraseña
            var usuarioAut = await _context.Usuarios.FirstOrDefaultAsync(s => s.Email == usuario.Email && s.Contraseña == usuario.Contraseña && s.Estatus == 1);
            if (usuarioAut?.Id > 0 && usuarioAut.Email == usuario.Email)
            {
                var claims = new[] {
                    new Claim(ClaimTypes.Name, usuarioAut.Email),
                     new Claim(ClaimTypes.Role, usuarioAut.Rol),
                    new Claim("Id", usuarioAut.Id.ToString())
                    };
                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity), new AuthenticationProperties { IsPersistent = true }); ;
                var result = User.Identity.IsAuthenticated;
                if (!string.IsNullOrWhiteSpace(ReturnUrl))
                    return Redirect(ReturnUrl);
                else
                    return RedirectToAction("Index", "Home");
            }
            else
                ViewBag.Error = "Credenciales incorrectas";
            ViewBag.pReturnUrl = ReturnUrl;
            return View(usuario);
        }

        [Authorize(Roles = "Administrador")]
        // GET: Usuarios/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Usuarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Administrador")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Apellido,Edad,Email,Telefono,Contraseña,Rol,Estatus")] Usuario usuario)
        {
            usuario.Contraseña = CalcularHashMD5(usuario.Contraseña);

            _context.Add(usuario);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }

        [Authorize(Roles = "Administrador")]
        // GET: Usuarios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Usuarios == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }
            return View(usuario);
        }

        // POST: Usuarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Administrador")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Apellido,Edad,Email,Telefono,Rol,Estatus")] Usuario usuario)
        {
            if (id != usuario.Id)
            {
                return NotFound();
            }

            try
            {
                var usuarioData = await _context.Usuarios.FirstOrDefaultAsync(s => s.Id == id);
                usuarioData.Nombre = usuario.Nombre;
                usuarioData.Apellido = usuario.Apellido;
                usuarioData.Edad = usuario.Edad;
                usuarioData.Email = usuario.Email;
                usuarioData.Telefono = usuario.Telefono;
                usuarioData.Rol = usuario.Rol;
                usuarioData.Estatus = usuario.Estatus;
                _context.Update(usuarioData);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsuarioExists(usuario.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
            return View(usuario);
        }

        [Authorize(Roles = "Administrador")]
        // GET: Usuarios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Usuarios == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(m => m.Id == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // POST: Usuarios/Delete/5
        [Authorize(Roles = "Administrador")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Usuarios == null)
            {
                return Problem("Entity set 'TirriFashionWebJMContext.Usuarios'  is null.");
            }
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario != null)
            {
                _context.Usuarios.Remove(usuario);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsuarioExists(int id)
        {
            return (_context.Usuarios?.Any(e => e.Id == id)).GetValueOrDefault();
        }
        private string CalcularHashMD5(string texto)
        {
            using (MD5 md5 = MD5.Create())
            {
                // Convierte la cadena de texto a bytes
                byte[] inputBytes = Encoding.UTF8.GetBytes(texto);

                // Calcula el hash MD5 de los bytes
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Convierte el hash a una cadena hexadecimal
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("x2"));
                }

                return sb.ToString();
            }
        }
    }
}
