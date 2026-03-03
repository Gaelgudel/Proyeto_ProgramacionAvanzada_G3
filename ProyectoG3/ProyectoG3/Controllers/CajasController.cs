using Microsoft.AspNetCore.Mvc;
using ProyectoG3.Models;
using ProyectoG3.Services;
using ProyectoG3.Services.ProyectoSinpe.Services;

namespace ProyectoG3.Controllers
{
    public class CajasController : Controller
    {
        private readonly ICajaService _cajaService;
        private readonly ISinpeService _sinpeService;

        public CajasController(ICajaService cajaService, ISinpeService sinpeService)
        {
            _cajaService = cajaService;
            _sinpeService = sinpeService;
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
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registrar(Caja modelo)
        {
            try
            {
                await _cajaService.RegistrarCaja(modelo);
                return RedirectToAction("VerCajas", new { idComercio = modelo.IdComercio });
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
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
            try
            {
                await _cajaService.EditarCaja(modelo);
                return RedirectToAction("VerCajas", new { idComercio = modelo.IdComercio });
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
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