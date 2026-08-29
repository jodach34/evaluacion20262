using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TecnoGasHogar.Data;
using TecnoGasHogar.Models;

namespace TecnoGasHogar.Controllers
{
    public class SolicitudesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SolicitudesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /Solicitudes/Index
        public async Task<IActionResult> Index()
        {
            var solicitudes = await _context.SolicitudesServicio
                .OrderByDescending(s => s.FechaRegistro)
                .ToListAsync();

            return View(solicitudes);
        }

        // GET: /Solicitudes/Crear
        public IActionResult Crear()
        {
            return View();
        }

        // POST: /Solicitudes/Crear
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear(SolicitudServicio solicitud)
        {
            if (ModelState.IsValid)
            {
                _context.SolicitudesServicio.Add(solicitud);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index)); // Redirige al listado después de crear
            }
            return View(solicitud);
        }
    }
}