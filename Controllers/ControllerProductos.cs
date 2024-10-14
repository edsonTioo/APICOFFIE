using ApiMSCOFFIE.Models;
using ApiMSCOFFIE.Services;
using Microsoft.AspNetCore.Mvc;

namespace ApiMSCOFFIE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ControllerProductos:ControllerBase
    {
        private readonly ProductosService _serviceProducto;
        public ControllerProductos(ProductosService servicioProducto) => _serviceProducto = servicioProducto;

        [HttpGet]
        public async Task<List<Productos>> Obtener() => await _serviceProducto.ObtenerAsync();

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Productos>> Obtener(string id)
        {
            var producto = await _serviceProducto.ObtenerAsync(id);
            if (producto is null)
            {
                return NotFound();
            }
            return producto;
        }
        [HttpPost]
        public async Task<IActionResult> Crear(Productos nuevoproducto)
        {
            await _serviceProducto.CrearAsync(nuevoproducto);
            return CreatedAtAction(nameof(Obtener), new { id = nuevoproducto.Id }, nuevoproducto);
        }


        [HttpPut("{id:length(24)}")]
        public async Task<ActionResult> Actualizar(string id, Productos productoActualizado)
        {
            var producto = await _serviceProducto.ObtenerAsync(id);
            if (producto is null) return NotFound();
            productoActualizado.Id = producto.Id;
            await _serviceProducto.ActualizarAsync(id,  productoActualizado);
            return NoContent();
        }
        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Eliminar(string id)
        {
            var producto = await _serviceProducto.ObtenerAsync(id);
            if (producto is null) return NotFound();
            await _serviceProducto.EliminarAsync(id);
            return NoContent();
        }
        [HttpGet("buscar/{nombre}")]
        public async Task<ActionResult<List<Productos>>> BuscarNombre(string nombre)
        {
            var producto = await _serviceProducto.BuscarPorNombre(nombre);
            if (producto == null || producto.Count == 0)
            {
                return NotFound($"No se encontraron estudiantes con el nombre '{nombre}'.");
            }
            return producto;
        }
    }
}
