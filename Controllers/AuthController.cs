

using ApiMSCOFFIE.Models;
using ApiMSCOFFIE.Services;
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
        private readonly IConfiguration _configuration;

        public AuthController(EmpleadosService empleadosService, IConfiguration configuration)
        {
            _servicio = empleadosService;
            _configuration = configuration;
        }
        [HttpPost("login")]
        public async Task<IActionResult> login([FromBody] Empleados empleados)
        {
            var existingUser = await _servicio.ObtenerUsuariosAsync(empleados.Correo);
            if (existingUser == null || existingUser.Password != empleados.Password) { return Unauthorized(); }
            var token = GenerateJwtToken(existingUser.Correo);
            return Ok(new {Token = token});
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] Empleados empleados)
        {
            var existingUser = await _servicio.ObtenerUsuariosAsync(empleados.Correo);
            if(existingUser !=null)
            {
                return Conflict("El empleados ya existe");
            }
            await _servicio.CrearUsuarioAsync(empleados);
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

    }
}
