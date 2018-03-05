using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProyectoWeb.Models;

namespace ProyectoWeb.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly ModeloContext _context;

        public UsuariosController(ModeloContext context)
        {
            _context = context;
        }

        // GET: Usuarios
        public async Task<IActionResult> Index()
        {
            return View(await _context.usuario.ToListAsync());
        }

        // GET: Usuarios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuarios = await _context.usuario
                .SingleOrDefaultAsync(m => m.ID == id);
            if (usuarios == null)
            {
                return NotFound();
            }

            return View(usuarios);
        }

        // GET: Usuarios/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Usuarios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Nombre,Apellidos,NicName,Password,TipoUsuario")] Usuarios usuarios)
        {
            if (ModelState.IsValid)
            {
                _context.Add(usuarios);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(usuarios);
        }

        // GET: Usuarios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuarios = await _context.usuario.SingleOrDefaultAsync(m => m.ID == id);
            if (usuarios == null)
            {
                return NotFound();
            }
            return View(usuarios);
        }

        // POST: Usuarios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Nombre,Apellidos,NicName,Password,TipoUsuario")] Usuarios usuarios)
        {
            if (id != usuarios.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(usuarios);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsuariosExists(usuarios.ID))
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
            return View(usuarios);
        }

        // GET: Usuarios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuarios = await _context.usuario
                .SingleOrDefaultAsync(m => m.ID == id);
            if (usuarios == null)
            {
                return NotFound();
            }

            return View(usuarios);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var usuarios = await _context.usuario.SingleOrDefaultAsync(m => m.ID == id);
            _context.usuario.Remove(usuarios);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsuariosExists(int id)
        {
            return _context.usuario.Any(e => e.ID == id);
        }


        [HttpPost, ActionName("Login")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([Bind("NicName, Password")] Usuarios usuario)
        {

            var usu = await _context.usuario
               .SingleOrDefaultAsync(m => m.NicName == usuario.NicName && m.Password == usuario.Password);

            if (usu != null)
            {
                HttpContext.Session.SetString("usuario", usu.ToString());
                if (usu.TipoUsuario == "admin")
                {
                    return RedirectToAction("Index");//redireccion para Admin
                    
                }
                else if (usu.TipoUsuario == "usuario")
                {
                    return UsuarioCorriente();
                }
            }
            else
            {
                ModelState.AddModelError("", "El Usuario o la contraseña son erroneas");
                return RedirectToAction("#");
            }
            return View();
        }

        /*
        *Redireccion a la pagina de usuario corriente
        *
        */
          public IActionResult UsuarioCorriente()
        {
           
            return View("UsuarioCorriente");
        }
       





    }
}
