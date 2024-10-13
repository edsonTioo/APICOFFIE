
using ApiMSCOFFIE.Models;
using ApiMSCOFFIE.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiMSCOFFIE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ControllerEmpleados : ControllerBase
    {
        private readonly EmpleadosService _serviceEmpleados;
        public ControllerEmpleados(EmpleadosService serviceEmpleados) => _serviceEmpleados = serviceEmpleados;

        [HttpGet]
        public async Task<List<Empleados>> Obtener() => await _serviceEmpleados.ObtenerAsync();


        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Empleados>> Obtener(string id)
        {
            var empleados = await _serviceEmpleados.ObtenerAsync(id);
            if (empleados is null)
            {
                return NotFound();
            }
            return empleados;
        }
  
        [HttpPost]
        public async Task<IActionResult> Crear(Empleados nuevoempleados)
        {
            await _serviceEmpleados.CrearAsync(nuevoempleados);
            return CreatedAtAction(nameof(Obtener), new { id = nuevoempleados.Id }, nuevoempleados);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<ActionResult> Actualizar(string id, Empleados empleadosAtualizado)
        {
            var empleado = await _serviceEmpleados.ObtenerAsync(id);
            if (empleado is null) return NotFound();
            empleadosAtualizado.Id = empleado.Id;
            await _serviceEmpleados.ActualizarAsync(id, empleadosAtualizado);
            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult>Eliminar(string id)
        {
            var empleado = await _serviceEmpleados.ObtenerAsync(id);
            if(empleado is null) return NotFound();
            await _serviceEmpleados.EliminarAsync(id);
            return NoContent();
        }

        [HttpGet("buscar/{nombre}")]
        public async Task<ActionResult<List<Empleados>>> BuscarNombre(string nombre)
        {
            var empleado = await _serviceEmpleados.BuscarPorNombre(nombre);
            if (empleado == null || empleado.Count == 0)
            {
                return NotFound($"No se encontraron estudiantes con el nombre '{nombre}'.");
            }
            return empleado;
        }
    }
}
