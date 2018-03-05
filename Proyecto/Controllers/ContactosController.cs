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
    public class ContactosController : Controller
    {
        private readonly ModeloContext _context;

        public ContactosController(ModeloContext context)
        {
            _context = context;
        }

        // GET: Contactos
        public async Task<IActionResult> Index()
        {
            var modeloContext = _context.contacto.Include(c => c.Cliente);
            return View(await modeloContext.ToListAsync());
        }

        // GET: Contactos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contactos = await _context.contacto
                .Include(c => c.Cliente)
                .SingleOrDefaultAsync(m => m.ID == id);
            if (contactos == null)
            {
                return NotFound();
            }

            return View(contactos);
        }

        // GET: Contactos/Create
        public IActionResult Create()
        {
            ViewData["ClienteID"] = new SelectList(_context.cliente, "ID", "Nombre");
            return View();
        }

        // POST: Contactos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Nombre,Apellidos,Correo,Telefono,Puesto,ClienteID")] Contactos contactos)
        {
            if (ModelState.IsValid)
            {
                _context.Add(contactos);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClienteID"] = new SelectList(_context.cliente, "ID", "ID", contactos.ClienteID);
            return View(contactos);
        }

        // GET: Contactos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contactos = await _context.contacto.SingleOrDefaultAsync(m => m.ID == id);
            if (contactos == null)
            {
                return NotFound();
            }
            ViewData["ClienteID"] = new SelectList(_context.cliente, "ID", "ID", contactos.ClienteID);
            return View(contactos);
        }

        // POST: Contactos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Nombre,Apellidos,Correo,Telefono,Puesto,ClienteID")] Contactos contactos)
        {
            if (id != contactos.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(contactos);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContactosExists(contactos.ID))
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
            ViewData["ClienteID"] = new SelectList(_context.cliente, "ID", "ID", contactos.ClienteID);
            return View(contactos);
        }

        // GET: Contactos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contactos = await _context.contacto
                .Include(c => c.Cliente)
                .SingleOrDefaultAsync(m => m.ID == id);
            if (contactos == null)
            {
                return NotFound();
            }

            return View(contactos);
        }

        // POST: Contactos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var contactos = await _context.contacto.SingleOrDefaultAsync(m => m.ID == id);
            _context.contacto.Remove(contactos);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContactosExists(int id)
        {
            return _context.contacto.Any(e => e.ID == id);
        }
    }
}
