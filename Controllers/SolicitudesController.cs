using Microsoft.AspNetCore.Mvc;
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
                return RedirectToAction(nameof(Crear)); // O a una vista de éxito / listado
            }
            return View(solicitud);
        }
    }
}