using Microsoft.AspNetCore.Mvc;
using ProyectoG3.Models;
using ProyectoG3.Application.Interfaces;
using ProyectoG3.Infrastructure.Services;

namespace ProyectoG3.Controllers
{
    public class CajasController : Controller
    {
        private readonly ICajaService _cajaService;
        private readonly ISinpeService _sinpeService;
        private readonly IBitacoraService _bitacoraService;
        public CajasController(ICajaService cajaService, ISinpeService sinpeService, IBitacoraService bitacoraService)
        {
            _cajaService = cajaService;
            _sinpeService = sinpeService;
            _bitacoraService = bitacoraService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return RedirectToAction("VerCajas", new { idComercio = 1 });
        }

        [HttpGet]
        public async Task<IActionResult> VerCajas(int idComercio)
        {
            ViewBag.IdComercio = idComercio;
            var cajas = await _cajaService.ListarPorComercio(idComercio);
            return View(cajas);
        }

        [HttpGet]
        [ValidateAntiForgeryToken]
        public IActionResult Registrar(int idComercio)
        {
            return View(new Caja
            {
                IdComercio = idComercio,
                Nombre = string.Empty,
                Descripcion = string.Empty,
                TelefonoSINPE = string.Empty
            });
        }

        [HttpPost]

        public async Task<IActionResult> Registrar(Caja modelo)
        {
            try
            {
                var cajaCreada = await _cajaService.CreateAsync(modelo);
                await _bitacoraService.RegistrarEventoAsync("Cajas", "Registrar",
                    $"Se logró registrar la caja {modelo.Nombre}", anterior: cajaCreada);
                return RedirectToAction("VerCajas", new { idComercio = modelo.IdComercio });
            }
            catch (Exception ex)
            {
                await _bitacoraService.RegistrarErrorAsync("Cajas", ex.Message, ex.StackTrace ?? "");
                return View(modelo);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Editar(int id)
        {
            var caja = await _cajaService.ObtenerPorId(id);
            if (caja == null) return NotFound();

            return View(caja);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(Caja modelo)
        {
            var cajaOriginal = await _cajaService.ObtenerPorId(modelo.IdCaja);

            try
            {
                await _cajaService.UpdateAsync(modelo);

                await _bitacoraService.RegistrarEventoAsync("Cajas", "Editar",
                    $"La caja se editó con el ID {modelo.IdCaja}",
                    anterior: cajaOriginal,
                    posterior: modelo);

                return RedirectToAction("VerCajas", new { idComercio = modelo.IdComercio });
            }
            catch (Exception ex)
            {
                await _bitacoraService.RegistrarErrorAsync("Cajas", ex.Message, ex.StackTrace ?? "");
                return View(modelo);
            }
        }

        [HttpGet]
        public async Task<IActionResult> VerSinpe(int idCaja)
        {
            var caja = await _cajaService.ObtenerPorId(idCaja);
            if (caja == null) return NotFound();

            var sinpes = await _sinpeService.ObtenerSinpePorTelefono(caja.TelefonoSINPE);

            ViewBag.NombreCaja = caja.Nombre;

            return View(sinpes);
        }
    }
}