using ApiMSCOFFIE.Models;
using ApiMSCOFFIE.Services;
using Microsoft.AspNetCore.Mvc;

namespace ApiMSCOFFIE.Controllers
{
    [Route("api/controller")]
    [ApiController]
    public class VerificarPasswordController:ControllerBase
    {
        private readonly EmpleadosService _empleado;
        private readonly ClienteService _cliente;
        private readonly IConfiguration _configuration;
       public VerificarPasswordController(EmpleadosService empleadosService,ClienteService clienteService,IConfiguration configuration)
       {
            _empleado = empleadosService;
            _cliente = clienteService;
            _configuration = configuration;
        }
        [HttpGet("verificar-correo")]
        public async Task<IActionResult> VerificarCorreo([FromQuery] string correo)
        {
            try
            {
                // Intenta obtener un empleado con el correo proporcionado
                var existeEmpleado = await _empleado.ObtenerEmpleadoAsync(correo);

                // Si se encuentra un empleado, retorna el mensaje correspondiente
                if (existeEmpleado != null)
                {
                    return Ok(new { Existe = true, Mensaje = "El correo está registrado como empleado." });
                }

                // Intenta obtener un cliente con el correo proporcionado
                var existeCliente = await _cliente.ObtenerClienteAsync(correo);

                // Si se encuentra un cliente, retorna el mensaje correspondiente
                if (existeCliente != null)
                {
                    return Ok(new { Existe = true, Mensaje = "El correo está registrado como cliente." });
                }

                // Si no se encuentra en ninguna de las dos entidades, retorna el mensaje de no registrado
                return Ok(new { Existe = false, Mensaje = "El correo no está registrado en el sistema." });
            }
            catch (Exception ex)
            {
                // Manejo de errores en caso de excepción
                return StatusCode(500, new { Error = "Ocurrió un error al verificar el correo", Detalles = ex.Message });
            }
        }
        //[HttpPost("cambiar-password")]
        //public async Task<IActionResult> CambiarPassword([FromQuery] string correo, [FromQuery] string nuevaPassword)
        //{
        //    try
        //    {
        //        // Verifica si el correo pertenece a un empleado
        //        var empleado = await _empleado.ObtenerEmpleadoAsync(correo);
        //        if (empleado != null)
        //        {
        //            /// Actualiza la contraseña del cliente usando el nuevo método
        //            var actualizado = await _empleado.ActualizarPasswordPorCorreoAsync(correo, nuevaPassword);

        //            if (actualizado)
        //            {
        //                return Ok(new { Mensaje = "La contraseña del empleado se ha actualizado correctamente." });
        //            }
        //            else
        //            {
        //                return StatusCode(500, new { Error = "No se pudo actualizar la contraseña del cliente." });
        //            }
        //        }

        //        // Verifica si el correo pertenece a un cliente
        //        var cliente = await _cliente.ObtenerClienteAsync(correo);
        //        if (cliente != null)
        //        {
        //            // Actualiza la contraseña del cliente usando el nuevo método
        //            var actualizado = await _cliente.ActualizarPasswordPorCorreoAsync(correo, nuevaPassword);

        //            if (actualizado)
        //            {
        //                return Ok(new { Mensaje = "La contraseña del cliente se ha actualizado correctamente." });
        //            }
        //            else
        //            {
        //                return StatusCode(500, new { Error = "No se pudo actualizar la contraseña del cliente." });
        //            }
        //        }

        //        // Si el correo no pertenece a un empleado ni a un cliente
        //        return NotFound(new { Error = "El correo no está registrado en el sistema." });
        //    }
        //    catch (Exception ex)
        //    {
        //        // Manejo de errores en caso de excepción
        //        return StatusCode(500, new { Error = "Ocurrió un error al cambiar la contraseña", Detalles = ex.Message });
        //    }
        //}



        [HttpPut("cambiar-password")]
        public async Task<IActionResult> CambiarPassword([FromBody] CambiarPasswordRequest request)
        {
            try
            {
                // Verifica si el correo pertenece a un empleado
                var empleado = await _empleado.ObtenerEmpleadoAsync(request.Correo);
                if (empleado != null)
                {
                    var actualizado = await _empleado.ActualizarPasswordPorCorreoAsync(request.Correo, request.NuevaPassword);
                    if (actualizado)
                    {
                        return Ok(new { Mensaje = "La contraseña del empleado se ha actualizado correctamente." });
                    }
                    else
                    {
                        return StatusCode(500, new { Error = "No se pudo actualizar la contraseña del empleado." });
                    }
                }

                // Verifica si el correo pertenece a un cliente
                var cliente = await _cliente.ObtenerClienteAsync(request.Correo);
                if (cliente != null)
                {
                    var actualizado = await _cliente.ActualizarPasswordPorCorreoAsync(request.Correo, request.NuevaPassword);
                    if (actualizado)
                    {
                        return Ok(new { Mensaje = "La contraseña del cliente se ha actualizado correctamente." });
                    }
                    else
                    {
                        return StatusCode(500, new { Error = "No se pudo actualizar la contraseña del cliente." });
                    }
                }

                // Si el correo no pertenece a un empleado ni a un cliente
                return NotFound(new { Error = "El correo no está registrado en el sistema." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = "Ocurrió un error al cambiar la contraseña", Detalles = ex.Message });
            }
        }


    }

}
