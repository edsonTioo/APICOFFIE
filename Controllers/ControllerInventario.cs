using ApiMSCOFFIE.Models;
using ApiMSCOFFIE.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace ApiMSCOFFIE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ControllerInventario : ControllerBase
    {
        private readonly InventarioService _inventarioService;
        public ControllerInventario(InventarioService servicioinv) => _inventarioService = servicioinv;

        [HttpGet]
        public async Task<List<Inventario>> Obtener() => await _inventarioService.ObtenerAsync();

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Inventario>> Obtener(string id)
        {
            var inventario = await _inventarioService.ObtenerAsync(id);
            if (inventario is null)
            {
                return NotFound();
            }
            return inventario;
        }

        [HttpPost]
        public async Task<IActionResult> Crear(Inventario nuevoinventario)
        {
            await _inventarioService.CrearAsync(nuevoinventario);
            return CreatedAtAction(nameof(Obtener), new { id = nuevoinventario.Id }, nuevoinventario);
        }


        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Eliminar(string id)
        {
            var producto = await _inventarioService.ObtenerAsync(id);
            if (producto is null) return NotFound();
            await _inventarioService.EliminarAsync(id);
            return NoContent();
        }

        [HttpPut("{id:length(24)}")]
        public async Task<ActionResult> Actualizar(string id, Inventario inventarioactualizado)
        {
            var producto = await _inventarioService.ObtenerAsync(id);
            if (producto is null) return NotFound();
            inventarioactualizado.Id = producto.Id;
            await _inventarioService.ActualizarAsync(id, inventarioactualizado);
            return NoContent();
        }
        [HttpGet("buscar/{nombre}")]
        public async Task<ActionResult<List<Inventario>>> Buscarnombre(string nombre)
        {
            var inventarios = await _inventarioService.buscarpornombre(nombre);
            if (inventarios == null || inventarios.Count == 0)
            {
                return NotFound($"No se encontraron estudiantes con el nombre '{nombre}'.");
            }
            return inventarios;
        }
    }
}
