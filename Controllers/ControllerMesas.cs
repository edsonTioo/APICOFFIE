using ApiMSCOFFIE.Models;
using ApiMSCOFFIE.Services;
using Microsoft.AspNetCore.Mvc;

namespace ApiMSCOFFIE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ControllerMesas:ControllerBase
    {
        private readonly MesasServicio _serviceMesas;

        public ControllerMesas(MesasServicio serviceMesas)
        {
            _serviceMesas = serviceMesas;
        }

        // Obtener todas las mesas
        [HttpGet]
        public async Task<List<Mesas>> Obtener()
        {
            return await _serviceMesas.ObtenerAsync();
        }

        // Obtener una mesa por ID
        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Mesas>> Obtener(string id)
        {
            var mesa = await _serviceMesas.ObtenerAsync(id);
            if (mesa is null)
            {
                return NotFound();
            }
            return mesa;
        }

        // Crear una nueva mesa
        [HttpPost]
        public async Task<IActionResult> Crear(Mesas nuevaMesa)
        {
            await _serviceMesas.CrearAsync(nuevaMesa);
            return CreatedAtAction(nameof(Obtener), new { id = nuevaMesa.Id }, nuevaMesa);
        }

        // Actualizar una mesa
        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Actualizar(string id, Mesas mesaActualizada)
        {
            var mesa = await _serviceMesas.ObtenerAsync(id);
            if (mesa is null)
            {
                return NotFound();
            }
            mesaActualizada.Id = mesa.Id;
            await _serviceMesas.ActualizarAsync(id, mesaActualizada);
            return NoContent();
        }

        // Eliminar una mesa
        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Eliminar(string id)
        {
            var mesa = await _serviceMesas.ObtenerAsync(id);
            if (mesa is null)
            {
                return NotFound();
            }
            await _serviceMesas.EliminarAsync(id);
            return NoContent();
        }

        // Agregar un pedido a una mesa
        [HttpPost("{idMesa:length(24)}/pedidos")]
        public async Task<IActionResult> AgregarPedido(string idMesa, Pedido nuevoPedido)
        {
            var mesa = await _serviceMesas.ObtenerAsync(idMesa);
            if (mesa is null)
            {
                return NotFound();
            }

            await _serviceMesas.AgregarPedidoAsync(idMesa, nuevoPedido);
            return Ok(nuevoPedido);
        }

        // Actualizar un pedido en una mesa
        [HttpPut("{idMesa:length(24)}/pedidos/{indicePedido}")]
        public async Task<IActionResult> ActualizarPedido(string idMesa, int indicePedido, Pedido pedidoActualizado)
        {
            var mesa = await _serviceMesas.ObtenerAsync(idMesa);
            if (mesa is null)
            {
                return NotFound();
            }

            await _serviceMesas.ActualizarPedidoAsync(idMesa, indicePedido, pedidoActualizado);
            return NoContent();
        }

        // Eliminar un pedido de una mesa
        [HttpDelete("{idMesa:length(24)}/pedidos/{indicePedido}")]
        public async Task<IActionResult> EliminarPedido(string idMesa, int indicePedido)
        {
            var mesa = await _serviceMesas.ObtenerAsync(idMesa);
            if (mesa is null)
            {
                return NotFound();
            }

            await _serviceMesas.EliminarPedidoAsync(idMesa, indicePedido);
            return NoContent();
        }
    }
}