using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Salones.Data;
using Salones.Models;

namespace Salones.Controllers
{
    public class EquipoTecnologicoController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EquipoTecnologicoController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: EquipoTecnologico
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.EquiposTecnologicos.Include(e => e.Salon);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: EquipoTecnologico/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var equipoTecnologico = await _context.EquiposTecnologicos
                .Include(e => e.Salon)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (equipoTecnologico == null)
            {
                return NotFound();
            }

            return View(equipoTecnologico);
        }

        // GET: EquipoTecnologico/Create
        public IActionResult Create()
        {
            ViewBag.Salones = _context.Salones.ToList();
            return View();
        }


        // POST: EquipoTecnologico/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nombre, SalonId")] EquipoTecnologico equipoTecnologico)
        {
            if (!ModelState.IsValid)
            {
                _context.Add(equipoTecnologico);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

            }
            ViewBag.Salones = _context.Salones.ToList();
            return View(equipoTecnologico);
        }

        // GET: EquipoTecnologico/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            Console.WriteLine("GET Edit called with id: " + id);

            if (id == null)
            {
                Console.WriteLine("id is null, returning NotFound");
                return NotFound();
            }

            var equipoTecnologico = await _context.EquiposTecnologicos.FindAsync(id);
            Console.WriteLine("EquipoTecnologico fetched: " + (equipoTecnologico != null ? equipoTecnologico.Nombre : "null"));

            if (equipoTecnologico == null)
            {
                Console.WriteLine("EquipoTecnologico not found, returning NotFound");
                return NotFound();
            }

            ViewBag.Salones = _context.Salones.ToList();
            Console.WriteLine("Salones loaded into ViewBag: " + ViewBag.Salones.Count);

            return View(equipoTecnologico);
        }

        // POST: EquipoTecnologico/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id, Nombre, SalonId")] EquipoTecnologico equipoTecnologico)
        {
            Console.WriteLine("POST Edit called with id: " + id);
            Console.WriteLine("EquipoTecnologico received: Nombre = " + equipoTecnologico.Nombre + ", SalonId = " + equipoTecnologico.SalonId);

            if (id != equipoTecnologico.Id)
            {
                Console.WriteLine("id does not match equipoTecnologico.Id, returning NotFound");
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                Console.WriteLine("ModelState is valid, attempting to update EquipoTecnologico");

                try
                {
                    _context.Update(equipoTecnologico);
                    await _context.SaveChangesAsync();
                    Console.WriteLine("EquipoTecnologico updated successfully");
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    Console.WriteLine("DbUpdateConcurrencyException caught: " + ex.Message);

                    if (!EquipoTecnologicoExists(equipoTecnologico.Id))
                    {
                        Console.WriteLine("EquipoTecnologico does not exist, returning NotFound");
                        return NotFound();
                    }
                    else
                    {
                        Console.WriteLine("Throwing exception");
                        throw;
                    }
                }
                Console.WriteLine("Redirecting to Index");
                return RedirectToAction(nameof(Index));
            }

            Console.WriteLine("ModelState is invalid, reloading Salones and returning View");
            ViewBag.Salones = _context.Salones.ToList();
            return View(equipoTecnologico);
        }



        // GET: EquipoTecnologico/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var equipoTecnologico = await _context.EquiposTecnologicos
                .Include(e => e.Salon)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (equipoTecnologico == null)
            {
                return NotFound();
            }

            return View(equipoTecnologico);
        }

        // POST: EquipoTecnologico/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var equipoTecnologico = await _context.EquiposTecnologicos.FindAsync(id);
            if (equipoTecnologico != null)
            {
                _context.EquiposTecnologicos.Remove(equipoTecnologico);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EquipoTecnologicoExists(int id)
        {
            return _context.EquiposTecnologicos.Any(e => e.Id == id);
        }
    }
}
