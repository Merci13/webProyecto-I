using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProyectoWeb.Models;

namespace ProyectoWeb.Controllers
{
    public class ReunionesController : Controller
    {
        private readonly ModeloContext _context;

        public ReunionesController(ModeloContext context)
        {
            _context = context;
        }

        // GET: Reuniones
        public async Task<IActionResult> Index()
        {
            var modeloContext = _context.reunion.Include(r => r.Usuario);
            return View(await modeloContext.ToListAsync());
        }

        // GET: Reuniones/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reuniones = await _context.reunion
                .Include(r => r.Usuario)
                .SingleOrDefaultAsync(m => m.ID == id);
            if (reuniones == null)
            {
                return NotFound();
            }

            return View(reuniones);
        }

        // GET: Reuniones/Create
        public IActionResult Create()
        {
            ViewData["UsuarioID"] = new SelectList(_context.usuario, "ID", "Nombre");
            return View();
        }

        // POST: Reuniones/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,TituloDeLaReunion,Fecha,UsuarioAsignado,Virtual,UsuarioID")] Reuniones reuniones)
        {
            if (ModelState.IsValid)
            {
                _context.Add(reuniones);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UsuarioID"] = new SelectList(_context.usuario, "ID", "ID", reuniones.UsuarioID);
            return View(reuniones);
        }

        // GET: Reuniones/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reuniones = await _context.reunion.SingleOrDefaultAsync(m => m.ID == id);
            if (reuniones == null)
            {
                return NotFound();
            }
            ViewData["UsuarioID"] = new SelectList(_context.usuario, "ID", "ID", reuniones.UsuarioID);
            return View(reuniones);
        }

        // POST: Reuniones/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,TituloDeLaReunion,Fecha,UsuarioAsignado,Virtual,UsuarioID")] Reuniones reuniones)
        {
            if (id != reuniones.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reuniones);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReunionesExists(reuniones.ID))
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
            ViewData["UsuarioID"] = new SelectList(_context.usuario, "ID", "ID", reuniones.UsuarioID);
            return View(reuniones);
        }

        // GET: Reuniones/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reuniones = await _context.reunion
                .Include(r => r.Usuario)
                .SingleOrDefaultAsync(m => m.ID == id);
            if (reuniones == null)
            {
                return NotFound();
            }

            return View(reuniones);
        }

        // POST: Reuniones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reuniones = await _context.reunion.SingleOrDefaultAsync(m => m.ID == id);
            _context.reunion.Remove(reuniones);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReunionesExists(int id)
        {
            return _context.reunion.Any(e => e.ID == id);
        }
    }
}
