using ApiMSCOFFIE.Models;
using ApiMSCOFFIE.Services;
using Microsoft.AspNetCore.Mvc;

namespace ApiMSCOFFIE.Controllers
{
    public class ControllerCocina:ControllerBase
    {
        private readonly CocinaService _serviceCocina;

        public ControllerCocina(CocinaService serviceCocina) => _serviceCocina = serviceCocina;

        [HttpGet]
        public async Task<List<Cocina>> Obtener() => await _serviceCocina.ObtenerAsync();

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Cocina>> Obtener(string id)
        {
            var cocina = await _serviceCocina.ObtenerAsync(id);
            if (cocina is null) return NotFound();
            return cocina;
        }

        [HttpPost]
        public async Task<IActionResult> Crear(Cocina nuevoPedido)
        {
            await _serviceCocina.CrearAsync(nuevoPedido);
            return CreatedAtAction(nameof(Obtener), new { id = nuevoPedido.Id }, nuevoPedido);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<ActionResult> Actualizar(string id, Cocina cocinaActualizada)
        {
            var cocina = await _serviceCocina.ObtenerAsync(id);
            if (cocina is null) return NotFound();
            cocinaActualizada.Id = cocina.Id;
            await _serviceCocina.ActualizarAsync(id, cocinaActualizada);
            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Eliminar(string id)
        {
            var cocina = await _serviceCocina.ObtenerAsync(id);
            if (cocina is null) return NotFound();
            await _serviceCocina.EliminarAsync(id);
            return NoContent();
        }
    }
}
