using ApiMSCOFFIE.Models;
using ApiMSCOFFIE.Services;
using Microsoft.AspNetCore.Mvc;

namespace ApiMSCOFFIE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ControllerCliente:ControllerBase
    {

        private readonly ClienteService _serviceClientes;
        public ControllerCliente(ClienteService serviceClientes) => _serviceClientes = serviceClientes;

        // Obtener todos los clientes
        [HttpGet]
        public async Task<List<Cliente>> Obtener() => await _serviceClientes.ObtenerAsync();

        // Obtener un cliente por ID
        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Cliente>> Obtener(string id)
        {
            var cliente = await _serviceClientes.ObtenerAsync(id);
            if (cliente is null)
            {
                return NotFound();
            }
            return cliente;
        }

        // Crear nuevo cliente
        [HttpPost]
        public async Task<IActionResult> Crear(Cliente nuevoCliente)
        {
            await _serviceClientes.CrearAsync(nuevoCliente);
            return CreatedAtAction(nameof(Obtener), new { id = nuevoCliente.Id }, nuevoCliente);
        }

        // Actualizar cliente existente
        [HttpPut("{id:length(24)}")]
        public async Task<ActionResult> Actualizar(string id, Cliente clienteActualizado)
        {
            var cliente = await _serviceClientes.ObtenerAsync(id);
            if (cliente is null) return NotFound();
            clienteActualizado.Id = cliente.Id;
            await _serviceClientes.ActualizarAsync(id, clienteActualizado);
            return NoContent();
        }

        // Eliminar cliente
        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Eliminar(string id)
        {
            var cliente = await _serviceClientes.ObtenerAsync(id);
            if (cliente is null) return NotFound();
            await _serviceClientes.EliminarAsync(id);
            return NoContent();
        }

        // Buscar clientes por nombre
        [HttpGet("buscar/{nombre}")]
        public async Task<ActionResult<List<Cliente>>> BuscarNombre(string nombre)
        {
            var clientes = await _serviceClientes.BuscarPorNombre(nombre);
            if (clientes == null || clientes.Count == 0)
            {
                return NotFound($"No se encontraron clientes con el nombre '{nombre}'.");
            }
            return clientes;
        }
    }
}
