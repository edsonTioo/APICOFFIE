

using ApiMSCOFFIE.Models;
using ApiMSCOFFIE.Services;
using BCrypt.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ApiMSCOFFIE.Controllers
{
    [Route("api/controller")]
    [ApiController]
    public class AuthController:ControllerBase
    {
        private readonly EmpleadosService _servicio;
        private readonly ClienteService _cliente;
        private readonly IConfiguration _configuration;
        public AuthController(EmpleadosService empleadosService, ClienteService clienteService, IConfiguration configuration)
        {
            _servicio = empleadosService;
            _configuration = configuration;
            _cliente = clienteService;
        }
        [HttpPost("login")]
        public async Task<IActionResult> login([FromBody] User user)
        {
            try
            {
                // Intentar encontrar el usuario como empleado
                var existingEmpleado = await _servicio.ObtenerEmpleadoAsync(user.Correo);
                if (existingEmpleado != null && existingEmpleado.Password == user.Password)
                {
                    var token = GenerateJwtToken(existingEmpleado.Correo);
                    return Ok(new { Token = token, Id = existingEmpleado.Id, Rol = existingEmpleado.Rol });
                }

                // Si no se encuentra como empleado, intenta buscarlo como cliente
                var existingCliente = await _cliente.ObtenerClienteAsync(user.Correo);
                if (existingCliente != null && existingCliente.Password == user.Password)
                {
                    var token = GenerateJwtToken(existingCliente.Correo);
                    return Ok(new { Token = token, Id = existingCliente.Id, Rol = "" }); // o omitir Rol si prefieres
                }
                return Unauthorized();
            }
            catch (Exception ex)
            {
                // Capturar cualquier error y devolver una respuesta de error interno
                return StatusCode(500, $"Error al procesar la solicitud: {ex.Message}");
            }
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] Empleados empleados)
        {
            var hasspassword = BCrypt.Net.BCrypt.HashPassword(empleados.Password);
            empleados.Password = hasspassword;
            var existingUser = await _servicio.ObtenerEmpleadoAsync(empleados.Correo);
            if(existingUser !=null)
            {
                return Conflict("El empleados ya existe");
            }
            await _servicio.CrearEmpleadoAsync(empleados);
            return Ok("Empleado registrado exitosamente");

        }
        private string GenerateJwtToken(string correo) 
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]));
            var credentiasl = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub,correo),
                new Claim(JwtRegisteredClaimNames.Jti,
                Guid.NewGuid().ToString())
            };

            Claim[] claims1 = claims;
            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims1,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentiasl
                );

            return new JwtSecurityTokenHandler().WriteToken(token);

        }

        //[HttpPost("Rol")]
        //public async Task<IActionResult> Rol([FromBody] Empleados user)
        //{
        //    var existingUser = await _servicio.ObtenerEmpleadoAsync(user.Correo);

        //    if (existingUser == null || existingUser.Correo != user.Correo || existingUser.Password != user.Password)
        //        return Unauthorized();
        //    return Ok(new { Rol = existingUser.Rol });
        //}
        //[HttpPost("usuarioId")]
        //public async Task<IActionResult> Id([FromBody] Empleados user)
        //{
        //    var existingUser = await _servicio.ObtenerEmpleadoAsync(user.Correo);

        //    if (existingUser == null || existingUser.Correo != user.Correo || existingUser.Password != user.Password)
        //        return Unauthorized();
        //    return Ok(new { usuarioId = existingUser.Id });
        //}


    }
}
