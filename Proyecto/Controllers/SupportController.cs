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
    public class SupportController : Controller
    {
        private readonly ModeloContext _context;

        public SupportController(ModeloContext context)
        {
            _context = context;
        }

        // GET: Soporte
        public async Task<IActionResult> Index()
        {
            var dataMvc = _context.soporte.Include(s => s.Cliente);
            return View(await dataMvc.ToListAsync());
        }

        // GET: Soporte/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var soporte = await _context.soporte
                .Include(s => s.Cliente)
                .SingleOrDefaultAsync(m => m.ID == id);
            if (soporte == null)
            {
                return NotFound();
            }

            return View(soporte);
        }

        // GET: Soporte/Create
        public IActionResult Create()
        {
            ViewData["ClienteID"] = new SelectList(_context.cliente, "ID", "Nombre");
            return View();
        }

        // POST: Soporte/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Titulo,DetalleDelProblema,QuienReporto,EstadoActual,ClienteID")] Support soporte)
        {
            if (ModelState.IsValid)
            {
                _context.Add(soporte);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClienteID"] = new SelectList(_context.cliente, "ID", "ID", soporte.ClienteID);
            return View(soporte);
        }

        // GET: Soporte/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var soporte = await _context.soporte.SingleOrDefaultAsync(m => m.ID == id);
            if (soporte == null)
            {
                return NotFound();
            }
            ViewData["ClienteID"] = new SelectList(_context.cliente, "ID", "ID", soporte.ClienteID);
            return View(soporte);
        }

        // POST: Soporte/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int ID, [Bind("ID,Titulo,DetalleDelProblema,QuienReporto,EstadoActual,ClienteID")] Support soporte)
        {
            if (ID != soporte.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(soporte);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!soporteExists(soporte.ID))
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
            ViewData["ClienteID"] = new SelectList(_context.cliente, "ID", "ID", soporte.ClienteID);
            return View(soporte);
        }

        // GET: Soporte/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var soporte = await _context.soporte
                .Include(s => s.Cliente)
                .SingleOrDefaultAsync(m => m.ID == id);
            if (soporte == null)
            {
                return NotFound();
            }

            return View(soporte);
        }

        // POST: Soporte/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var soporte = await _context.soporte.SingleOrDefaultAsync(m => m.ID == id);
            _context.soporte.Remove(soporte);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool soporteExists(int id)
        {
            return _context.soporte.Any(e => e.ID == id);
        }
    }
}
