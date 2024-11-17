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
    public class ReservaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReservaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Reserva
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Reservas.Include(r => r.Salon).Include(r => r.Usuario);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Reserva/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reserva = await _context.Reservas
                .Include(r => r.Salon)
                .Include(r => r.Usuario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reserva == null)
            {
                return NotFound();
            }

            return View(reserva);
        }

        // GET: Reserva/Create
        public IActionResult Create()
        {
            // Cargar la lista de usuarios y salones
            ViewBag.Usuarios = _context.Usuarios.ToList();
            ViewBag.Salones = _context.Salones.ToList();

            return View();
        }


        // POST: Reserva/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FechaInicio, FechaFin, Estado, UsuarioId, SalonId")] Reserva reserva)
        {
            if (!ModelState.IsValid)
            {
                _context.Add(reserva);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }


            // Re-cargar los valores de los select en caso de que la validación falle
            ViewBag.Usuarios = _context.Usuarios.ToList();
            ViewBag.Salones = _context.Salones.ToList();

            return View(reserva);
        }


        // GET: Reserva/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            Console.WriteLine("GET Edit called with id: " + id);

            if (id == null)
            {
                Console.WriteLine("ID is null, returning NotFound");
                return NotFound();
            }

            var reserva = await _context.Reservas.FindAsync(id);
            if (reserva == null)
            {
                Console.WriteLine("Reserva not found, returning NotFound");
                return NotFound();
            }

            Console.WriteLine("Reserva fetched: Id = " + reserva.Id + ", FechaInicio = " + reserva.FechaInicio + ", FechaFin = " + reserva.FechaFin + ", Estado = " + reserva.Estado + ", UsuarioId = " + reserva.UsuarioId + ", SalonId = " + reserva.SalonId);

            ViewBag.Salones = _context.Salones.ToList();
            Console.WriteLine("Salones loaded into ViewBag: " + ViewBag.Salones.Count);

            ViewBag.Usuarios = _context.Usuarios.ToList();
            Console.WriteLine("Usuarios loaded into ViewBag: " + ViewBag.Usuarios.Count);

            return View(reserva);
        }

        // POST: Reserva/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FechaInicio,FechaFin,Estado,UsuarioId,SalonId")] Reserva reserva)
        {
            Console.WriteLine("POST Edit called with id: " + id);
            Console.WriteLine("Reserva received: Id = " + reserva.Id + ", FechaInicio = " + reserva.FechaInicio + ", FechaFin = " + reserva.FechaFin + ", Estado = " + reserva.Estado + ", UsuarioId = " + reserva.UsuarioId + ", SalonId = " + reserva.SalonId);

            if (id != reserva.Id)
            {
                Console.WriteLine("id does not match reserva.Id, returning NotFound");
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                try
                {
                    Console.WriteLine("ModelState is valid, updating reserva");
                    _context.Update(reserva);
                    await _context.SaveChangesAsync();
                    Console.WriteLine("Reserva updated successfully");
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    Console.WriteLine("DbUpdateConcurrencyException caught: " + ex.Message);
                    if (!ReservaExists(reserva.Id))
                    {
                        Console.WriteLine("Reserva does not exist, returning NotFound");
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                Console.WriteLine("Redirecting to Index");
                return RedirectToAction(nameof(Index));
            }

            Console.WriteLine("ModelState is invalid, reloading ViewBag data");
            ViewBag.Salones = _context.Salones.ToList();
            Console.WriteLine("Salones reloaded into ViewBag: " + ViewBag.Salones.Count);

            ViewBag.Usuarios = _context.Usuarios.ToList();
            Console.WriteLine("Usuarios reloaded into ViewBag: " + ViewBag.Usuarios.Count);

            return View(reserva);
        }


        // GET: Reserva/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reserva = await _context.Reservas
                .Include(r => r.Salon)
                .Include(r => r.Usuario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reserva == null)
            {
                return NotFound();
            }

            return View(reserva);
        }

        // POST: Reserva/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reserva = await _context.Reservas.FindAsync(id);
            if (reserva != null)
            {
                _context.Reservas.Remove(reserva);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReservaExists(int id)
        {
            return _context.Reservas.Any(e => e.Id == id);
        }
    }
}
