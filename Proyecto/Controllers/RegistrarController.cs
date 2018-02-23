using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Proyecto.Models;

namespace Proyecto.Controllers
{
    public class RegistrarController : Controller
    {
        private readonly RegistrarContext _context;

        public RegistrarController(RegistrarContext context)
        {
            _context = context;
        }

        // GET: Registrar
        public async Task<IActionResult> Index()
        {
            return View(await _context.Registrar.ToListAsync());
        }

        // GET: Registrar/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var registrar = await _context.Registrar
                .SingleOrDefaultAsync(m => m.ID == id);
            if (registrar == null)
            {
                return NotFound();
            }

            return View(registrar);
        }

        // GET: Registrar/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Registrar/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Nombre,Apellidos,NickName,Password")] Registrar registrar)
        {
            if (ModelState.IsValid)
            {
                _context.Add(registrar);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(registrar);
        }

        // GET: Registrar/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var registrar = await _context.Registrar.SingleOrDefaultAsync(m => m.ID == id);
            if (registrar == null)
            {
                return NotFound();
            }
            return View(registrar);
        }

        // POST: Registrar/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Nombre,Apellidos,NickName,Password")] Registrar registrar)
        {
            if (id != registrar.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(registrar);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RegistrarExists(registrar.ID))
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
            return View(registrar);
        }

        // GET: Registrar/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var registrar = await _context.Registrar
                .SingleOrDefaultAsync(m => m.ID == id);
            if (registrar == null)
            {
                return NotFound();
            }

            return View(registrar);
        }

        // POST: Registrar/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var registrar = await _context.Registrar.SingleOrDefaultAsync(m => m.ID == id);
            _context.Registrar.Remove(registrar);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RegistrarExists(int id)
        {
            return _context.Registrar.Any(e => e.ID == id);
        }
    }
}
